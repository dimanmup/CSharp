using Confluent.Kafka;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace Kafka_Consumer_Serializer
{
    public class CarDeserializer : IDeserializer<Car>
    {
        public Car Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
            if (!context.Headers.Any(x => x.Key == "user"))
            {
                return null;
            }

            string json = string.Join("", data.ToArray().Select(x => (char)x));
            return JsonConvert.DeserializeObject<Car>(json);
        }
    }
}
