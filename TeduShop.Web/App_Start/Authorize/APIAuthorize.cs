using System.Web.Http;
using System.Web.Http.Controllers;

namespace TeduShop.Web.App_Start.Authorize
{
    public class ApiAuthorize : AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext context)
        {
            if (!base.IsAuthorized(context)) return false;
            var currentIdentity = context.RequestContext.Principal.Identity;
            var n = context.Request.Properties.GetEnumerator();
            var userName = currentIdentity.Name;
            var headers = context.Request.Headers;
            var h = context.RequestContext.ClientCertificate;
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
            return false;
        }
    }
}