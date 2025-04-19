#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using com.IvanMurzak.Unity.MCP.Common.Data.Utils;

namespace com.IvanMurzak.Unity.MCP.Common.Json
{
    public class SerializedMemberConverter : JsonConverter<SerializedMember>
    {
        public override SerializedMember? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
            {
                return null;
            }

            var member = new SerializedMember();

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                    break;

                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    var propertyName = reader.GetString();
                    reader.Read(); // Move to the value token

                    switch (propertyName)
                    {
                        case "name":
                            member.name = reader.GetString();
                            break;
                        case "type":
                            member.type = reader.GetString();
                            break;
                        case "value":
                            member.valueJsonElement = JsonElement.ParseValue(ref reader);
                            break;
                        case "fields":
                            member.fields = JsonUtils.Deserialize<List<SerializedMember>>(ref reader, options);
                            break;
                        case "properties":
                            member.properties = JsonUtils.Deserialize<List<SerializedMember>>(ref reader, options);
                            break;
                        default:
                            reader.Skip();
                            break;
                    }
                }
            }


            return member;
        }

        public override void Write(Utf8JsonWriter writer, SerializedMember value, JsonSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteNullValue();
                return;
            }

            writer.WriteStartObject();

            writer.WriteString("name", value.name);
            writer.WriteString("type", value.type);

            if (value.valueJsonElement.HasValue)
            {
                writer.WritePropertyName("value");
                value.valueJsonElement.Value.WriteTo(writer);
            }
            if (value.fields != null && value.fields.Count > 0)
            {
                writer.WritePropertyName("fields");
                JsonSerializer.Serialize(writer, value.fields, options);
            }
            if (value.properties != null && value.properties.Count > 0)
            {
                writer.WritePropertyName("properties");
                JsonSerializer.Serialize(writer, value.properties, options);
            }

            writer.WriteEndObject();
        }
    }
}