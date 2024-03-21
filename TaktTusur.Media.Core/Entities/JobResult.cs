namespace TaktTusur.Media.BackgroundCrawling.Core.Entities;

/// <summary>
/// No value result of Job.
/// </summary>
public class JobResult
{
	/// <summary>
	/// True - if result is error,
	/// false - if success
	/// </summary>
	public bool IsError { get; protected set; }
	
	/// <summary>
	/// Error text, available only when IsError=true
	/// </summary>
	public string? Error { get; protected set; }

	/// <summary>
	/// Create <see cref="JobResult"/> for Error result
	/// </summary>
	/// <param name="error">Error message</param>
	/// <returns><see cref="JobResult"/> with IsError=true and error message</returns>
	public static JobResult ErrorResult(string error)
	{
		return new JobResult()
		{
			Error = error,
			IsError = true
		};
	}

	/// <summary>
	/// Create <see cref="JobResult"/> for Success result
	/// </summary>
	/// <returns><see cref="JobResult"/> where IsError=false</returns>
	public static JobResult SuccessResult()
	{
		return new JobResult()
		{
			IsError = false,
			Error = null
		};
	}
}