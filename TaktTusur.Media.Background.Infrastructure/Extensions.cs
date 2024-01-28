using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace TaktTusur.Media.BackgroundCrawling.Infrastructure;

public static class Extensions
{
	public static IHostApplicationBuilder AddInfrastructureLayer(this IHostApplicationBuilder builder)
	{
		builder.AddRedis("Redis");

		return builder;
	}
}