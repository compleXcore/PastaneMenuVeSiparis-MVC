using PastaneMenuVeSiparis.VarlikKatmani;
using PastaneMenuVeSiparis.VarlikKatmani.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastaneMenuVeSiparis.VeriTabaniErisimKatmani.Repositories.Base
{
    public interface IKullaniciRepository : IRepository<Kullanici>
    {
        Kullanici GetLoginKullanici(string eposta, string parola);
        bool Login(Kullanici kullanici);
        bool GetKullaniciAdiControl(string kullanici);
        ICollection<Kullanici> GetAllYetkiliAndYonetici();
        int GetAllKullaniciCount();
    }
}