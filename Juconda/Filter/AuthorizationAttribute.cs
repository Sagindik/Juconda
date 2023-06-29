using Microsoft.AspNetCore.Mvc;

namespace Juconda.Filter
{
    public class AuthorizationAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="permissions"></param>
        public AuthorizationAttribute(string roles) : base(typeof(AuthorizationFilter))
        {
            Arguments = new object[] { roles };
        }
    }
}
