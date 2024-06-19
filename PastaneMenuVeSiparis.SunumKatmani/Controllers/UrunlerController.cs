using PastaneMenuVeSiparis.IsKatmani;
using PastaneMenuVeSiparis.SunumKatmani.Filters;
using PastaneMenuVeSiparis.VarlikKatmani;
using PastaneMenuVeSiparis.VarlikKatmani.Enums;
using System;
using System.Linq;
using System.Web.Mvc;

namespace PastaneMenuVeSiparis.SunumKatmani.Controllers
{
    public class UrunlerController : Controller
    {
        // GET: Urunler
        public ActionResult List(string aranan = null)
        {
            if (Session["user"] != null && ((int)(Session["user"] as Kullanici).Yetki) >= ((int)Yetkiler.YETKILI))
            {
                return RedirectToAction("List", "Siparis", new { area = "Yonetim" });
            }
            else
            {
                using (UrunManager urunManager = new UrunManager())
                {
                    if (String.IsNullOrWhiteSpace(aranan))
                        return View(urunManager.ListeleKategori());
                    else
                        return View(urunManager.Arama(aranan.Trim()));
                }
            }
        }

        [Kimlik]
        public ActionResult SepeteEkle(int id)
        {
            Kullanici kullanici = (Kullanici)Session["user"];

            if (((int)(Session["user"] as Kullanici).Yetki) >= ((int)Yetkiler.YETKILI))
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
                        using(SiparisDetayManager siparisDetayManager =  new SiparisDetayManager())
                        using(UrunManager urunManager =  new UrunManager())
                        {
                            var siparisDetay = siparisDetayManager.SiparisDetaylar(siparis.Id);
                            Urun urun = urunManager.GetItem(id);
                            SiparisDetay siparisDetay1 = siparisDetay.FirstOrDefault(x => x.Urun.Id == urun.Id);
                            if (siparisDetay1 == null)
                            {
                                siparisDetayManager.Ekle(new SiparisDetay
                                {
                                    UrunId = id,
                                    Adet = 1,
                                    Tutar = urun.Fiyat,
                                    SiparisId = siparis.Id
                                });
                                siparis.ToplamFiyat += urun.Fiyat;
                                siparisManager.Guncelle(siparis);
                            }
                            else
                            {
                                siparisDetay1.Adet++;
                                siparisDetay1.Tutar += urun.Fiyat;
                                siparis.ToplamFiyat += urun.Fiyat;
                                siparisDetayManager.Guncelle(siparisDetay1);
                                siparisManager.Guncelle(siparis);
                            }
                        }
                    }
                    else
                    {
                        using (UrunManager urunManager = new UrunManager())
                        using (SiparisDetayManager siparisDetayManager = new SiparisDetayManager())
                        {
                            var urun = urunManager.GetItem(id);
                            Siparis item = siparisManager.Ekle(new Siparis
                            {
                                KullaniciId = kullanici.Id,
                                ToplamFiyat = urun.Fiyat,
                                Tarih = DateTime.Now
                            });

                            siparisDetayManager.Ekle(new SiparisDetay
                            {
                                UrunId = id,
                                Adet = 1,
                                Tutar = urun.Fiyat,
                                SiparisId = item.Id
                            });
                            siparisManager.Guncelle(item);
                        }
                    }
                }
            }

            return RedirectToAction("List");
        }
    }
}