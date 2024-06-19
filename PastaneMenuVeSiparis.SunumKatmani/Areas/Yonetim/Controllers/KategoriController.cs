using PastaneMenuVeSiparis.SunumKatmani.Filters;
using PastaneMenuVeSiparis.VarlikKatmani;
using PastaneMenuVeSiparis.VarlikKatmani.Enums;
using PastaneMenuVeSiparis.VeriTabaniErisimKatmani;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PastaneMenuVeSiparis.SunumKatmani.Areas.Yonetim.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Yonetim/Kategori
        [Yetki(Yetki = Yetkiler.YETKILI)]
        public ActionResult List()
        {
            using(UnitOfWork uow = new UnitOfWork())
            {
                return View(uow.KategoriRepo.GetAll());
            }
        }
        [Yetki(Yetki = Yetkiler.YETKILI)]
        public ActionResult Add()
        {
            return View();
        }
        [Yetki(Yetki = Yetkiler.YETKILI)]
        [HttpPost]
        public ActionResult Add(Kategori item)
        {
            if (ModelState.IsValid)
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    uow.KategoriRepo.Add(item);
                    uow.Save();
                    return RedirectToAction("List");
                }
            }

            return View(item);
        }
        [Yetki(Yetki = Yetkiler.YETKILI)]
        public ActionResult Update(int id)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var item = uow.KategoriRepo.GetItem(id);
                if (item == null) throw new Exception("Kategori Yok?");

                return View(item);
            }
        }
        [Yetki(Yetki = Yetkiler.YETKILI)]
        [HttpPost]
        public ActionResult Update(int id, Kategori item)
        {
            if (ModelState.IsValid)
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    uow.KategoriRepo.Update(item);
                    uow.Save();
                    return RedirectToAction("List");
                }
            }

            return View(item);
        }
        [Yetki(Yetki = Yetkiler.YETKILI)]
        public ActionResult Remove(int id)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var item = uow.KategoriRepo.GetItem(id);
                if (item == null) throw new Exception("Kategori Yok?");

                return View(item);
            }
        }
        [Yetki(Yetki = Yetkiler.YETKILI)]
        [HttpPost]
        public ActionResult Remove(int id, Kategori item)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                uow.KategoriRepo.Remove(id);
                uow.Save();
                return RedirectToAction("List");
            }
        }
    }
}