using System.Drawing;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ScheduleLib
{
    internal class SubmissionParser
    {
        internal class ColorConverter : JsonConverter<Color>
        {

            public override Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                throw new NotImplementedException();
            }

            public override void Write(Utf8JsonWriter writer, Color value, JsonSerializerOptions options)
            {
                string hexColor = value.A == 255?$"#{value.R:X2}{value.G:X2}{value.B:X2}": $"#{value.R:X2}{value.G:X2}{value.B:X2}{value.A:X2}";
                writer.WriteStringValue(hexColor);
            }
        }
    }
}
