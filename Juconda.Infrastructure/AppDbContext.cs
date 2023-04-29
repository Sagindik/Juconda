using Juconda.Domain.Models;
using Microsoft.AspNetCore.Identity;
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
    }
}