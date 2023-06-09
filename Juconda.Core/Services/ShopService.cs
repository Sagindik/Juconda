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

        public async Task<int> GetBasketItemsCount()
        {
            var basket = await GetCurrentBasket();

            if (basket == null) return 0;

            return basket.BasketItems.Where(_ => _.Actual).Count();
        }

        public async Task<decimal> GetBasketTotalPrice()
        {
            var basket = await GetCurrentBasket();

            if (basket == null) return decimal.Zero;

            decimal? total = _context.BasketItems.Where(_ => _.BasketId == basket.Id && _.Product != null && _.Actual)
                .Select(_ => _.Count * _.Product.Price).Sum();

            return Math.Round(total ?? decimal.Zero, 2);
        }

        public async Task<int> GetBasketProductCount(int? productId)
        {
            var basket = await GetCurrentBasket();

            if (basket == null) return 1;

            var count = basket.BasketItems.FirstOrDefault(_ => _.Actual && _.ProductId == productId)?.Count;

            return count ?? 1;
        }

        public async Task<int> ChangeCountOfProduct(int count, int basketItemId)
        {
            var basket = await GetCurrentBasket();

            if (basket == null) return 1;

            var basketItem = basket.BasketItems.FirstOrDefault(_ => _.Id == basketItemId && _.Product != null);

            if (basketItem == null) return 1;

            basketItem.Count = count;

            _context.Update(basketItem);
            await _context.SaveChangesAsync();

            return count;
        }
    }
}
