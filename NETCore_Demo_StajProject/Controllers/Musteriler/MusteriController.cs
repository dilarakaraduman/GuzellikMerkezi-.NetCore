using Microsoft.AspNetCore.Mvc;
using NETCore_Demo_StajProject.App_Filter;
using NETCore_Demo_StajProject.DAL;
using NETCore_Demo_StajProject.Models;

namespace NETCore_Demo_StajProject.Controllers.Musteriler
{
    public class MusteriController : Controller
    {
        [LoginFilter]
        #region Kayıt Listeleme
        public IActionResult Index()
        {
            DataBaseContext db = new DataBaseContext();
            var r = db.Musteri.ToList();
            return View(r);

        }
        #endregion
        #region Yeni Kayıt

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ActionName("Create")]
        public IActionResult CreatePost(Musteri model)
        {
            try
            {
                DataBaseContext db = new DataBaseContext();
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
        [LoginFilter]
        #region Kayıt Güncelleme
        public IActionResult Edit(int id)
        {
            DataBaseContext db = new DataBaseContext();
            var r = db.Musteri.Find(id);
            return View(r);
        }
        [HttpPost]
        [ActionName("Edit")]
        public IActionResult EditPost(Musteri model)
        {
            DataBaseContext db = new DataBaseContext();
            db.Musteri.Update(model);
            db.SaveChanges();
            return View();

        }
        #endregion
        [LoginFilter]
        #region Kayıt Silme
        public IActionResult Remove(int id)
        {
            DataBaseContext db = new DataBaseContext();
            var r = db.Musteri.Find(id);
            return View(r);
        }
        [HttpPost]
        [ActionName("Remove")]
        public IActionResult RemovePost(Musteri model)
        {
            DataBaseContext db = new DataBaseContext();
            db.Musteri.Remove(model);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        #endregion
        //public IActionResult MusteriOperasyonlari(int id)
        //{
        //    ViewBag.MusteriId = id;
        //    using (DataBaseContext db = new DataBaseContext())
        //    {
        //        var result = (from pg in db.MusteriOperasyon
        //                      join g in db.Operasyonlar on pg.OperasyonId equals g.OperasyonId
        //                      join p in db.Musteri on pg.MusteriId equals p.MusteriId
        //                      where p.MusteriId == id
        //                      select new MusteriOperasyonVM()
        //                      {
        //                          Operasyonlar = g,
        //                          Musteri = p,
        //                          MusteriOperasyon = pg
        //                      }
        //                      ).ToList();

        //        return View(result);
        //    }


        //}
    }
}