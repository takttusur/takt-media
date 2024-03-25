using StackExchange.Redis;
using TaktTusur.Media.Core.Interfaces;
using TaktTusur.Media.Core.News;
using TaktTusur.Media.Infrastructure.Serializers;

namespace TaktTusur.Media.Infrastructure.RedisRepository;

public class ArticlesRedisRepository(
	IConnectionMultiplexer redisConnection,
	IJsonSerializer<Article> jsonSerializer,
	string objectBaseKey)
	: BaseRedisRepository<Article>(redisConnection, jsonSerializer, objectBaseKey), IArticlesRepository
{
	public Article? GetByOriginalId(string originalId, string originalSource)
	{
		throw new NotImplementedException();
	}
}