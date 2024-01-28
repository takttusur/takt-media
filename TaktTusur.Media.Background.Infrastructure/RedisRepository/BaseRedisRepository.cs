using StackExchange.Redis;
using TaktTusur.Media.BackgroundCrawling.Core.Entities;
using TaktTusur.Media.BackgroundCrawling.Core.Interfaces;
using TaktTusur.Media.BackgroundCrawling.Infrastructure.Serializers;

namespace TaktTusur.Media.BackgroundCrawling.Infrastructure.RedisRepository;

/// <summary>
/// Base <see cref="IRepository{TEntity}"/> implementation using Redis cache.
/// Non thread safe.
/// Use transactions approach, don't forget to call Save finally.
/// </summary>
/// <typeparam name="T">DB Entity</typeparam>
public abstract class BaseRedisRepository<T> : IRepository<T> where T: IIdentifiable
{
	protected const string KeyDelimiter = ":";
	private readonly IJsonSerializer<T> _jsonSerializer;
	private readonly string _objectBaseKey;
	private readonly IDatabase _db;
	
	private ITransaction? _transaction = null;

	/// <summary>
	/// </summary>
	/// <param name="redisConnection"></param>
	/// <param name="jsonSerializer"></param>
	/// <param name="objectBaseKey">
	/// The key base for stored object.
	/// For example, ClassName = MyEntity, Id of MyEntity instance = 5. The key can be baseKey+id => MyEntity5.
	/// </param>
	protected BaseRedisRepository(IConnectionMultiplexer redisConnection, IJsonSerializer<T> jsonSerializer, string objectBaseKey)
	{
		if (string.IsNullOrWhiteSpace(objectBaseKey))
		{
			throw new ArgumentException("Base key cannot be null, empty or whitespace", nameof(objectBaseKey));
		}

		_jsonSerializer = jsonSerializer;
		_objectBaseKey = objectBaseKey;
		_db = redisConnection.GetDatabase();
	}
	
	public virtual void Add(T entity)
	{
		if (_transaction == null)
		{
			CreateTransaction();
		}

		var key = new RedisKey(GetEntityKey(entity));
		var value = new RedisValue(_jsonSerializer.Serialize(entity));

		_transaction!.StringSetAsync(key, value).ConfigureAwait(false);
	}

	public virtual void Delete(T entity)
	{
		Delete(entity.Id);
	}
	
	public virtual void Delete(long id)
	{
		if (_transaction == null)
		{
			CreateTransaction();
		}

		var key = new RedisKey(GetEntityKey(id));

		_transaction!.StringGetDeleteAsync(key).ConfigureAwait(false);
	}

	public bool Save()
	{
		return _transaction != null && _transaction.Execute();
	}

	public Task<bool> SaveAsync()
	{
		return _transaction != null ? _transaction.ExecuteAsync() : Task.FromResult(false);
	}

	protected string GetFullKey(string key)
	{
		return $"{_objectBaseKey}{KeyDelimiter}{key}";
	}
	
	protected string GetEntityKey(IIdentifiable identifiable)
	{
		return GetEntityKey(identifiable.Id);
	}
	
	private string GetEntityKey(long id)
	{
		return GetFullKey(id.ToString());
	}
	
	private void CreateTransaction()
	{
		_transaction = _db.CreateTransaction();
	}
}