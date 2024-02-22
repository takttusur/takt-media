using TaktTusur.Media.BackgroundCrawling.Core.Entities;

namespace TaktTusur.Media.BackgroundCrawling.Core.Interfaces;

/// <summary>
/// The local repository for objects, to store in persistent storage(database).
/// </summary>
/// <typeparam name="TEntity">Should have ID(implements <see cref="IIdentifiable"/>)</typeparam>
public interface IRepository<in TEntity> where TEntity: IIdentifiable
{
	/// <summary>
	/// Add <see cref="TEntity"/> to storage
	/// </summary>
	/// <param name="entity">Stored object</param>
	void Add(TEntity entity);

	/// <summary>
	/// Delete entity from storage
	/// </summary>
	/// <param name="entity">Stored object</param>
	void Delete(TEntity entity);

	/// <summary>
	/// Update the entity by Id.
	/// </summary>
	/// <param name="entity">Entity with defined ID</param>
	void Update(TEntity entity);
	
	/// <summary>
	/// Execute planned actions with stored objects.
	/// IMPORTANT: Not all repositories can support transactions.
	/// Some actions will be executed at the moment of call.
	/// </summary>
	/// <returns>
	///	true - if saved successful, false - if not saved
	/// </returns>
	bool Save();
	
	/// <summary>
	/// <inheritdoc cref="Save"/>
	/// </summary>
	Task<bool> SaveAsync();
}