using TaktTusur.Media.Core.News;

namespace TaktTusur.Media.BackgroundCrawling.Core.Interfaces;

/// <summary>
/// The <see cref="IRemoteSource{TEntity}"/> for <see cref="Article"/>
/// </summary>
public interface IArticlesRemoteSource : IRemoteSource<Article>;