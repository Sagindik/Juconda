using Juconda.Domain.DomainModel;

namespace Juconda.Domain.Models
{
    public class Country : NameEntityBase
    {
        public virtual List<City> Cities { get; set; } = new();
    }
}
