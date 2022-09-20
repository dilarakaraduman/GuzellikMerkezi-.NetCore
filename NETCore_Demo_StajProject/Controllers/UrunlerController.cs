using Microsoft.AspNetCore.Mvc;
using NETCore_Demo_StajProject.Models;
namespace NETCore_Demo_StajProject.Controllers
{
    public class UrunlerController : Controller
    {
        public IActionResult Index()
        {
            List<Urun> urunler = new List<Urun>();
            for(int i = 0; i < 30; i++)
            {
                Urun urun = new Urun();
                urun.Id = i;
                urun.Ad = $"{i}.urun";
                urun.Fiyat = i * 100;
                urunler.Add(urun);

            }
            return View(urunler);
        }
    }
}
