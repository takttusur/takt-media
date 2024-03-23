using StackExchange.Redis;
using TaktTusur.Media.Core.Events;
using TaktTusur.Media.Infrastructure.Serializers;

namespace TaktTusur.Media.Infrastructure.RedisRepository;

public class PublicEventsRedisRepository(
	IConnectionMultiplexer redisConnection,
	IJsonSerializer<PublicEvent> jsonSerializer)
	: BaseRedisRepository<PublicEvent>(redisConnection, jsonSerializer, PublicEventsRedisKey)
{
	private const string PublicEventsRedisKey = "TaktPublicEvents";
}