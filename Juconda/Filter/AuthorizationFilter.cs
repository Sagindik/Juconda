using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Juconda.Domain.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace Juconda.Filter
{
    public class AuthorizationFilter : IAuthorizationFilter
    {
        private readonly UserManager<User> _userManager;
        private List<string> Roles { get; }

        public AuthorizationFilter(
            string roles,
            UserManager<User> userManager)
        {
            _userManager = userManager;
            Roles = roles.Split(',').ToList().Select(_ => _.Trim()).ToList();
        }

        public async void OnAuthorization(AuthorizationFilterContext context)
        {
            //try
            //{
            //    if (_currentUserService.UserId == null)
            //    {
            //        context.Result = new UnauthorizedResult();
            //        return;
            //    }

            //    var user = _userManager.FindByIdAsync(_currentUserService.UserId.ToString()).Result;
            //    if (user == null)
            //    {
            //        context.Result = new UnauthorizedResult();
            //        return;
            //    }

            //    if (user.State != null && user.State.Code == User.StateCodeArchived)
            //    {
            //        context.Result = new UnauthorizedObjectResult("Учетная запись не активна");
            //        return;
            //    }

            //    var permissions = _currentUserService.Permissions;
            //    var allowAccess = true;
            //    foreach (var role in Roles)
            //    {
            //        if (!permissions.Contains(permission))
            //            allowAccess = false;
            //    }

            //    if (!allowAccess)
            //    {
            //        context.Result = new UnauthorizedObjectResult("Нет прав");
            //        return;
            //    }

            //}
            //catch (Exception ex)
            //{
            //    context.Result = new UnauthorizedObjectResult(ex.Message);
            //}

            //context.Result = new RedirectToRouteResult(
            //       new RouteValueDictionary(
            //        new { action = "Login", Controller = "Home" }));
        }
    }
}
