using System.Reflection;
using System.Text.Json;
using StackExchange.Redis;

namespace Ambev.DeveloperEvaluation.ORM.Repositories.Common;

internal static class EntityPropertiesQuery
{
    internal static async Task<HashSet<string>> GetValidPropertiesAsync<T>(IDatabase redis)
    {
        var entityType = typeof(T).Name;
        var cacheKey = $"entity:{entityType}:properties";

        var cachedProperties = await redis.StringGetAsync(cacheKey);
        if (cachedProperties.HasValue)
        {
            return JsonSerializer.Deserialize<HashSet<string>>(cachedProperties!)!;
        }

        var properties = typeof(T)
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Select(p => p.Name.ToLower())
            .ToHashSet();

        await redis.StringSetAsync(cacheKey, JsonSerializer.Serialize(properties), TimeSpan.FromHours(1));

        return properties;
    }
}