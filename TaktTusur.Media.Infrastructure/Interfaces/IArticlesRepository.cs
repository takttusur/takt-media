using TaktTusur.Media.Core.News;

namespace TaktTusur.Media.BackgroundCrawling.Core.Interfaces;

public interface IArticlesRepository : IRepository<Article>
{
	public Article? GetByOriginalReference(string originalReference);
}