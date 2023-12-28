using System.Buffers.Text;
using System.Buffers;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace Slm.Utils.Core.Converters;
/// <summary>
/// Json日期类型转换器
/// </summary>
public class LongConverter : JsonConverter<long>
{
    public override long Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            // try to parse number directly from bytes
            ReadOnlySpan<byte> span = reader.HasValueSequence ? reader.ValueSequence.ToArray() : reader.ValueSpan;
            if (Utf8Parser.TryParse(span, out long number, out int bytesConsumed) && span.Length == bytesConsumed)
                return number;

            // try to parse from a string if the above failed, this covers cases with other escaped/UTF characters
            if (Int64.TryParse(reader.GetString(), out number))
                return number;
        }

        // fallback to default handling
        return reader.GetInt64();

    }

    public override void Write(Utf8JsonWriter writer, long value, JsonSerializerOptions options)
    {
        if (value.ToString().Length == 19)
        {
            writer.WriteStringValue(value.ToString());
        }
        else
        {
            writer.WriteNumberValue(value);
        }

    }
}