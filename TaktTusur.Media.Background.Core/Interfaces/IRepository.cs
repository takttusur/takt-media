namespace TaktTusur.Media.BackgroundCrawling.Core.Interfaces;

public interface IRepository<in TEntity>
{
	void Add(TEntity entity);
	
	void Save();
	Task SaveAsync();
}