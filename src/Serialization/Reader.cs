using System.IO;

namespace BeatThat.Serialization
{

	public delegate T ReadItemDelegate<T>(Stream s);

	/// <summary>
	/// interface for a class that can have its data overwritten, generally so it can be pooled and reused
	/// </summary>
	public interface Overwritable<T>
	{
		void Overwrite(T tempDTO);
	}

	public interface Reader<T> 
	{
		ReadItemDelegate<T> itemReader { get; }

		T ReadOne(Stream s);

		/// <summary>
		/// Read that uses an existing object to write into
		/// </summary>
		T ReadOne(Stream s, T toObject);

		T[] ReadArray(Stream s);
	}
}
