using TaktTusur.Media.BackgroundCrawling.Core.Entities;
using TaktTusur.Media.BackgroundCrawling.Core.Interfaces;

namespace TaktTusur.Media.BackgroundCrawling.Core.Services;

public class PublicEventsFetchingAsyncJob : IAsyncJob
{
	public Task<JobResult> Execute(CancellationToken token)
	{
		throw new NotImplementedException();
	}
}