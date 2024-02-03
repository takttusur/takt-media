using TaktTusur.Media.Core.News;

namespace TaktTusur.Media.BackgroundCrawling.Core.Interfaces;

/// <summary>
/// The <see cref="IRepository{TEntity}"/> for <see cref="Article"/>
/// </summary>
public interface IArticlesRepository : IRepository<Article>
{
	/// <summary>
	/// Get <see cref="Article"/> using original identifier(remote resource id).
	/// </summary>
	/// <param name="originalId">ID of <see cref="Article"/> at remote resource.</param>
	/// <returns>If found - <see cref="Article"/>, otherwise null</returns>
	public Article? GetByOriginalId(string originalId);
}