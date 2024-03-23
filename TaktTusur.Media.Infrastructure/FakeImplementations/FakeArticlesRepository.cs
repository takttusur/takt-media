using TaktTusur.Media.Core.Interfaces;
using TaktTusur.Media.Core.News;

namespace TaktTusur.Media.Infrastructure.FakeImplementations;

public class FakeArticlesRepository : FakeRepository<Article> ,IArticlesRepository
{
	public Article? GetByOriginalId(string originalId, string originalSource)
	{
		var article = _db.Values.FirstOrDefault(v => v.OriginalSource == originalSource && v.OriginalId == originalId);
		return article;
	}

	public Article? GetByOriginalReference(string originalReference)
	{
		throw new NotImplementedException();
	}

	public void Save()
	{
		throw new NotImplementedException();
	}

	public Task SaveAsync()
	{
		throw new NotImplementedException();
	}
}