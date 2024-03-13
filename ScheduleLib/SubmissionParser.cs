using System.Drawing;
using Newtonsoft.Json;
namespace ScheduleLib
{
    internal class SubmissionParser
    {
        internal class ColorConverter : JsonConverter<Color>
        {

            public override Color ReadJson(JsonReader reader, Type objectType, Color existingValue, bool hasExistingValue, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }



            public override void WriteJson(JsonWriter writer, Color value, JsonSerializer serializer)
            {
                string hexColor = value.A == 255?$"#{value.R:X2}{value.G:X2}{value.B:X2}": $"#{value.R:X2}{value.G:X2}{value.B:X2}{value.A:X2}";
                writer.WriteValue(hexColor);

            }
        }
    }
}
