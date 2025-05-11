using System.Linq.Expressions;
using MongoDB.Driver.Linq;
using StackExchange.Redis;

namespace Ambev.DeveloperEvaluation.ORM.Repositories.Common;

public static class FilterQueryBuilder
{
    /// <summary>
    /// Applies filters to a queryable collection based on the provided filters.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="query"></param>
    /// <param name="redis"></param>
    /// <param name="filters"></param>
    /// <returns></returns>
    public static async Task<IQueryable<T>> ApplyFilters<T>(this IQueryable<T> query, IDatabase redis, Dictionary<string, string> filters)
    {
        var validPropertiesNames = await EntityPropertiesQuery.GetValidPropertiesAsync<T>(redis);
        foreach (var filter in filters)
        {
            var (propertyName, propertyValue, modifier) = CleanFilterKeyValue(filter);

            if (!validPropertiesNames.Contains(propertyName))
            {
                throw new ArgumentException($"Invalid filter format. Property {propertyName} does not exist.");
            }

            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, propertyName);

            Expression condition;

#pragma warning disable S6580 // Use a format provider when parsing date and time
            if (decimal.TryParse(propertyValue, out var numericValue))
            {
                condition = modifier switch
                {
                    "greaterThanOrEqual" => Expression.GreaterThanOrEqual(property, Expression.Constant(numericValue)),
                    "lessThanOrEqual" => Expression.LessThanOrEqual(property, Expression.Constant(numericValue)),
                    _ => Expression.Equal(property, Expression.Constant(numericValue))
                };
            }
            else if (DateTime.TryParse(propertyValue, out var dateValue))
            {
                condition = modifier switch
                {
                    "greaterThanOrEqual" => Expression.GreaterThanOrEqual(property, Expression.Constant(dateValue)),
                    "lessThanOrEqual" => Expression.LessThanOrEqual(property, Expression.Constant(dateValue)),
                    _ => Expression.Equal(property, Expression.Constant(dateValue))
                };
            }
            else
            {
                condition = modifier switch
                {
                    "startsWith" => Expression.Call(
                        Expression.Call(property, typeof(string).GetMethod("ToLower", Type.EmptyTypes)!),
                        typeof(string).GetMethod("StartsWith", new[] { typeof(string) })!,
                        Expression.Constant(propertyValue.ToLower())
                    ),
                    "endsWith" => Expression.Call(
                        Expression.Call(property, typeof(string).GetMethod("ToLower", Type.EmptyTypes)!),
                        typeof(string).GetMethod("EndsWith", new[] { typeof(string) })!,
                        Expression.Constant(propertyValue.ToLower())
                    ),
                    "contains" => Expression.Call(
                        Expression.Call(property, typeof(string).GetMethod("ToLower", Type.EmptyTypes)!),
                        typeof(string).GetMethod("Contains", new[] { typeof(string) })!,
                        Expression.Constant(propertyValue.ToLower())
                    ),
                    _ => Expression.Equal(
                        Expression.Call(property, typeof(string).GetMethod("ToLower", Type.EmptyTypes)!),
                        Expression.Constant(propertyValue.ToLower())
                    )
                };
            }
#pragma warning restore S6580 // Use a format provider when parsing date and time

            var lambda = Expression.Lambda<Func<T, bool>>(condition, parameter);
            query = query.Where(lambda);
        }

        return query;
    }

    private static (string CleanedKey, string CleanedValue, string Modifier) CleanFilterKeyValue(KeyValuePair<string, string> filter)
    {
        string modifier = "";
        string key = filter.Key;
        string value = filter.Value;

        if (value.StartsWith('*')) modifier = "endsWith";
        if (value.EndsWith('*')) modifier = "startsWith";
        if (value.StartsWith('*') && value.EndsWith('*')) modifier = "contains";
        if (key.Contains("_min")) modifier = "greaterThanOrEqual";
        if (key.Contains("_max")) modifier = "lessThanOrEqual";

        key = key.Replace("*", "").Replace("_min", "").Replace("_max", "");
        value = value.Replace("*", "").Replace("_min", "").Replace("_max", "");

        return (key, value, modifier);
    }
}