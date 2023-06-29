namespace Juconda.Areas.admin.ViewModels.Pagination
{
    public class BasePage<TEntityList> where TEntityList : class
    {
        /// <summary>
        /// Возвращаемые объекты
        /// </summary>
        public IList<TEntityList> Objects { get; set; } = new List<TEntityList>();

        /// <summary>
        /// Возвращаемая пагинация
        /// </summary>
        public PageViewModel? Pagination { get; set; }
    }
}
