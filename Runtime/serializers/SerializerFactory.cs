using System.IO;

namespace BeatThat.Serializers
{
    public interface SerializerFactory<T>
	{
        Serializer<T> Create();
	}
}
