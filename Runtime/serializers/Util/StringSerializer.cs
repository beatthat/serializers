using System.IO;
using System.Text;
using UnityEngine;

namespace BeatThat.Serializers
{
    /// <summary>
    /// read/write a string
    /// </summary>
    public class StringSerializer : ReaderBase<string>, Serializer<string>
    {
        public static StringSerializer UTF8 = new StringSerializer(Encoding.UTF8);
        public const int DEFAULT_BUFFER_SIZE = 1024;

        public StringSerializer(Encoding encoding = null, int bufferSize = DEFAULT_BUFFER_SIZE)
        {
            this.encoding = encoding ?? Encoding.UTF8;
            this.bufferSize = bufferSize;
        }

        public Encoding encoding { get; private set; }
        public int bufferSize { get; private set; }

        public override bool isThreadsafe { get { return true; } }

        public override string[] ReadArray(Stream s)
        {
            throw new System.NotImplementedException();
        }

        public override string ReadOne(Stream s)
        {
            using (var r = new StreamReader(s, this.encoding))
            {
                return r.ReadToEnd();
            }
        }

        public override string ReadOne(Stream s, ref string toObject)
        {
            using (var r = new StreamReader(s, this.encoding))
            {
                toObject = r.ReadToEnd();
                return toObject;
            }
        }

        public void WriteOne(Stream s, string obj)
        {
#if NET_4_6
            using (var w = new StreamWriter(s, this.encoding, this.bufferSize, leaveOpen: true))
            {
                w.Write(obj);
                w.Flush();
            }
#else
            var bytes = this.encoding.GetBytes(obj);
            s.Write(bytes, 0, bytes.Length);
#endif
        }
    }
}


