using Juconda.Domain.Models;
using Juconda.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Juconda.Controllers
{
    public class ShoppingCartController : Controller
    {
        public string ShoppingCartId { get; set; }

        private AppDbContext _context { get; set; }

        public const string CartSessionKey = "CartId";

        public ShoppingCartController(AppDbContext context)
        {
            _context = context;
        }

        public void AddToCart(int id)
        {
            // Retrieve the product from the database.           
            ShoppingCartId = GetCartId();

            var cartItem = _context.CartItems.SingleOrDefault(
                c => c.CartId == ShoppingCartId
                && c.ProductId == id);

            if (cartItem == null)
            {
                // Create a new cart item if no cart item exists.                 
                cartItem = new CartItem
                {
                    ProductId = id,
                    CartId = ShoppingCartId,
                    Product = _context.Products.FirstOrDefault(p => p.Id == id),
                    Quantity = 1
                };

                _context.CartItems.Add(cartItem);
            }
            else
            {
                // If the item does exist in the cart,                  
                // then add one to the quantity.                 
                cartItem.Quantity++;
            }
            _context.SaveChanges();
        }

        public int RemoveFromCart(int id)
        {
            // Get the cart
            var cartItem = _context.CartItems.Single(
                cart => cart.CartId == ShoppingCartId
                && cart.ProductId == id);

            int itemCount = 0;

            if (cartItem != null)
            {
                if (cartItem.Quantity > 1)
                {
                    cartItem.Quantity--;
                    itemCount = cartItem.Quantity;
                }
                else
                {
                    _context.CartItems.Remove(cartItem);
                }
                // Save changes
                _context.SaveChanges();
            }
            return itemCount;
        }

        public void EmptyCart()
        {
            var cartItems = _context.CartItems.Where(
                cart => cart.CartId == ShoppingCartId);

            foreach (var cartItem in cartItems)
            {
                _context.CartItems.Remove(cartItem);
            }
            // Save changes
            _context.SaveChanges();
        }

        public List<CartItem> GetCartItems()
        {
            ShoppingCartId = GetCartId();

            return _context.CartItems.Where(
                c => c.CartId == ShoppingCartId).ToList();
        }

        public int GetCount()
        {
            // Get the count of each item in the cart and sum them up
            int? count = (from cartItems in _context.CartItems
                          where cartItems.CartId == ShoppingCartId
                          select (int?)cartItems.Quantity).Sum();
            // Return 0 if all entries are null
            return count ?? 0;
        }

        public decimal GetTotal()
        {
            // Multiply album price by count of that album to get 
            // the current price for each of those albums in the cart
            // sum all album price totals to get the cart total
            decimal? total = _context.CartItems.Where(_ => _.CartId == ShoppingCartId && _.Product != null)
                .Select(_ => _.Quantity * _.Product.Price).Sum();

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

        public string GetCartId()
        {
            if (HttpContext.Session.GetString(CartSessionKey) == null)
            {
                if (!string.IsNullOrWhiteSpace(HttpContext?.User?.Identity?.Name))
                {
                    HttpContext.Session.SetString(CartSessionKey, HttpContext?.User?.Identity?.Name ?? "");
                }
                else
                {
                    // Generate a new random GUID using System.Guid class
                    Guid tempCartId = Guid.NewGuid();
                    // Send tempCartId back to client as a cookie
                    HttpContext.Session.SetString(CartSessionKey, tempCartId.ToString());
                }
            }

            return HttpContext?.Session?.GetString(CartSessionKey) ?? string.Empty;
        }
    }
}
