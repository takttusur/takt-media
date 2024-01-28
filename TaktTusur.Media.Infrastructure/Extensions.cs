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
}