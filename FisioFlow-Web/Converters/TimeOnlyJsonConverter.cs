using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FisioFlow_Web.Converters
{
    public class TimeOnlyJsonConverter : JsonConverter<TimeOnly>
    {

        public override TimeOnly Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {

            var value = reader.GetString();



            if (string.IsNullOrEmpty(value))
            {
                return default;
            }



            // Aceita 14:00
            if (TimeOnly.TryParseExact(
                value,
                "HH:mm",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var time))
            {
                return time;
            }



            // Aceita 14:00:00
            if (TimeOnly.TryParseExact(
                value,
                "HH:mm:ss",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out time))
            {
                return time;
            }



            throw new JsonException(
                $"Formato de horário inválido: {value}"
            );

        }





        public override void Write(
            Utf8JsonWriter writer,
            TimeOnly value,
            JsonSerializerOptions options)
        {

            writer.WriteStringValue(
                value.ToString(
                    "HH:mm"
                )
            );

        }

    }
}