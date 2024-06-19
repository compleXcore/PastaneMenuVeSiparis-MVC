using PastaneMenuVeSiparis.VarlikKatmani;
using PastaneMenuVeSiparis.VeriTabaniErisimKatmani.DataBaseContext;
using PastaneMenuVeSiparis.VeriTabaniErisimKatmani.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace PastaneMenuVeSiparis.VeriTabaniErisimKatmani.Repositories
{
    public class UrunRepository : Repository<Urun>, IUrunRepository
    {
        public UrunRepository(AppDbContext context) : base(context)
        {
        }

        public ICollection<Urun> GetAllWithKategori()
        {
            return context.Urunler.Include(x => x.Kategori).ToList();
        }

        public Urun GetItemWithKategori(int id)
        {
            return context.Urunler.Include(x => x.Kategori).FirstOrDefault(x => x.Id == id);
        }

        public ICollection<Urun> Search(string text)
        {
            return context.Urunler.Include(x => x.Kategori)
                .Where(
                x => x.Ad.Contains(text) ||
                     x.Aciklama.Contains(text) ||
                     x.Kategori.Ad.Contains(text) ||
                     x.Fiyat.ToString().Contains(text)
                     ).ToList();
        }
    }
}
