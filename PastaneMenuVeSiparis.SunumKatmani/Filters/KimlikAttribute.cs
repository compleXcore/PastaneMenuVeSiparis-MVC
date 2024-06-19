using PastaneMenuVeSiparis.VarlikKatmani;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;

namespace PastaneMenuVeSiparis.SunumKatmani.Filters
{
    public class KimlikAttribute : FilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            if (filterContext.HttpContext.Session["user"] != null)
            {
                Kullanici user = filterContext.HttpContext.Session["user"] as Kullanici;
                if (user != null)
                {
                    return;
                }
            }

            filterContext.Result = new HttpUnauthorizedResult();
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            if (filterContext.Result != null && filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                        {"area", "Yonetim" },
                        {"controller", "Kullanici" },
                        {"action", "Login" }
                    }
                    );
            }
        }
    }
}