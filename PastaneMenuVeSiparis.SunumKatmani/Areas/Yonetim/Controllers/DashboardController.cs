using PastaneMenuVeSiparis.SunumKatmani.Filters;
using PastaneMenuVeSiparis.VarlikKatmani.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PastaneMenuVeSiparis.SunumKatmani.Areas.Yonetim.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Yonetim/Dashboard
        [Yetki(Yetki = Yetkiler.YONETICI)]
        public ActionResult Index()
        {
            return View();
        }
    }
}