using Quartz;
using TaktTusur.Media.Core.Interfaces;
using TaktTusur.Media.Core.Settings;
using TaktTusur.Media.Infrastructure.Jobs;

namespace TaktTusur.Media.BackgroundCrawler.QuartzJobs;

public class NewsFetchingQuartzJob(
	NewsFetchingJobSettings settings,
	IArticlesRemoteSource articlesRemoteSource,
	IArticlesRepository articlesRepository,
	ITextTransformer textTransformer,
	IEnvironment environment,
	ILogger<NewsFetchingAsyncJob> logger)
	: NewsFetchingAsyncJob(settings, articlesRemoteSource, articlesRepository, textTransformer, environment, logger),
		IJob
{
	public async Task Execute(IJobExecutionContext context)
	{
		await this.ExecuteAsync(context.CancellationToken);
	}
}