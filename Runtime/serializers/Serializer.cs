using System.IO;

namespace BeatThat.Serializers
{
    public interface Serializer<T> : Reader<T> where T : class
	{
		void WriteOne(Stream s, T obj);
	}
}
