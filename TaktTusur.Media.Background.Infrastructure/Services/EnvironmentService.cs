using TaktTusur.Media.BackgroundCrawling.Core.Interfaces;

namespace TaktTusur.Media.BackgroundCrawling.Infrastructure.Services;

public class EnvironmentService : IEnvironment
{
	public DateTimeOffset GetCurrentDateTime()
	{
		return DateTimeOffset.Now;
	}
}