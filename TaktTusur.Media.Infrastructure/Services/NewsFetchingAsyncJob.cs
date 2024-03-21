using Microsoft.Extensions.Logging;
using TaktTusur.Media.BackgroundCrawling.Core.Entities;
using TaktTusur.Media.BackgroundCrawling.Core.Exceptions;
using TaktTusur.Media.BackgroundCrawling.Core.Interfaces;
using TaktTusur.Media.BackgroundCrawling.Core.Settings;
using TaktTusur.Media.Core.News;

namespace TaktTusur.Media.BackgroundCrawling.Core.Services;

/// <summary>
/// The job is responsible for fetching news from source and save it to <see cref="IArticlesRepository"/>
/// </summary>
public class NewsFetchingAsyncJob : IAsyncJob
{
	private readonly NewsFetchingJobSettings _settings;
	private readonly IArticlesRemoteSource _articlesRemoteSource;
	private readonly IArticlesRepository _articlesRepository;
	private readonly ITextTransformer _textTransformer;
	private readonly IEnvironment _environment;
	private readonly ILogger<NewsFetchingAsyncJob> _logger;
	private readonly Queue<Article> _brokenArticles = new Queue<Article>();

	private const string StartWorkingMsg = $"{nameof(NewsFetchingAsyncJob)} job is started";
	private const string FinishWorkingMsg = $"{nameof(NewsFetchingAsyncJob)} job is finished";
	private const string InterruptedMsg = $"{nameof(NewsFetchingAsyncJob)} job was interrupted";
	private const string DisabledMsg = $"{nameof(NewsFetchingAsyncJob)} job is disabled";
	
	public NewsFetchingAsyncJob(NewsFetchingJobSettings settings, 
		IArticlesRemoteSource articlesRemoteSource,
		IArticlesRepository articlesRepository,
		ITextTransformer textTransformer,
		IEnvironment environment,
		ILogger<NewsFetchingAsyncJob> logger)
	{
		_settings = settings;
		_articlesRemoteSource = articlesRemoteSource;
		_articlesRepository = articlesRepository;
		_textTransformer = textTransformer;
		_environment = environment;
		_logger = logger;
	}

	public async Task<JobResult> Execute(CancellationToken token)
	{
		if (!_settings.IsEnabled)
		{
			_logger.LogDebug(DisabledMsg);
			return JobResult.SuccessResult();
		}
		_logger.LogDebug(StartWorkingMsg);

		try
		{
			if (_articlesRemoteSource.IsPaginationSupported)
			{
				await ProcessArticlesByChunks();
			}
			else
			{
				await BulkProcessArticles();
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
	
	private async Task ProcessArticlesByChunks()
	{
		var skip = 0;
		const int take = 10;
		int total;
		do
		{
			var (articles, totalCount) = await _articlesRemoteSource.GetListAsync(skip, take);
			total = totalCount;
			
			await ProcessArticles(articles);

			skip += take;
		} while (skip < total);
	}

	private async Task BulkProcessArticles()
	{
		var articles = await _articlesRemoteSource.GetListAsync();
		if (articles.Count > _settings.MaxArticlesCount)
		{
			articles = articles
				.OrderBy(a => a.OriginalLastUpdated)
				.Take(_settings.MaxArticlesCount)
				.ToList();
		}
		await ProcessArticles(articles);
	}
	
	private async Task ProcessArticles(List<Article> articles)
	{
		var counter = 0;
		foreach (var article in articles)
		{
			if (string.IsNullOrEmpty(article.OriginalReference))
			{
				_brokenArticles.Enqueue(article);
				continue;
			}

			if (!TryUpdateExistingArticle(article))
			{
				AddNewArticle(article);	
			}

			counter++;
			
			if (counter < _settings.CommitBuffer) continue;
			
			await _articlesRepository.SaveAsync();
			counter = 0;
		}

		if (counter != 0)
		{
			await _articlesRepository.SaveAsync();
		}
	}

	private bool TryUpdateExistingArticle(Article remoteArticle)
	{
		// OriginalReference was verified previously
		var localArticle = _articlesRepository.GetByOriginalReference(remoteArticle.OriginalReference!);
		if (localArticle == null) return false;
		localArticle.OriginalLastUpdated = remoteArticle.OriginalLastUpdated;
		localArticle.Text =
			_textTransformer.MakeShorter(remoteArticle.Text, _settings.MaxSymbolsCount, _settings.MaxParagraphCount);
		localArticle.LastUpdated = _environment.GetCurrentDateTime();
		return true;
	}
	
	private void AddNewArticle(Article article)
	{
		article.Text = _textTransformer.MakeShorter(article.Text);
		article.LastUpdated = _environment.GetCurrentDateTime();
		_articlesRepository.Add(article);
	}
}