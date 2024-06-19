using PastaneMenuVeSiparis.VarlikKatmani.Enums;
using PastaneMenuVeSiparis.VarlikKatmani;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PastaneMenuVeSiparis.SunumKatmani.Filters
{
    public class YetkiAttribute : FilterAttribute, IAuthorizationFilter
    {
        public Yetkiler Yetki { get; set; }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Session["user"] != null)
            {
                Kullanici user = filterContext.HttpContext.Session["user"] as Kullanici;
                if (user != null)
                {
                    if (((int)user.Yetki) >= ((int)Yetki))
                    {
                        return;
                    }
                }
            }
            filterContext.Result = new ViewResult()
            {
                ViewName = "YetkisizErisim"
            };
        }
    }
}