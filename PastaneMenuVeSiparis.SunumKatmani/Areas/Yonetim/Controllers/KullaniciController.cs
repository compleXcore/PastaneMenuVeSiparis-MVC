using PastaneMenuVeSiparis.IsKatmani;
using PastaneMenuVeSiparis.SunumKatmani.Filters;
using PastaneMenuVeSiparis.VarlikKatmani;
using PastaneMenuVeSiparis.VarlikKatmani.Enums;
using PastaneMenuVeSiparis.VeriTabaniErisimKatmani;
using System;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Web.Mvc;

namespace PastaneMenuVeSiparis.SunumKatmani.Areas.Yonetim.Controllers
{
    public class KullaniciController : Controller
    {
        // GET: Yonetim/Kullanici
        [Kimlik, Yetki(Yetki = Yetkiler.YONETICI)]
        public ActionResult List()
        {
            using(KullaniciManager kullaniciManager = new KullaniciManager())
            {
                var count = kullaniciManager.Listele();
                return View(kullaniciManager.ListeleYetkililer());
            }
        }

        public ActionResult Login()
        {
            if (Session["user"] == null)
                return View();
            else
            {
                if((Session["user"] as Kullanici).Yetki == Yetkiler.YONETICI)
                    return RedirectToAction("Index", "DashBoard", new { area = "Yonetim" });
                else
                    return RedirectToAction("List", "Urunler", new { area = ""});
            }
        }

        public ActionResult Register()
        {
            if (Session["user"] == null)
                return View();
            else
            {
                if ((Session["user"] as Kullanici).Yetki == Yetkiler.YONETICI)
                    return RedirectToAction("Index", "DashBoard", new { area = "Yonetim" });
                else
                    return RedirectToAction("List", "Urunler", new { area = "" });
            }
        }

        [HttpPost]
        public ActionResult Register(Kullanici kullanici)
        {
            if (ModelState.IsValid)
            {
                using (KullaniciManager kullaniciManager = new KullaniciManager())
                {
                    if (kullaniciManager.KullaniciAdiKontrol(kullanici.KullaniciAdi))
                    {
                        var user = kullaniciManager.Ekle(kullanici);
                        Session["user"] = user;
                        return RedirectToAction("List", "Urunler", new { area = "" });
                    }
                    else
                    {
                        ModelState.AddModelError("", "Kullanıcı adını değiştiriniz.");
                    }
                }
            }

            return View(kullanici);
        }

        [Kimlik, Yetki(Yetki = Yetkiler.YONETICI)]
        public ActionResult Add()
        {
            var enumValues = Enum.GetValues(typeof(Yetkiler)).Cast<Yetkiler>()
                .Where(e => e != Yetkiler.KULLANICI)
                .Select(e => new SelectListItem
                {
                    Value = e.ToString(),
                    Text = e.ToString()
                });

            ViewBag.EnumOptions = enumValues;

            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Kimlik, Yetki(Yetki = Yetkiler.YONETICI)]
        public ActionResult Add(Kullanici kullanici)
        {
            if (ModelState.IsValid)
            {
                using (KullaniciManager kullaniciManager = new KullaniciManager())
                {
                    var user = kullaniciManager.Ekle(kullanici);
                    return RedirectToAction("List");
                }
            }

            var enumValues = Enum.GetValues(typeof(Yetkiler)).Cast<Yetkiler>()
                .Where(e => e != Yetkiler.KULLANICI)
                .Select(e => new SelectListItem
                {
                    Value = e.ToString(),
                    Text = e.ToString()
                });

            ViewBag.EnumOptions = enumValues;

            return View(kullanici);
        }

        [Kimlik, Yetki(Yetki = Yetkiler.YONETICI)]
        public ActionResult Update(int id)
        {
            using (KullaniciManager kullaniciManager = new KullaniciManager())
            {
                var kullanici = kullaniciManager.GetKullanici(id);
                if (kullanici == null) throw new Exception("Yetkili yok.");

                var enumValues = Enum.GetValues(typeof(Yetkiler)).Cast<Yetkiler>()
                .Where(e => e != Yetkiler.KULLANICI)
                .Select(e => new SelectListItem
                {
                    Value = e.ToString(),
                    Text = e.ToString()
                });

                ViewBag.EnumOptions = enumValues;
                return kullanici == null ? throw new Exception("") : (ActionResult)View(kullanici);
            }
        }

        [Kimlik, Yetki(Yetki = Yetkiler.YONETICI)]
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Update(Kullanici kullanici)
        {
            if (ModelState.IsValid)
            {
                using (KullaniciManager kullaniciManager = new KullaniciManager())
                {
                    kullaniciManager.Guncelle(kullanici);
                    return RedirectToAction("List");
                }
            }
            var enumValues = Enum.GetValues(typeof(Yetkiler)).Cast<Yetkiler>()
                .Where(e => e != Yetkiler.KULLANICI)
                .Select(e => new SelectListItem
                {
                    Value = e.ToString(),
                    Text = e.ToString()
                });

            ViewBag.EnumOptions = enumValues;
            return View(kullanici);
        }

        [Kimlik, Yetki(Yetki = Yetkiler.YONETICI)]
        public ActionResult Remove(int id)
        {
            using (KullaniciManager kullaniciManager = new KullaniciManager())
            {
                var item = kullaniciManager.GetKullanici(id);
                return item == null ? throw new Exception("") : (ActionResult)View(item);
            }
        }
        [Kimlik, Yetki(Yetki = Yetkiler.YONETICI)]
        [HttpPost]
        public ActionResult Remove(int id, Kullanici item)
        {
            using (KullaniciManager kullaniciManager = new KullaniciManager())
            {
                var user = kullaniciManager.GetKullanici(id);

                if (user.Id == (Session["user"] as Kullanici).Id && Session["user"] != null)
                {
                    Session.Remove("user");
                    kullaniciManager.Sil(id);
                    return RedirectToAction("List", "Urunler", new { area = "" });
                }
                else
                {
                    kullaniciManager.Sil(id);
                    return RedirectToAction("List");
                }
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Login(Kullanici kullanici)
        {
            if (kullanici != null)
            {
                using(KullaniciManager kullaniciManager = new KullaniciManager())
                {
                    if (kullaniciManager.Giris(kullanici))
                    {
                        Kullanici user = kullaniciManager.GetGirisKullanici(kullanici.KullaniciAdi, kullanici.KullaniciSifre);
                        Session["user"] = user;
                        if (user.Yetki == Yetkiler.YONETICI)
                            return RedirectToAction("Index", "Dashboard", new { area = "Yonetim" });
                        else
                            return RedirectToAction("List", "Urunler", new { area = ""});
                    }
                }
            }
            ModelState.AddModelError("", "Kullanıcı adı ya da parola hatalı!!!");
            return View(kullanici);
        }

        public ActionResult Logout()
        {
            if (Session["user"] != null)
                Session.Remove("user");
            return RedirectToAction("List", "Urunler", new { area = ""});
        }
    }
}