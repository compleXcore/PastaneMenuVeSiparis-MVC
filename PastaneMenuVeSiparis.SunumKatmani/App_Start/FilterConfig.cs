using PastaneMenuVeSiparis.SunumKatmani.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PastaneMenuVeSiparis.SunumKatmani.App_Start
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HataAttribute());
        }
    }
}