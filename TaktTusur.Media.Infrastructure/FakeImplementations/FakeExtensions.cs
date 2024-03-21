using Microsoft.Extensions.DependencyInjection;
using TaktTusur.Media.BackgroundCrawling.Core.Interfaces;
using TaktTusur.Media.Core.News;

namespace TaktTusur.Media.BackgroundCrawling.Infrastructure.FakeImplementations;

public static class FakeExtensions
{
	public static IServiceCollection AddFakeRemoteSources(this IServiceCollection serviceCollection)
	{
		return serviceCollection.AddSingleton<IRemoteSource<Article>, FakeArticleRemoteSource>();
	}

	public static IServiceCollection AddFakeRepositories(this IServiceCollection serviceCollection)
	{
		return serviceCollection.AddSingleton<IRepository<Article>, FakeArticlesRepository>()
			.AddSingleton<IArticlesRepository, FakeArticlesRepository>();
	}
}