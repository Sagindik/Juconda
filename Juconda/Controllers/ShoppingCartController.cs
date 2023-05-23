using AutoMapper;
using Juconda.Core.Services;
using Juconda.Domain.Models;
using Juconda.Infrastructure;
using Juconda.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Juconda.Controllers
{
    public class ShoppingCartController : Controller
    {
        private AppDbContext _context;
        private IMapper _mapper;
        private ShopService _shopService;

        public string ShoppingCartId { get; set; }
        public const string CartSessionKey = "CartId";

        public ShoppingCartController(AppDbContext context, IMapper mapper, ShopService shopService)
        {
            _context = context;
            _mapper = mapper;
            _shopService = shopService;
        }

        public async Task<IActionResult> Index()
        {
            var basket = await _shopService.GetCurrentBasket();

            if (basket == null)
                return View(new List<BasketItemViewModel>());

            var basketItems = _context.BasketItems.Where(_ => _.Actual && _.BasketId == basket.Id).ToList();

            var models = _mapper.Map<List<BasketItemViewModel>>(basketItems);

            return View(models);
        }

        public async Task<IActionResult> AddToCart(int productId, int productCount)
        {
            var basket = await _shopService.GetCurrentBasket();

            if (basket == null)
                return NotFound();

            var basketItem = basket.BasketItems.FirstOrDefault(_ => _.Actual && _.ProductId == productId);

            if (basketItem == null)
            {
                basketItem = new BasketItem
                {
                    ProductId = productId,
                    Product = _context.Products.FirstOrDefault(p => p.Id == productId),
                    Count = productCount
                };

                _context.BasketItems.Add(basketItem);
            }
            else
            {
                basketItem.Count = productCount;
                _context.BasketItems.Update(basketItem);
            }

            _context.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public ActionResult RemoveFromCart(int basketItemId)
        {
            var basketItem = _context.BasketItems.FirstOrDefault(
                cart => cart.Id == basketItemId);

            if (basketItem == null)
                return NotFound();

            basketItem.Actual = false;

            _context.Update(basketItem);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> RemoveAllBasketItems()
        {
            var basket = await _shopService.GetCurrentBasket();

            if (basket == null)
                return View(new List<BasketItemViewModel>());

            var basketItems = _context.BasketItems.Where(_ => _.Actual && _.BasketId == basket.Id).ToList();
            basketItems.ForEach(_ => _.Actual = false);

            _context.UpdateRange(basketItems);
            _context.SaveChanges();

            return Ok();
        }

        //public void EmptyCart()
        //{
        //    var cartItems = _context.BasketItems.Where(
        //        cart => cart.CartId == ShoppingCartId);

        //    foreach (var cartItem in cartItems)
        //    {
        //        _context.BasketItems.Remove(cartItem);
        //    }
        //    // Save changes
        //    _context.SaveChanges();
        //}

        //public List<BasketItem> GetCartItems()
        //{
        //    ShoppingCartId = GetCartId();

        //    return _context.BasketItems.Where(
        //        c => c.CartId == ShoppingCartId).ToList();
        //}

        ////public int GetCount()
        ////{
        ////    // Get the count of each item in the cart and sum them up
        ////    int? count = (from cartItems in _context.CartItems
        ////                  where cartItems.CartId == ShoppingCartId
        ////                  select (int?)cartItems.Count).Sum();
        ////    // Return 0 if all entries are null
        ////    return count ?? 0;
        ////}

        public async Task<decimal> GetTotal()
        {
            var basket = await _shopService.GetCurrentBasket();

            decimal? total = _context.BasketItems.Where(_ => _.BasketId == basket.Id && _.Product != null)
                .Select(_ => _.Count * _.Product.Price).Sum();

            return total ?? decimal.Zero;
        }

        //public int CreateOrder(Order order)
        //{
        //    decimal orderTotal = 0;

        //    var cartItems = GetCartItems();
        //    // Iterate over the items in the cart, 
        //    // adding the order details for each
        //    foreach (var item in cartItems)
        //    {
        //        var orderDetail = new OrderDetail
        //        {
        //            AlbumId = item.AlbumId,
        //            OrderId = order.OrderId,
        //            UnitPrice = item.Album.Price,
        //            Quantity = item.Count
        //        };
        //        // Set the order total of the shopping cart
        //        orderTotal += (item.Count * item.Album.Price);

        //        _context.OrderDetails.Add(orderDetail);

        //    }
        //    // Set the order's total to the orderTotal count
        //    order.Total = orderTotal;

        //    // Save the order
        //    _context.SaveChanges();
        //    // Empty the shopping cart
        //    EmptyCart();
        //    // Return the OrderId as the confirmation number
        //    return order.OrderId;
        //}

        // When a user has logged in, migrate their shopping cart to
        // be associated with their username
        //public void MigrateCart(string userName)
        //{
        //    var shoppingCart = _context.CartItems.Where(
        //        c => c.CartId == ShoppingCartId);

        //    foreach (var item in shoppingCart)
        //    {
        //        item.CartId = userName;
        //    }
        //    _context.SaveChanges();
        //}

        //public string GetCartId()
        //{
        //    if (HttpContext.Session.GetString(CartSessionKey) == null)
        //    {
        //        if (!string.IsNullOrWhiteSpace(HttpContext?.User?.Identity?.Name))
        //        {
        //            HttpContext.Session.SetString(CartSessionKey, HttpContext?.User?.Identity?.Name ?? "");
        //        }
        //        else
        //        {
        //            // Generate a new random GUID using System.Guid class
        //            Guid tempCartId = Guid.NewGuid();
        //            // Send tempCartId back to client as a cookie
        //            HttpContext.Session.SetString(CartSessionKey, tempCartId.ToString());
        //        }
        //    }

        //    return HttpContext?.Session?.GetString(CartSessionKey) ?? string.Empty;
        //}
    }
}
