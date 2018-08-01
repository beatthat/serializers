using System.IO;
using System.Text;
using UnityEngine;

namespace BeatThat.Serializers
{
    /// <summary>
    /// writes objects to json
    /// </summary>
    public class JsonSerializer<T> : JsonReader<T>, Serializer<T>
    {
        public static JsonSerializer<T> SHARED_INSTANCE = new JsonSerializer<T>();
        public static SerializerFactory<T> SHARED_INSTANCE_FACTORY = new SingleInstanceFactory<T>(SHARED_INSTANCE);

        public JsonSerializer() : this(null) { } // need a zero-arg constructor in case created by pool
        public JsonSerializer(Encoding encoding, int bufferSize = DEFAULT_BUFFER_SIZE) : base(encoding, bufferSize)
        {
            
        }

        virtual public void WriteOne(Stream s, T obj)
        {
            var json = JsonUtility.ToJson(obj);
#if NET_4_6
            using (var w = new StreamWriter(s, this.encoding, this.bufferSize, leaveOpen: true))
            {
                w.Write(json);
                w.Flush();
            }
#else
            var bytes = this.encoding.GetBytes(json);
            s.Write(bytes, 0, bytes.Length);
#endif
        }
    }
}


