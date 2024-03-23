using TaktTusur.Media.Core.Events;
using TaktTusur.Media.Core.Interfaces;
using TaktTusur.Media.Core.News;
using TaktTusur.Media.Infrastructure.RedisRepository;
using TaktTusur.Media.Infrastructure.Serializers;
using TaktTusur.Media.Infrastructure.Services;

namespace TaktTusur.Media.Infrastructure;

public static class Extensions
{
	public static IServiceCollection AddInfrastructureLayer(this IServiceCollection serviceCollection, IConfiguration configuration)
	{
		serviceCollection.AddStackExchangeRedisCache(options =>
		{
			options.Configuration = configuration.GetValue<string>("CacheSettings:ConnectionString");
		});

		return serviceCollection;
	}
	
	public static IHostApplicationBuilder AddWorkerInfrastructureLayer(this IHostApplicationBuilder builder)
	{
		builder.Services.AddScoped<IRepository<PublicEvent>, PublicEventsRedisRepository>();

		builder.Services.AddJsonSerializerFor<Article>();

		builder.Services.AddScoped<ITextTransformer, TextTransformer>();
		builder.Services.AddScoped<IEnvironment, EnvironmentService>();
		
		return builder;
	}

	public static IServiceCollection AddJsonSerializerFor<T>(this IServiceCollection serviceCollection)
	{
		return serviceCollection.AddSingleton<IJsonSerializer<T>, CommonJsonSerializer<T>>();
	}
}