namespace Juconda.Framework.Queryable.FilterExtension
{
    public static class QueryablePageFilterExtension
    {
        /// <summary>
        /// Применение фильтрации по страницам к запросу
        /// </summary>
        /// <param name="queryable"> Входящий запрос </param>
        /// <param name="page"> Текущая страница </param>
        /// <param name="take"> Количество объектов на странице </param>
        public static List<TEntityListDto> ExecutePageFilter<TEntityListDto>(this List<TEntityListDto> list, int page, int take)
            where TEntityListDto : class
        {
            return list.Skip((page > 0 ? page - 1 : 0) * take).Take(take).ToList();
        }
    }
}