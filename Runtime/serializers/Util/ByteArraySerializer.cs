using System.IO;

namespace BeatThat.Serializers
{
    /// <summary>
    /// read/write a string
    /// </summary>
    public class ByteArraySerializer : ReaderBase<byte[]>, Serializer<byte[]>
    {
        public static Serializer<byte[]> SHARED_INSTANCE = new ByteArraySerializer();
        public static SerializerFactory<byte[]> SHARED_INSTANCE_FACTORY = new SingleInstanceFactory<byte[]>(SHARED_INSTANCE);

        public override bool isThreadsafe { get { return true; } }

        private const int DEFAULT_BUFFER_SIZE = 16 * 1024;

        public ByteArraySerializer(int bufferSize = DEFAULT_BUFFER_SIZE) 
        {
            this.bufferSize = bufferSize;
        }

        public int bufferSize { get; private set; }

        public override byte[][] ReadArray(Stream s)
        {
            throw new System.NotImplementedException();
        }

        public override byte[] ReadOne(Stream s)
        {
            // TODO: make ArrayPool threadsafe
            //using(var buffer = ArrayPool<byte>.Get(this.bufferSize))
            var buffer = new byte[this.bufferSize];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = s.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        public override byte[] ReadOne(Stream s, ref byte[] toObject)
        {
            toObject = ReadOne(s);
            return toObject;
        }

        public void WriteOne(Stream s, byte[] obj)
        {
            s.Write(obj, 0, obj.Length);
            s.Flush();
        }
    }
}


