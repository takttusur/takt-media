using TaktTusur.Media.Core.Entities;
using TaktTusur.Media.Core.Interfaces;

namespace TaktTusur.Media.Infrastructure.Jobs;

public class PublicEventsFetchingAsyncJob : IAsyncJob
{
	public Task<JobResult> ExecuteAsync(CancellationToken token)
	{
		throw new NotImplementedException();
	}
}