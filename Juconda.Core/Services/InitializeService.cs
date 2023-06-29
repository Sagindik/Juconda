using Juconda.Domain.Models;
using Juconda.Domain.Models.Users;
using Juconda.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace Juconda.Core.Services
{
    public class InitializeService
    {
        private AppDbContext _context;
        private UserManager<User> _userManager;

        public InitializeService(AppDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;

            if (!_context.Products.Any())
            {
                var products = new List<Product>()
                {
                    //new()
                    //{
                    //    Name = "Помидор",
                    //    Price = 99,
                    //    Description = "Самый лучший помидор",
                    //    IsBestseller = true,
                    //    Image = "images/2fc5fa6cac7a778c15f7b1b61a15cb77.jpg",
                    //    Count = 1,
                    //},
                    //new()
                    //{
                    //    Name = "Огурец",
                    //    Price = 23,
                    //    Description = "Самий не лучший помидор",
                    //    Image = "images/2fc5fa6cac7a778c15f7b1b61a15cb77.jpg",
                    //    Count = 1,
                    //},
                    //new()
                    //{
                    //    Name = "памела",
                    //    Price = 99,
                    //    Description = "Самый лучший помидор",
                    //    IsBestseller = true,
                    //    Image = "images/2fc5fa6cac7a778c15f7b1b61a15cb77.jpg",
                    //    Count = 1,
                    //},
                    //new()
                    //{
                    //    Name = "амела",
                    //    Price = 23,
                    //    Description = "Самий не лучший помидор",
                    //    Image = "images/2fc5fa6cac7a778c15f7b1b61a15cb77.jpg",
                    //    Count = 1,
                    //},
                    //new()
                    //{
                    //    Name = "мама я поела",
                    //    Price = 99,
                    //    Description = "Самый лучший помидор",
                    //    Image = "images/2fc5fa6cac7a778c15f7b1b61a15cb77.jpg",
                    //    Count = 1,
                    //},
                    //new()
                    //{
                    //    Name = "Огурец",
                    //    Price = 23,
                    //    Description = "Самий не лучший помидор",
                    //    IsBestseller = true,
                    //    Image = "images/2fc5fa6cac7a778c15f7b1b61a15cb77.jpg",
                    //    Count = 1,
                    //},
                    //new()
                    //{
                    //    Name = "Помидор",
                    //    Price = 99,
                    //    Description = "Самый лучший помидор",
                    //    Image = "images/2fc5fa6cac7a778c15f7b1b61a15cb77.jpg",
                    //    Count = 1,
                    //},
                    //new()
                    //{
                    //    Name = "Огурец",
                    //    Price = 23,
                    //    Description = "Самий не лучший помидор",
                    //    IsBestseller = true,
                    //    Image = "images/2fc5fa6cac7a778c15f7b1b61a15cb77.jpg",
                    //    Count = 1,
                    //},
                    //new()
                    //{
                    //    Name = "Помидор",
                    //    Price = 99,
                    //    Description = "Самый лучший помидор",
                    //    IsBestseller = true,
                    //    Image = "images/2fc5fa6cac7a778c15f7b1b61a15cb77.jpg",
                    //    Count = 1,
                    //},
                    //new()
                    //{
                    //    Name = "Огурец",
                    //    Price = 23,
                    //    Description = "Самий не лучший помидор",
                    //    IsBestseller = true,
                    //    Image = "images/2fc5fa6cac7a778c15f7b1b61a15cb77.jpg",
                    //    Count = 1,
                    //},
                    //new()
                    //{
                    //    Name = "Помидор",
                    //    Price = 99,
                    //    Description = "Самый лучший помидор",
                    //    Image = "images/2fc5fa6cac7a778c15f7b1b61a15cb77.jpg",
                    //    Count = 1,
                    //},
                    //new()
                    //{
                    //    Name = "Огурец",
                    //    Price = 23,
                    //    Description = "Самий не лучший помидор",
                    //    IsBestseller = true,
                    //    Image = "images/2fc5fa6cac7a778c15f7b1b61a15cb77.jpg",
                    //    Count = 1,
                    //},
                };

                _context.Products.AddRange(products);
            }

            if (!_context.Users.Any()) 
            {
                var user = new User() { Email = "admin@a.a", UserName = "admin" };
                var userProfile = new UserProfile()
                {
                    User = user,
                    FirstName = "Имя",
                    MiddleName = "Отчество",
                    LastName = "Фамилия",
                    Address = "Планета Земля"
                };
                user.UserProfile = userProfile;

                _userManager.CreateAsync(user, "admin123").Wait();
            }

            _context.SaveChanges();
        }
    }
}
