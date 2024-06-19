using PastaneMenuVeSiparis.IsKatmani;
using PastaneMenuVeSiparis.SunumKatmani.Filters;
using PastaneMenuVeSiparis.VarlikKatmani;
using PastaneMenuVeSiparis.VarlikKatmani.Enums;
using System;
using System.Web.Mvc;

namespace PastaneMenuVeSiparis.SunumKatmani.Areas.Yonetim.Controllers
{
    public class SiparisController : Controller
    {
        // GET: Yonetim/Siparis
        [Kimlik, Yetki(Yetki = Yetkiler.YETKILI)]
        public ActionResult List()
        {
            using(SiparisManager siparisManager = new SiparisManager())
            {
                return View(siparisManager.SiparislerWithKullanici());
            }
        }
        [Kimlik, Yetki(Yetki = Yetkiler.YETKILI)]
        public ActionResult Remove(int id)
        {
            using (SiparisManager siparisManager = new SiparisManager())
            {
                var item = siparisManager.GetSiparisWithKullanici(id);
                return item == null ? throw new Exception("Öyle bir sipariş yok.") : (ActionResult)View(item);
            }
        }
        [Kimlik, Yetki(Yetki = Yetkiler.YETKILI)]
        [HttpPost]
        public ActionResult Remove(int id, Siparis item)
        {
            using (SiparisManager siparisManager = new SiparisManager())
            {
                siparisManager.Sil(id);
                return RedirectToAction("List");
            }
        }
    }
}