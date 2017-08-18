using System.IO;

namespace BeatThat.Serialization
{
	public interface Serializer<T> : Reader<T> where T : class
	{
		void WriteOne(Stream s, T obj);
	}
}