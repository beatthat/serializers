namespace BeatThat.Serializers
{
    public interface SerializerFactory
    {
        Serializer<T> Create<T>();
    }

    public interface SerializerFactory<T>
	{
        Serializer<T> Create();
	}
}
