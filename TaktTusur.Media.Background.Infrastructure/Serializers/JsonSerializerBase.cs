using Newtonsoft.Json;

namespace TaktTusur.Media.BackgroundCrawling.Infrastructure.Serializers;

public class JsonSerializerBase<T> : IJsonSerializer<T>
{
	public string Serialize(T o)
	{
		return JsonConvert.SerializeObject(o);
	}

	public T? Deserialize(string json)
	{
		return JsonConvert.DeserializeObject<T>(json);
	}
}