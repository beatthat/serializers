using System.IO;

namespace BeatThat.Serializers
{
    public abstract class ReaderBase<T> : Reader<T>
    {
        public ReadItemDelegate<T> itemReader { get { return m_itemReader ?? (m_itemReader = this.ReadOne); } }
        private ReadItemDelegate<T> m_itemReader;

        abstract public T ReadOne(Stream s);

        virtual public T ReadOne(Stream s, ref T toObject)
        {
            toObject = ReadOne(s);
            return toObject;
        }

        abstract public T[] ReadArray(Stream s);

        /// <summary>
        /// Override to return TRUE if you want async calls to read to run on a separate thread.
        /// </summary>
        virtual public bool isThreadsafe { get { return false; } }

    }
}

