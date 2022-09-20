using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NETCore_Demo_StajProject.App_Filter;
using NETCore_Demo_StajProject.DAL;
using NETCore_Demo_StajProject.Models;
using NETCore_Demo_StajProject.Views;
using System.Diagnostics;
using System.Text.Json;

namespace NETCore_Demo_StajProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.FirmaAdi = "Düzce Üniversitesi";
            ViewData["Adres"] = "Düzce-Konuralp";
            TempData["Telefon"] = "123 456 78 98";
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Hakkimizda()
        {
            return View();
        }
        public IActionResult Iletisim()
        {
            return View();
        }
        #region Yeni Kayıt

        public IActionResult Kayit()
        {
            return View();
        }
        [HttpPost]
        [ActionName("Kayit")]
        public IActionResult CreatePost(DAL.Musteri model)
        {
            DataBaseContext db = new DataBaseContext();
            try
            {
                db.Musteri.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return View();
        }
        #endregion
  
        public IActionResult Giris()
        {
            return View();
        }
        [HttpPost]
        [ActionName("Giris")]
        public IActionResult GirisPost(string email,string parola)
        {
            
            using (DataBaseContext db = new DataBaseContext())
            {
                var resultM = (from m in db.Musteri
                              where m.MusteriMail == email && m.MusteriParola == parola
                              select m).SingleOrDefault();
                var resultY = (from p in db.Personel
                               where p.PersonelMail == "meliscay@gmail.com" && p.PersonelParola == "127985156"
                               select p).SingleOrDefault(); ;
                var resultP = (from p in db.Personel
                               where p.PersonelMail == email && p.PersonelParola == parola
                               select p).SingleOrDefault(); ;
 

                if (resultM != null)
                {
                    var nm = JsonSerializer.Serialize(resultM);

                    HttpContext.Session.SetString(ProgramUtility.musteriSession,nm);
                    //musteri sayfası yönlndir
                    return RedirectToAction("Index", "Randevu");

                }
                if (resultY != null)
                {
                    var mn = JsonSerializer.Serialize(resultY);

                    HttpContext.Session.SetString(ProgramUtility.personelSession, mn);
                    return RedirectToAction("AdminAnasayfa", "Home");

                }
                if (resultP != null) {
                    var mn = JsonSerializer.Serialize(resultP);

                    HttpContext.Session.SetString(ProgramUtility.personelSession, mn);
                    return RedirectToAction("PersonelAnasayfa", "Home");

                }


                //kullanici bulunamadıgı zaman kodların
                ViewBag.Mesaj = "Kullanıcı adı sifre hatalı";

                return RedirectToAction("Giris", "Home");
            }


        }
        [LoginFilter]
        public IActionResult PersonelAnasayfa()
        {
            ViewBag.FirmaAdi = "Düzce Üniversitesi";
            ViewData["Adres"] = "Düzce-Konuralp";
            TempData["Telefon"] = "123 456 78 98";
            return View();
        }
        [LoginFilter]
       
        public IActionResult AdminAnasayfa()
        {
            ViewBag.FirmaAdi = "Düzce Üniversitesi";
            ViewData["Adres"] = "Düzce-Konuralp";
            TempData["Telefon"] = "123 456 78 98";
            return View();
        }
        [LoginFilter]
        public IActionResult Cikis()
        {
            HttpContext.Session.Remove("musteriSession");
            ViewBag.Mesaj = "Oturum Sonlandırıldı.";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}