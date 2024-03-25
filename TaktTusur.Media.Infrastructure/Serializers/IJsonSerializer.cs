namespace TaktTusur.Media.Infrastructure.Serializers;

public interface IJsonSerializer<T>
{
	public string Serialize(T o);

	public T? Deserialize(string json);
}