using PastaneMenuVeSiparis.IsKatmani;
using PastaneMenuVeSiparis.SunumKatmani.Filters;
using PastaneMenuVeSiparis.VarlikKatmani;
using PastaneMenuVeSiparis.VarlikKatmani.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PastaneMenuVeSiparis.SunumKatmani.Controllers
{
    public class SiparislerimController : Controller
    {
        // GET: Siparislerim
        [Kimlik, Yetki(Yetki = Yetkiler.KULLANICI)]
        public ActionResult List()
        {
            if (((int)(Session["user"] as Kullanici).Yetki) >= ((int)Yetkiler.YETKILI))
            {
                return RedirectToAction("List", "Siparis", new { area = "Yonetim" });
            }
            else
            {
                using (SiparisManager siparisManager = new SiparisManager())
                {
                    var userid = (Session["user"] as Kullanici).Id;
                    var list = siparisManager.SiparislerKullaniciVeDurum(userid, Durumlar.TESLIMEDILDI);
                    return View(list);
                }
            }
        }
    }
}