using PastaneMenuVeSiparis.VarlikKatmani;
using PastaneMenuVeSiparis.VeriTabaniErisimKatmani.DataBaseContext;
using PastaneMenuVeSiparis.VeriTabaniErisimKatmani.Repositories;
using PastaneMenuVeSiparis.VeriTabaniErisimKatmani.Repositories.Base;
using System;

namespace PastaneMenuVeSiparis.VeriTabaniErisimKatmani
{
    public class UnitOfWork : IDisposable
    {
        private AppDbContext _appDbContext;
        private IRepository<Kategori> kategoriRepo;
        private IKullaniciRepository kullaniciRepo;
        private ISiparisRepository siparisRepo;
        private IUrunRepository urunRepo;
        private ISiparisDetayRepository siparisDetayRepo;

        public IRepository<Kategori> KategoriRepo
        {
            get
            {
                if (kategoriRepo == null)
                    kategoriRepo = new Repository<Kategori>(_appDbContext);
                return kategoriRepo;
            }
        }
        public IKullaniciRepository KullaniciRepo
        {
            get
            {
                if(kullaniciRepo == null)
                    kullaniciRepo = new KullaniciRepository(_appDbContext);
                return kullaniciRepo;
            }
        }
        public ISiparisRepository SiparisRepo
        {
            get
            {
                if(siparisRepo == null)
                    siparisRepo = new SiparisRepository(_appDbContext);
                return siparisRepo;
            }
        }
        public IUrunRepository UrunRepo
        {
            get
            {
                if(urunRepo == null)
                    urunRepo = new UrunRepository(_appDbContext);
                return urunRepo;
            }
        }
        public ISiparisDetayRepository SiparisDetayRepo
        {
            get
            {
                if (siparisDetayRepo == null)
                    siparisDetayRepo = new SiparisDetayRepository(_appDbContext);
                return siparisDetayRepo;
            }
        }

        public UnitOfWork()
        {
            _appDbContext = new AppDbContext();
        }
        public void Save()
        {
            _appDbContext.SaveChanges();
        }
        public void Dispose()
        {
            _appDbContext?.Dispose();
            kategoriRepo?.Dispose();
            kullaniciRepo?.Dispose();
            siparisRepo?.Dispose();
            urunRepo?.Dispose();
            siparisDetayRepo?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
