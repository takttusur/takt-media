using TaktTusur.Media.Core.Entities;

namespace TaktTusur.Media.Core.Interfaces;

/// <summary>
/// Describes asynchronous background job. 
/// </summary>
public interface IAsyncJob
{
	/// <summary>
	/// Execute the job.
	/// </summary>
	/// <param name="token">Cancellation token</param>
	/// <returns>Result of job execution</returns>
	Task<JobResult> ExecuteAsync(CancellationToken token);
}