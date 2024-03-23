using Quartz;
using TaktTusur.Media.Core.Exceptions;
using TaktTusur.Media.Core.Interfaces;
using TaktTusur.Media.Core.News;
using TaktTusur.Media.Core.Settings;

namespace TaktTusur.Media.BackgroundCrawler.QuartzJobs;

public class NewsFetchingQuartzJob(
	NewsFetchingJobSettings settings,
	IArticlesRemoteSource articlesRemoteSource,
	IArticlesRepository articlesRepository,
	ITextTransformer textTransformer,
	IEnvironment environment,
	ILogger<NewsFetchingQuartzJob> logger)
	: IJob
{
	private readonly Queue<Article> _brokenArticles = new Queue<Article>();

	private const string StartWorkingMsg = $"{nameof(NewsFetchingQuartzJob)} job is started";
	private const string FinishWorkingMsg = $"{nameof(NewsFetchingQuartzJob)} job is finished";
	private const string InterruptedMsg = $"{nameof(NewsFetchingQuartzJob)} job was interrupted";
	private const string DisabledMsg = $"{nameof(NewsFetchingQuartzJob)} job is disabled";
	
	public async Task Execute(IJobExecutionContext context)
	{
		if (!settings.IsEnabled)
		{
			logger.LogDebug(DisabledMsg);
			return;
		}
		logger.LogDebug(StartWorkingMsg);

		try
		{
			if (articlesRemoteSource.IsPaginationSupported)
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
			logger.LogWarning(e, InterruptedMsg);
			return;
		}
		catch (RepositoryReadingException e)
		{
			logger.LogError(e, InterruptedMsg);
			return;
		}
		catch (RepositoryWritingException e)
		{
			logger.LogError(e, InterruptedMsg);
			return;
		}
		
		logger.LogDebug(FinishWorkingMsg);
	}
	
	private async Task ProcessArticlesByChunks()
	{
		var skip = 0;
		const int take = 10;
		int total;
		do
		{
			var (articles, totalCount) = await articlesRemoteSource.GetListAsync(skip, take);
			total = totalCount;
			
			await ProcessArticles(articles);

			skip += take;
		} while (skip < total);
	}

	private async Task BulkProcessArticles()
	{
		var articles = await articlesRemoteSource.GetListAsync();
		if (articles.Count > settings.MaxArticlesCount)
		{
			articles = articles
				.OrderBy(a => a.OriginalUpdatedAt)
				.Take(settings.MaxArticlesCount)
				.ToList();
		}
		await ProcessArticles(articles);
	}
	
	private async Task ProcessArticles(List<Article> articles)
	{
		var counter = 0;
		foreach (var article in articles)
		{
			if (string.IsNullOrEmpty(article.OriginalId))
			{
				_brokenArticles.Enqueue(article);
				continue;
			}

			if (!TryUpdateExistingArticle(article))
			{
				AddNewArticle(article);	
			}

			counter++;
			
			if (counter < settings.CommitBuffer) continue;
			
			await articlesRepository.SaveAsync();
			counter = 0;
		}

		if (counter != 0)
		{
			await articlesRepository.SaveAsync();
		}
	}

	private bool TryUpdateExistingArticle(Article remoteArticle)
	{
		// OriginalReference was verified previously
		var localArticle = articlesRepository.GetByOriginalId(remoteArticle.OriginalId!, remoteArticle.OriginalSource!);
		if (localArticle == null) return false;
		localArticle.OriginalUpdatedAt = remoteArticle.OriginalUpdatedAt;
		localArticle.Text =
			textTransformer.MakeShorter(remoteArticle.Text, settings.MaxSymbolsCount, settings.MaxParagraphCount);
		localArticle.LastUpdated = environment.GetCurrentDateTime();
		return true;
	}
	
	private void AddNewArticle(Article article)
	{
		article.Text = textTransformer.MakeShorter(article.Text);
		article.LastUpdated = environment.GetCurrentDateTime();
		articlesRepository.Add(article);
	}
}