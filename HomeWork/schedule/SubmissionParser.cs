using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HomeWork.schedule
{
    internal class SubmissionParser
    {
        public class ColorConverter : JsonConverter<Color>
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
