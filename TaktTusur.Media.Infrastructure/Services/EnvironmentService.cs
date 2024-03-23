using TaktTusur.Media.Core.Interfaces;

namespace TaktTusur.Media.Infrastructure.Services;

public class EnvironmentService : IEnvironment
{
	public DateTimeOffset GetCurrentDateTime()
	{
		return DateTimeOffset.Now;
	}
}