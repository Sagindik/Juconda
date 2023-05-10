using Juconda.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Juconda.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public class AppDbContext : IdentityDbContext<User, Domain.Models.Identity.IdentityRole, long>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CountryOfProduction> CountryOfProductions { get; set; }
    }
}