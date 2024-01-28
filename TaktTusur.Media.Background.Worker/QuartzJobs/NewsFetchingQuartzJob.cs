using Quartz;
using TaktTusur.Media.BackgroundCrawling.Core.Interfaces;
using TaktTusur.Media.BackgroundCrawling.Core.Services;
using TaktTusur.Media.BackgroundCrawling.Core.Settings;

namespace TaktTusur.Media.BackgroundCrawler.QuartzJobs;

public class NewsFetchingQuartzJob : NewsFetchingAsyncJob, IJob
{
	public NewsFetchingQuartzJob(NewsFetchingJobSettings settings, IArticlesRemoteSource articlesRemoteSource, IArticlesRepository articlesRepository, ITextTransformer textTransformer, IEnvironment environment, ILogger<NewsFetchingAsyncJob> logger) : base(settings, articlesRemoteSource, articlesRepository, textTransformer, environment, logger)
	{
	}


	public async Task Execute(IJobExecutionContext context)
	{
		await this.Execute(context.CancellationToken);
	}
}