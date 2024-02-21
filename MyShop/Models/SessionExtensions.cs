using System.Text.Json;

namespace MyShop.Models
{
    public       static       class SessionExtensions
    {

        // Method Named Set that takes in any type we give it    we give a key or name of the variable and the value of that item
        public static void Set<T>(this ISession session, string key, T value)
        {
            // Take that Key (variable name) and Serialize that object into JSON
            session.SetString(key, JsonSerializer.Serialize(value));
        }


        // Reading the Key out
        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            // Returning that object DE-Serialized
            return value == null ? default(T) : JsonSerializer.Deserialize<T>(value);
        }


    }
}
