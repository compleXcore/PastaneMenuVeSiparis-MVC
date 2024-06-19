using PastaneMenuVeSiparis.IsKatmani;
using PastaneMenuVeSiparis.SunumKatmani.Filters;
using PastaneMenuVeSiparis.VarlikKatmani;
using PastaneMenuVeSiparis.VarlikKatmani.Enums;
using System.Linq;
using System.Web.Mvc;

namespace PastaneMenuVeSiparis.SunumKatmani.Controllers
{
    public class SepetController : Controller
    {
        // GET: Sepet
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
                using (SiparisDetayManager siparisDetayManager = new SiparisDetayManager())
                {
                    int id = (Session["user"] as Kullanici).Id;
                    var siparis = siparisManager.GetSepetteSiparis(id);
                    if (siparis != null)
                    {
                        var siparisDetay = siparisDetayManager.SiparisDetaylar(siparis.Id);
                        return View(siparisDetay);
                    }
                    return View();
                }
            }
        }

        [Kimlik, Yetki(Yetki = Yetkiler.KULLANICI)]
        public ActionResult SepetOnayla()
        {
            if (((int)(Session["user"] as Kullanici).Yetki) >= ((int)Yetkiler.YETKILI))
            {
                return RedirectToAction("List", "Siparis", new { area = "Yonetim" });
            }
            else
            {
                using (SiparisManager siparisManager = new SiparisManager())
                {
                    int id = (Session["user"] as Kullanici).Id;
                    var siparis = siparisManager.SiparislerKullaniciVeDurum(id, Durumlar.SEPETTE);

                    if (siparis != null)
                    {
                        siparisManager.GuncelleSepet(id);
                    }
                }
            }
            return RedirectToAction("List");
        }

        [Kimlik, Yetki(Yetki = Yetkiler.KULLANICI)]
        public ActionResult UrunMiktarArttir(int id)
        {
            Kullanici kullanici = (Kullanici)Session["user"];
            if (((int)kullanici.Yetki) >= ((int)Yetkiler.YETKILI))
            {
                return RedirectToAction("List", "Siparis", new { area = "Yonetim" });
            }
            else
            {
                using (SiparisManager siparisManager = new SiparisManager())
                {
                    var siparis = siparisManager.GetSepetteSiparis(kullanici.Id);
                    if (siparis != null)
                    {
                        using (SiparisDetayManager siparisDetayManager = new SiparisDetayManager())
                        using (UrunManager urunManager = new UrunManager())
                        {
                            var siparisDetay = siparisDetayManager.SiparisDetaylar(siparis.Id);
                            Urun urun = urunManager.GetItem(id);
                            SiparisDetay siparisDetay1 = siparisDetay.FirstOrDefault(x => x.Urun.Id == urun.Id);
                            siparisDetay1.Adet++;
                            siparisDetay1.Tutar += urun.Fiyat;
                            siparis.ToplamFiyat += urun.Fiyat;
                            siparisDetayManager.Guncelle(siparisDetay1);
                            siparisManager.Guncelle(siparis);
                        }
                    }
                }
            }

            return RedirectToAction("List");
        }

        [Kimlik, Yetki(Yetki = Yetkiler.KULLANICI)]
        public ActionResult UrunMiktarAzalt(int id)
        {
            Kullanici kullanici = (Kullanici)Session["user"];
            if (((int)kullanici.Yetki) >= ((int)Yetkiler.YETKILI))
            {
                return RedirectToAction("List", "Siparis", new { area = "Yonetim" });
            }
            else
            {
                using (SiparisManager siparisManager = new SiparisManager())
                {
                    var siparis = siparisManager.GetSepetteSiparis(kullanici.Id);
                    if (siparis != null)
                    {
                        using (SiparisDetayManager siparisDetayManager = new SiparisDetayManager())
                        using (UrunManager urunManager = new UrunManager())
                        {
                            var siparisDetay = siparisDetayManager.SiparisDetaylar(siparis.Id);
                            Urun urun = urunManager.GetItem(id);
                            SiparisDetay siparisDetay1 = siparisDetay.FirstOrDefault(x => x.Urun.Id == urun.Id);
                            if(siparisDetay1.Adet == 1)
                            {
                                siparis.ToplamFiyat -= urun.Fiyat;
                                siparisDetayManager.Sil(siparisDetay1.Id);
                                siparisManager.Guncelle(siparis);
                            }
                            else
                            {
                                siparisDetay1.Adet--;
                                siparisDetay1.Tutar -= urun.Fiyat;
                                siparis.ToplamFiyat -= urun.Fiyat;
                                siparisDetayManager.Guncelle(siparisDetay1);
                                siparisManager.Guncelle(siparis);
                            }

                            siparisDetay = siparisDetayManager.SiparisDetaylar(siparis.Id);
                            if(siparisDetay.Count <= 0)
                            {
                                siparisManager.Sil(siparis.Id);
                            }
                        }
                    }
                }
            }

            return RedirectToAction("List");
        }

        [Kimlik, Yetki(Yetki = Yetkiler.KULLANICI)]
        public ActionResult Remove(int id)
        {
            Kullanici kullanici = (Kullanici)Session["user"];

            if (((int)kullanici.Yetki) >= ((int)Yetkiler.YETKILI))
            {
                return RedirectToAction("List", "Siparis", new { area = "Yonetim" });
            }
            else
            {
                using (SiparisManager siparisManager = new SiparisManager())
                using (UrunManager urunManager =  new UrunManager())
                using (SiparisDetayManager siparisDetayManager = new SiparisDetayManager())
                {
                    var siparis = siparisManager.GetSepetteSiparis(kullanici.Id);
                    if(siparis != null)
                    {
                        var siparisDetay = siparisDetayManager.SiparisDetaylar(siparis.Id);
                        if(siparisDetay.Count == 1)
                        {
                            siparisManager.Sil(siparis.Id);
                        }
                        else
                        {
                            Urun urun = urunManager.GetItem(id);
                            SiparisDetay siparisDetay1 = siparisDetay.FirstOrDefault(x => x.Urun.Id == urun.Id);
                            siparisDetayManager.Sil(siparisDetay1.Id);
                            siparis.ToplamFiyat -= urun.Fiyat;
                            siparisManager.Guncelle(siparis);
                        }
                    }
                }
            }

            return RedirectToAction("List");
        }
    }
}