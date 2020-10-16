using Microsoft.Owin.Security.OAuth;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace TeduShop.Web.App_Start.Authorize
{
    public class APIAuthorize : AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext context)
        {
            //var currentIdentity = actionContext.RequestContext.Principal.Identity;
            //var userName = currentIdentity.Name;
            //using (var context = new DataContext())
            //{
            //    var userStore = new UserStore<AppUser>(context);
            //    var userManager = new UserManager<AppUser>(userStore);
            //    var user = userManager.FindByName(userName);

            //    if (user == null)
            //        return false;

            //    foreach (var role in permissionActions)
            //        if (!userManager.IsInRole(user.Id, Convert.ToString(role)))
            //            return false;

            //    return true;
            //}
            return true;
        }
    }
}