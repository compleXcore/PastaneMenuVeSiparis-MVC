using PastaneMenuVeSiparis.IsKatmani;
using PastaneMenuVeSiparis.SunumKatmani.Filters;
using PastaneMenuVeSiparis.VarlikKatmani;
using PastaneMenuVeSiparis.VarlikKatmani.Enums;
using PastaneMenuVeSiparis.VeriTabaniErisimKatmani;
using System;
using System.Web.Mvc;

namespace PastaneMenuVeSiparis.SunumKatmani.Areas.Yonetim.Controllers
{
    public class UrunController : Controller
    {
        // GET: Yonetim/Urun
        [Yetki(Yetki = Yetkiler.YETKILI)]
        public ActionResult List()
        {
            using(UrunManager urunManager = new UrunManager())
            {
                return View(urunManager.ListeleKategori());
            }
        }
        [Yetki(Yetki = Yetkiler.YETKILI)]
        public ActionResult Add()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                ViewBag.Kategoriler = new SelectList(uow.KategoriRepo.GetAll(), "Id", "Ad");
                return View();
            }
        }
        [Yetki(Yetki = Yetkiler.YETKILI)]
        [HttpPost]
        public ActionResult Add(Urun item)
        {
            if (ModelState.IsValid)
            {
                using (UrunManager urunManager = new UrunManager())
                {
                    urunManager.Ekle(item);
                    return RedirectToAction("List");
                }
            }

            using (UnitOfWork uow = new UnitOfWork())
            {
                ViewBag.Kategoriler = new SelectList(uow.KategoriRepo.GetAll(), "Id", "Ad");
                return View(item);
            }
        }
        [Yetki(Yetki = Yetkiler.YETKILI)]
        public ActionResult Update(int id)
        {
            using (UrunManager urunManager = new UrunManager())
            {
                var item = urunManager.GetItem(id);
                if (item == null) throw new Exception("Ürün yok.");

                using (UnitOfWork uow = new UnitOfWork())
                {
                    ViewBag.Kategoriler = new SelectList(uow.KategoriRepo.GetAll(), "Id", "Ad");
                    return View(item);
                }
            }
        }
        [Yetki(Yetki = Yetkiler.YETKILI)]
        [HttpPost]
        public ActionResult Update(int id, Urun item)
        {
            if (ModelState.IsValid)
            {
                using (UrunManager urunManager = new UrunManager())
                {
                    urunManager.Guncelle(item);
                    return RedirectToAction("List");
                }
            }

            using (UnitOfWork uow = new UnitOfWork())
            {
                ViewBag.Kategoriler = new SelectList(uow.KategoriRepo.GetAll(), "Id", "Ad");
                return View(item);
            }
        }
        [Yetki(Yetki = Yetkiler.YETKILI)]
        public ActionResult Remove(int id)
        {
            using (UrunManager urunManager = new UrunManager())
            {
                var item = urunManager.GetItem(id);
                if (item == null) throw new Exception("Ürün Yok.");

                return View(item);
            }
        }
        [Yetki(Yetki = Yetkiler.YETKILI)]
        [HttpPost]
        public ActionResult Remove(int id, Urun item)
        {
            using (UrunManager urunManager = new UrunManager())
            {
                urunManager.Sil(id);
                return RedirectToAction("List");
            }
        }
    }
}