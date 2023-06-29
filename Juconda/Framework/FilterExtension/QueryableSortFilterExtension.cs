using System.Linq.Expressions;

namespace Juconda.Framework.Queryable.FilterExtension
{
    public static class QueryableSortFilterExtension
    {
        /// <summary>
        /// Применение сортировки для запроса, по свойству в списочном Dto
        /// </summary>
        /// <param name="queryable"> Входящий запрос </param>
        /// <param name="propertyName"> Название столбца для фильтрации, как в Dto </param>
        /// <param name="orderByDescending"> В порядке убывания? </param>
        public static IQueryable<TEntityListDto> ExecuteOrderByFilter<TEntityListDto>(this IQueryable<TEntityListDto> queryable,
            string? propertyName, bool? orderByDescending)
            where TEntityListDto : class
        {
            var property = typeof(TEntityListDto).GetProperties().FirstOrDefault(p =>
                string.Equals(p.Name, propertyName, StringComparison.CurrentCultureIgnoreCase));

            if (string.IsNullOrEmpty(propertyName) && property == null)
            {
                // По умолчанию ef не применяет какой-либо конкретный порядок - ORDER BY (SELECT 1)
                // В таком случае возвращаются записи, которые быстрее найдутся
                // Указываем стандартную сортировку по полю Id
                return queryable;
            }

            ParameterExpression entity = Expression.Parameter(typeof(TEntityListDto), nameof(entity));
            var orderByExpression = Expression.Lambda(Expression.Property(entity, property), entity);

            var command = orderByDescending.GetValueOrDefault() ? 
                nameof(System.Linq.Queryable.OrderByDescending) : nameof(System.Linq.Queryable.OrderBy);

            var resultExpression = Expression.Call(
                typeof(System.Linq.Queryable),
                command,
                new[] { typeof(TEntityListDto), orderByExpression.ReturnType },
                queryable.Expression,
                Expression.Quote(orderByExpression));

            return queryable.Provider.CreateQuery<TEntityListDto>(resultExpression);
        }
    }
}