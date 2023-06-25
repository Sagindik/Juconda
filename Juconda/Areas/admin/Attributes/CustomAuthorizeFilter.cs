using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Juconda.Areas.admin.Attributes
{

    public sealed class CustomAuthorizeFilter : IAuthorizationFilter
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomAuthorizeFilter(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!(_httpContextAccessor?.HttpContext?.User?.Identity?.IsAuthenticated ?? false))
            {
                context.Result = new RedirectResult("~/admin/Account/Login");
            }
        }
    }
}
