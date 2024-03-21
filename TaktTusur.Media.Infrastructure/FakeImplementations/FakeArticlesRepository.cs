using TaktTusur.Media.BackgroundCrawling.Core.Interfaces;
using TaktTusur.Media.Core.News;

namespace TaktTusur.Media.BackgroundCrawling.Infrastructure.FakeImplementations;

public class FakeArticlesRepository : FakeRepository<Article> ,IArticlesRepository
{
	public Article? GetByOriginalId(string originalId, string originalSource)
	{
		var article = _db.Values.FirstOrDefault(v => v.OriginalSource == originalSource && v.OriginalId == originalId);
		return article;
	}
}