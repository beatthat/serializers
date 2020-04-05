using System;
using System.Reflection;
using BeatThat.TypeUtil;
using UnityEngine;

namespace BeatThat.Serializers
{
    public static class SerializerConfig
    {
        public static void SetDefaultSerializer(SerializerFactory f)
        {
            DEFAULT_SERIALIZER_FACTORY = f;
        }

        public static bool GetDefaultSerializer<T>(out Serializer<T> result, out string error)
        {
            if (!typeof(T).IsArray)
            {
                result = DEFAULT_SERIALIZER_FACTORY != null
                    ? DEFAULT_SERIALIZER_FACTORY.Create<T>()
                    : JsonSerializer<T>.SHARED_INSTANCE;
                error = null;
                return true;
            }
            if (!DEFAULT_ARRAY_SERIALIZER_HAS_DONE_LOAD)
            {
                DEFAULT_ARRAY_SERIALIZER_HAS_DONE_LOAD = true;
                try
                {
                    var opts = TypeUtils.FindTypesWithAttribute<CanReadAndWriteArraysAttribute>();
                    var facType = opts != null && opts.Length > 0
                        ? Array.Find(opts, x => typeof(SerializerFactory).IsAssignableFrom(x.type))
                        : null;
                    if (facType != default(TypeAndAttribute) && facType.type != null)
                    {
                        ConstructorInfo c = facType.type.GetConstructor(new Type[] { });
                        DEFAULT_ARRAY_SERIALIZER_FACTORY = c.Invoke(new object[] { }) as SerializerFactory;
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
            }
            if (DEFAULT_ARRAY_SERIALIZER_FACTORY != null)
            {
                result = DEFAULT_ARRAY_SERIALIZER_FACTORY.Create<T>();
                error = null;
                return true;
            }
#if UNITY_EDITOR || DEBUG_UNSTRIP
            Debug.LogWarning("No serializer found that can handle array types like "
                + typeof(T) + ". Possible fix: npm install --save @beatthat/serializers-netwonsoft");
#endif
            result = null;
            error = "No default serializer can handle array types like " + typeof(T);
            return false;
        }

        private static bool DEFAULT_ARRAY_SERIALIZER_HAS_DONE_LOAD = false;
        private static SerializerFactory DEFAULT_ARRAY_SERIALIZER_FACTORY;
        private static SerializerFactory DEFAULT_SERIALIZER_FACTORY;
    }
}