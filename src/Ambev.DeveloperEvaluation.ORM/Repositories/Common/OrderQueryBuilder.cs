using System.Linq.Expressions;
using System.Reflection;
using StackExchange.Redis;

namespace Ambev.DeveloperEvaluation.ORM.Repositories.Common
{
    public static class OrderQueryBuilder
    {
        /// <summary>
        /// Builds an order query based on the provided sort string.
        /// </summary>
        /// <param name="query">The base query to apply the order to.</param>
        /// <param name="redis">Instance os redis</param>
        /// <param name="sort">The sort string, e.g., "Name asc, Age desc".</param>
        /// <returns>The ordered query.</returns>
        public static async Task<IQueryable<T>> ApplyOrderAsync<T>(this IQueryable<T> query, IDatabase redis, string? sort)
        {
            if (string.IsNullOrWhiteSpace(sort))
            {
                return query;
            }

            var orderParts = sort.Split(',', StringSplitOptions.TrimEntries)
                .Select(part => part.Split(' ', StringSplitOptions.RemoveEmptyEntries))
                .Where(parts => parts.Length >= 1)
                .Select(parts => new
                {
                    PropertyName = parts[0],
                    Direction = parts.Length > 1 && parts[1].Equals("desc", StringComparison.CurrentCultureIgnoreCase) ? "desc" : "asc"
                })
                .ToList();

            if (orderParts.Count == 0)
            {
                throw new ArgumentException("Invalid ordering format (_order parameter)");
            }

            var validPropertiesNames = await EntityPropertiesQuery.GetValidPropertiesAsync<T>(redis);

            foreach (var part in orderParts)
            {
                if (!validPropertiesNames.Contains(part.PropertyName.ToLower()))
                {
                    throw new ArgumentException($"Invalid ordering format. Property {part.PropertyName} does not exist.");
                }
            }

            // Aplica a ordenação dinamicamente
            bool isFirstOrder = true;
            foreach (var part in orderParts)
            {
                var propertyInfo = typeof(T).GetProperty(part.PropertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (propertyInfo == null) continue;

                query = query.ApplyQueryOrder(propertyInfo, part.Direction, isFirstOrder);
                isFirstOrder = false;
            }

            return query;
        }

        private static IQueryable<T> ApplyQueryOrder<T>(this IQueryable<T> query, PropertyInfo propertyInfo, string direction, bool isFirstOrder)
        {
            var parameter = Expression.Parameter(typeof(T), "p");
            var property = Expression.Property(parameter, propertyInfo);
            var lambda = Expression.Lambda(property, parameter);

            // Determina o método (OrderBy, OrderByDescending, ThenBy, ThenByDescending)
            string orderOrThenCommand = isFirstOrder ? "OrderBy" : "ThenBy";
            string descOrAsc = direction.Equals("desc", StringComparison.CurrentCultureIgnoreCase) ? "Descending" : "";
            string methodName = $"{orderOrThenCommand}{descOrAsc}";

            // Cria a chamada do método
            var methodCall = Expression.Call(
                typeof(Queryable),
                methodName,
                [typeof(T), propertyInfo.PropertyType],
                query.Expression,
                Expression.Quote(lambda)
            );

            // Retorna a query com a ordenação aplicada
            return query.Provider.CreateQuery<T>(methodCall);
        }
    }
}