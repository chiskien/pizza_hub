using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace PizzaHubWebApp
{
    public static class SessionExtension
    {
        public static void Set(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonConvert.DeserializeObject<T>(value);
        }
    }
}