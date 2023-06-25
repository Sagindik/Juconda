namespace Juconda.Framework
{
    /// <summary>
    /// Настройка параметров для входящего запроса
    /// </summary>
    public class FilterRequest
    {
        private int _page;
        private int _take;

        /// <summary>
        /// Страница
        /// </summary>
        public int Page { get; set; } = 1;

        /// <summary>
        /// Количество записей
        /// </summary>
        public int Take { get; set; } = 10;

        /// <summary>
        /// Поиск Like, по свойствам сущности
        /// </summary>
        public string? Query { get; set; }

        /// <summary>
        /// Название столбца столбца (как в Dto), для сортировки
        /// </summary>
        public string? OrderByColumn { get; set; }

        /// <summary>
        /// В порядке убывания?
        /// </summary>
        public bool? OrderByDescending { get; set; }
    }
}
