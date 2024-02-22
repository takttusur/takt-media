using Quartz;
using TaktTusur.Media.BackgroundCrawling.Core.Interfaces;

namespace TaktTusur.Media.BackgroundCrawler.QuartzJobs;

public class QuartzJobDecorator<TJob>(TJob job, ILogger<QuartzJobDecorator<TJob>> logger) : IJob
	where TJob : IAsyncJob
{
	private const string NoMessageAboutError = "The job didn't reported about error";
	private const string JobInterruptedByException = "{0} was interrupted by exception";
	private const string MaxRefireReachedError = "The maximum refire count has been reached for job: {jobName}";
	
	private const int MaxRefireCount = 3;
	
	private readonly TJob _job = job;

	public async Task Execute(IJobExecutionContext context)
	{
		if (context.RefireCount > MaxRefireCount)
		{
			if (context.RefireCount == MaxRefireCount + 1)
			{
				logger.LogWarning(MaxRefireReachedError, context.JobDetail.Key);
			}
			return;
		}
		try
		{
			var result = await _job.ExecuteAsync(context.CancellationToken);
			if (result.IsError)
			{
				throw new JobExecutionException(result.Error ?? NoMessageAboutError, null!, true);
			}
			context.Result = result;
		}
		catch (Exception e)
		{
			if (e is JobExecutionException)
			{
				throw;
			}
			throw new JobExecutionException(string.Format(JobInterruptedByException, context.JobDetail.Key), e, true);
		}
		
		
	}
}