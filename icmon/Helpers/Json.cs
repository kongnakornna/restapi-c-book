using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace Jtech.Common.Helpers
{
    public static class Json
    {
        public static string Serialize(object obj)
        {
            if (obj.GetType() == typeof(JsonDocument))
                return System.Text.Json.JsonSerializer.Serialize(obj);
            else
                return JsonConvert.SerializeObject(obj);
        }
        public static object? DeserializeObject(string jsonData,Type? type=null)
        {
           return type==null?
                JsonConvert.DeserializeObject(jsonData) :
                JsonConvert.DeserializeObject(jsonData,type);
        }
        public static T? DeserializeObject<T>(string jsonData)
        {
            return JsonConvert.DeserializeObject<T>(jsonData);
        }

        public static JsonDocument Convert(DataTable dt)
        {
            var jsonText=Helpers.Json.Serialize(dt);
            return JsonDocument.Parse(jsonText);
        }
    }
}
