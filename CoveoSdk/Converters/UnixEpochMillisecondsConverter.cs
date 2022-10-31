using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CoveoSdk.Converters
{
    sealed internal class UnixEpochMillisecondsConverter : JsonConverter<DateTimeOffset>
    {
        public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            long millisecondsSinceEpoch = reader.GetInt64();
            return DateTimeOffset.FromUnixTimeMilliseconds(millisecondsSinceEpoch);
        }

        public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
        {
            long millisecondsSinceEpoch = value.ToUnixTimeMilliseconds();
            writer.WriteNumberValue(millisecondsSinceEpoch);
        }
    }
}
