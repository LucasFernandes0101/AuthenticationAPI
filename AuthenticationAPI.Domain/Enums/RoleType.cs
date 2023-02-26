using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;

namespace AuthenticationAPI.Domain.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RoleType
    {
        Administrator = 1,
        Common = 2
    }
}
