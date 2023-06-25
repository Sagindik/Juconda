using Juconda.Framework;

namespace Juconda.Areas.admin.ViewModels.Pagination
{
    public class PageViewModel
    {
        private PageViewModel()
        {
        }

        public PageViewModel(FilterRequest filter, int total) : this(filter.Page, filter.Take, total)
        {
        }

        public PageViewModel(int page, int take, int total = 0)
        {
            CountTotal = total;

            if (take > 0)
            {
                Page = page > 0 ? page : 1;
                Take = take;

                CountStart = CountTotal == 0 ? 0 : Page * Take - Take + 1;
                CountFinish = Page * Take > CountTotal ? CountTotal : Page * Take;

                PageCount = CountTotal % Take == 0 ? CountTotal / Take : CountTotal / Take + 1;
            }
        }

        /// <summary>
        /// Страница
        /// </summary>
        public int Page { get; }
        /// <summary>
        /// Количество записей на странице
        /// </summary>
        public int Take { get; }

        /// <summary>
        /// Отображены объекты с
        /// </summary>
        public int CountStart { get; }
        /// <summary>
        /// Отображены объекты по
        /// </summary>
        public int CountFinish { get; }
        /// <summary>
        /// Общее количество объектов
        /// </summary>
        public int CountTotal { get; }

        /// <summary>
        /// Количество объектов на странице
        /// </summary>
        public int PageCount { get; }

        public bool HasPreviousPage => Page > 1;

        public bool HasNextPage => Page < PageCount;
    }
}
