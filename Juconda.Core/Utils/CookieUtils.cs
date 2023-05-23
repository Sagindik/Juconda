using Microsoft.AspNetCore.Http;

namespace Juconda.Core.Utils
{
    public class CookieUtils
    {
        private const string CartSessionKey = "CartSessionKey";

        public static Guid CartSession
        {
            get
            {
                var httpContext = new HttpContextAccessor().HttpContext;
                if (httpContext.Request.Cookies.TryGetValue(CartSessionKey, out var cookieValue))
                {
                    if (Guid.TryParse(cookieValue, out var sessionUid))
                    {
                        return sessionUid;
                    };
                }

                return Guid.Empty;
            }
            set
            {
                var httpContext = new HttpContextAccessor().HttpContext;
                if (httpContext.Response.Headers.ContainsKey("Set-Cookie"))
                {
                    httpContext.Response.Headers["Set-Cookie"] = $"{CartSessionKey}={value}; Expires={DateTime.Now.AddDays(365)}; Path=/;";
                }
                else
                {
                    httpContext.Response.Headers.Append("Set-Cookie", $"{CartSessionKey}={value}; Expires={DateTime.Now.AddDays(365)}; Path=/;");
                }
            }
        }
    }
}
