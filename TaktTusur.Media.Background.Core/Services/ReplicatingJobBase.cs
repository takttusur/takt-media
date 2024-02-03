using Microsoft.Extensions.Logging;
using TaktTusur.Media.BackgroundCrawling.Core.Entities;
using TaktTusur.Media.BackgroundCrawling.Core.Exceptions;
using TaktTusur.Media.BackgroundCrawling.Core.Interfaces;
using TaktTusur.Media.BackgroundCrawling.Core.Settings;
using TaktTusur.Media.Core.Interfaces;

namespace TaktTusur.Media.BackgroundCrawling.Core.Services;

public abstract class ReplicatingJobBase<T> : IAsyncJob where T: IIdentifiable, IReplicated
{
	private const string StartWorkingMsg = $"{nameof(ReplicatingJobBase<T>)} job is started";
	private const string FinishWorkingMsg = $"{nameof(ReplicatingJobBase<T>)} job is finished";
	private const string InterruptedMsg = $"{nameof(ReplicatingJobBase<T>)} job was interrupted";
	private const string DisabledMsg = $"{nameof(ReplicatingJobBase<T>)} job is disabled";
	
	private readonly IRemoteSource<T> _remoteSource;
	private readonly IRepository<T> _repository;
	private readonly ReplicationJobSettings _jobSettings;
	private readonly ILogger _logger;
	private readonly IEnvironment _environment;
	private readonly Queue<T> _brokenItems = new Queue<T>();

	protected ReplicatingJobBase(
		IRemoteSource<T> remoteSource, 
		IRepository<T> repository,
		ReplicationJobSettings jobSettings,
		ILogger logger,
		IEnvironment environment)
	{
		_remoteSource = remoteSource;
		_repository = repository;
		_jobSettings = jobSettings;
		_logger = logger;
		_environment = environment;
	}
	
	public virtual async Task<JobResult> Execute(CancellationToken token)
	{
		if (!_jobSettings.IsEnabled)
		{
			_logger.LogDebug(DisabledMsg);
			return JobResult.SuccessResult();
		}
		
		try
		{
			if (_remoteSource.IsPaginationSupported)
			{
				await FetchByChunks();
			}
			else
			{
				await FetchByOneRequest();
			}
		}
		catch (RemoteReadingException e)
		{
			_logger.LogWarning(e, InterruptedMsg);
			return JobResult.ErrorResult(e.Message);
		}
		catch (RepositoryReadingException e)
		{
			_logger.LogError(e, InterruptedMsg);
			return JobResult.ErrorResult(e.Message);
		}
		catch (RepositoryWritingException e)
		{
			_logger.LogError(e, InterruptedMsg);
			return JobResult.ErrorResult(e.Message);
		}
		
		_logger.LogDebug(FinishWorkingMsg);
		return JobResult.SuccessResult();
	}
	
	private async Task FetchByChunks()
	{
		var skip = 0;
		const int take = 10;
		int total;
		do
		{
			var (items, totalCount) = await _remoteSource.GetListAsync(skip, take);
			total = totalCount;
			
			await ProcessItems(items);

			skip += take;
		} while (skip < total);
	}

	private async Task FetchByOneRequest()
	{
		var items = await _remoteSource.GetListAsync();
		if (items.Count > _jobSettings.MaxReplicatedItems)
		{
			items = items
				.OrderBy(a => a.OriginalUpdatedAt)
				.Take(_jobSettings.MaxReplicatedItems)
				.ToList();
		}
		await ProcessItems(items);
	}
	
	private async Task ProcessItems(List<T> items)
	{
		var counter = 0;
		foreach (var item in items)
		{
			if (string.IsNullOrEmpty(item.OriginalId))
			{
				_brokenItems.Enqueue(item);
				continue;
			}

			if (!TryUpdateExistingItem(item))
			{
				AddNewItem(item);	
			}

			counter++;
			
			if (counter < _jobSettings.CommitBuffer) continue;
			
			await _repository.SaveAsync();
			counter = 0;
		}

		if (counter != 0)
		{
			await _repository.SaveAsync();
		}
	}

	protected abstract bool TryUpdateExistingItem(T remoteItem);

	protected abstract void AddNewItem(T item);

	// private bool TryUpdateExistingItem(T remoteItem)
	// {
	// 	// OriginalReference was verified previously
	// 	var localArticle = _repository.GetByOriginalReference(remoteArticle.OriginalReference!);
	// 	if (localArticle == null) return false;
	// 	localArticle.OriginalLastUpdated = remoteArticle.OriginalLastUpdated;
	// 	localArticle.Text =
	// 		_textTransformer.MakeShorter(remoteArticle.Text, _settings.MaxSymbolsCount, _settings.MaxParagraphCount);
	// 	localArticle.LastUpdated = _environment.GetCurrentDateTime();
	// 	return true;
	// }

	// private void AddNewItem(T item)
	// {
	// 	item.Text = _textTransformer.MakeShorter(item.Text);
	// 	item.LastUpdated = _environment.GetCurrentDateTime();
	// 	_articlesRepository.Add(item);
	// }
}