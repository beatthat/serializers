using System.IO;
using UnityEngine;

namespace BeatThat.Serializers
{
    /// <summary>
    /// writes objects to json
    /// </summary>
    public class JsonSerializer<T> : JsonReader<T>, Serializer<T>
    {
        virtual public void WriteOne(Stream s, T obj)
        {
            var json = JsonUtility.ToJson(obj);
            using(var w = new StreamWriter(s)) {
                w.Write(json);
                w.Flush();
            }
        }
    }
}


