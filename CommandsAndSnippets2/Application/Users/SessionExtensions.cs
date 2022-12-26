using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace BlueWest.WebApi.Session;

public static class SessionExtensions
{
    public static void Set<T>(this ISession session, string key, T value) where T: class
    {
        session.SetString(key, JsonSerializer.Serialize(value));
    }

    public static T Get<T>(this ISession session, string key) where T: class
    {
        var value = session.GetString(key);
        return value == null ? default : JsonSerializer.Deserialize<T>(value);
    }
}