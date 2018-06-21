using System.IO;

namespace BeatThat.Serializers
{
    public abstract class ReaderBase<T> : Reader<T> 
	{
		public ReadItemDelegate<T> itemReader { get { return m_itemReader?? (m_itemReader = this.ReadOne); } }
		private ReadItemDelegate<T> m_itemReader;

		abstract public T ReadOne(Stream s);

		abstract public T ReadOne(Stream s, T toObject);

		abstract public T[] ReadArray(Stream s);
	}
}

