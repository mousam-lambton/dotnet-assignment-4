namespace Assignment4.Helpers;

using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
public static class SessionExtensions
{
    public static void SetObject(this ISession session, string key, object value)
    {
        session.SetString(key, JsonConvert.SerializeObject(value));  // Serialize object to string
    }

    public static T GetObject<T>(this ISession session, string key)
    {
        var value = session.GetString(key);
        return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);  // Deserialize to object
    }
    
    // Clears all session data
    public static void ClearSession(this ISession session)
    {
        session.Clear(); 
    }
    
    // Removes the specified session key
    public static void RemoveSession(this ISession session, string key)
    {
        session.Remove(key);  
    }
}
