using Juconda.Core.Utils;
using Juconda.Domain.Models;
using Juconda.Domain.Models.Users;
using Juconda.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace Juconda.Core.Services
{
    public class ShopService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private AppDbContext _context;

        public ShopService(
            UserManager<User> userManager,
            SignInManager<User> signInManager, 
            AppDbContext context) 
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
        }

        public async Task<Basket?> GetCurrentBasket()
        {
            var sessionKey = CookieUtils.CartSession;
            if (sessionKey.Equals(Guid.Empty))
                sessionKey = Guid.NewGuid();

            CookieUtils.CartSession = sessionKey;

            var basket = _context.Baskets.FirstOrDefault(_ => _.Actual && _.KeyGuid == sessionKey);

            if (basket == null)
            {
                basket = new Basket() { KeyGuid = sessionKey };

                var user = await _userManager.GetUserAsync(_signInManager.Context.User);
                if (user != null)
                {
                    basket.User = user;
                }

                _context.Add(basket);
                await _context.SaveChangesAsync();
            }

            return basket;
        }
    }
}
