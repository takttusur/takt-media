using TaktTusur.Media.BackgroundCrawling.Core.Entities;

namespace TaktTusur.Media.BackgroundCrawling.Core.Interfaces;

public interface IAsyncJob
{
	Task<JobResult> Execute(CancellationToken token);
}