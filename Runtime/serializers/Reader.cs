using System.IO;

namespace BeatThat.Serializers
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
        T ReadOne(Stream s, ref T toObject);

        T[] ReadArray(Stream s);

        bool isThreadsafe { get; }
    }


#if NET_4_6
    public static class ReaderAsyncExt
    {
        public static async System.Threading.Tasks.Task<T> ReadAsync<T>(this Reader<T> r, Stream s)
        {
            if(!r.isThreadsafe) {
                return r.ReadOne(s);
            }

            return await System.Threading.Tasks.Task.Run(() => r.ReadOne(s)).ConfigureAwait(false);
        }
    }
#endif
}

