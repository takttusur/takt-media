using StackExchange.Redis;
using TaktTusur.Media.BackgroundCrawling.Infrastructure.Serializers;
using TaktTusur.Media.Domain.Events;

namespace TaktTusur.Media.BackgroundCrawling.Infrastructure.RedisRepository;

public class PublicEventsRedisRepository(
	IConnectionMultiplexer redisConnection,
	IJsonSerializer<PublicEvent> jsonSerializer)
	: BaseRedisRepository<PublicEvent>(redisConnection, jsonSerializer, PublicEventsRedisKey)
{
	private const string PublicEventsRedisKey = "TaktPublicEvents";
}