namespace TaktTusur.Media.BackgroundCrawling.Core.Interfaces;

public interface IRepository<in TEntity>
{
	void Add(TEntity entity);

	void Delete(TEntity entity);
	
	bool Save();
	Task<bool> SaveAsync();
}