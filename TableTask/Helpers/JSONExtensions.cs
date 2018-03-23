using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace TableTask.Helper
{
    public static class JSONExtensions
    {
        public static string ToJson(this object o)
        {
            return JsonConvert.SerializeObject(o);
        }

        public static string ToJson(this object o, bool camelCase = false)
        {
            if (camelCase)
            {
                var settings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver() //new LowercaseContractResolver()
                };
                return JsonConvert.SerializeObject(o, settings);
            }
            return JsonConvert.SerializeObject(o);
        }

        public static string ToJsonCamelCase(this object o)
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            return JsonConvert.SerializeObject(o, settings);
        }

        public static string ToJsonLowerCase(this object o)
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new LowerCasePropertyNamesContractResolver()
            };
            return JsonConvert.SerializeObject(o, settings);
        }
    }

    public class LowerCasePropertyNamesContractResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            return propertyName.ToLower();
        }
    }
}