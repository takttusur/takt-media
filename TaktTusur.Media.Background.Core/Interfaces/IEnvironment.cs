namespace TaktTusur.Media.BackgroundCrawling.Core.Interfaces;

/// <summary>
/// Represents environment methods.
/// </summary>
public interface IEnvironment
{
	/// <summary>
	/// Returns current date time.
	/// </summary>
	/// <returns><see cref="DateTimeOffset"/></returns>
	DateTimeOffset GetCurrentDateTime();
}