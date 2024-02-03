using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TaktTusur.Media.BackgroundCrawling.Core.Interfaces;
using TaktTusur.Media.BackgroundCrawling.Infrastructure.RedisRepository;
using TaktTusur.Media.BackgroundCrawling.Infrastructure.Serializers;
using TaktTusur.Media.Core.Events;

namespace TaktTusur.Media.BackgroundCrawling.Infrastructure;

public static class Extensions
{
	public static IHostApplicationBuilder AddInfrastructureLayer(this IHostApplicationBuilder builder)
	{
		builder.AddRedis("Redis");

		builder.Services.AddScoped<IRepository<PublicEvent>, PublicEventsRedisRepository>();

		builder.Services.AddJsonSerializerFor<PublicEvent>();
		
		return builder;
	}

	public static IServiceCollection AddJsonSerializerFor<T>(this IServiceCollection serviceCollection)
	{
		return serviceCollection.AddSingleton<IJsonSerializer<T>, CommonJsonSerializer<T>>();
	}
}