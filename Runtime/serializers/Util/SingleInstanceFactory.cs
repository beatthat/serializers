
namespace BeatThat.Serializers
{

    public class SingleInstanceFactory<T> : SerializerFactory<T>
    {
        public SingleInstanceFactory(Serializer<T> instance)
        {
            this.instance = instance;
        }

        private Serializer<T> instance { get; set; }

        public Serializer<T> Create()
        {
            return this.instance;
        }

    }
}
