using Juconda.Domain.Models;
using Juconda.Domain.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Juconda.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public class AppDbContext : IdentityDbContext<User, Domain.Models.Identity.IdentityRole, long>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Регистрация всех IEntityTypeConfiguration<TEntity>
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);

            #region Identity

            builder.Entity<IdentityUserToken<long>>().ToTable("IdentityUserTokens", "Identity");
            builder.Entity<IdentityUserRole<long>>().ToTable("IdentityUserRoles", "Identity");
            builder.Entity<IdentityRoleClaim<long>>().ToTable("IdentityRoleClaims", "Identity");
            builder.Entity<IdentityUserClaim<long>>().ToTable("IdentityUserClaims", "Identity");
            builder.Entity<IdentityUserLogin<long>>().ToTable("IdentityUserLogin", "Identity");
            builder.Entity<Domain.Models.Identity.IdentityRole>().ToTable("IdentityRole", "Identity");

            #endregion

            #region Users

            builder.Entity<User>().ToTable("Users", "Users")
               .HasOne(i => i.UserProfile)
               .WithOne(u => u.User)
               .HasForeignKey<UserProfile>(i => i.UserId);

            builder.Entity<UserProfile>().ToTable("UserProfiles", "Users")
                .HasOne(i => i.User)
                .WithOne(u => u.UserProfile)
                .HasForeignKey<User>(i => i.UserProfileId);

            #endregion
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }

        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }

        public DbSet<CategoryProduct> CategoryProducts { get; set; }

        /// <summary>
        /// Пользовательские профили
        /// </summary>
        public DbSet<UserProfile> UserProfiles { get; set; }
    }
}