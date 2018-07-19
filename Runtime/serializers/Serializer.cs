using System.IO;

namespace BeatThat.Serializers
{
    public interface Serializer<T> : Reader<T>
	{
		void WriteOne(Stream s, T obj);
	}
}
