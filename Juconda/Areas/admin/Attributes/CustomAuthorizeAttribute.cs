using Microsoft.AspNetCore.Mvc;

namespace Juconda.Areas.admin.Attributes
{
    public class CustomAuthorizeAttribute : TypeFilterAttribute
    {
        public CustomAuthorizeAttribute() : base(typeof(CustomAuthorizeFilter))
        {
        }
    }
}
