using Microsoft.AspNetCore.Mvc;
using NETCore_Demo_StajProject.App_Filter;
using NETCore_Demo_StajProject.DAL;
using NETCore_Demo_StajProject.Models;

namespace NETCore_Demo_StajProject.Controllers.Personeller
{
    public class PersonelController : Controller
    {
        [LoginFilter]
        #region Listeleme
        public IActionResult Index()
        {
            DataBaseContext db = new DataBaseContext();
            var r = db.Personel.ToList();
            return View(r);

        }
        #endregion
        [LoginFilter]
        #region Yeni Kayıt
        [LoginFilter]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ActionName("Create")]
        public IActionResult CreatePost(Personel model)
        {
            try
            {

                DataBaseContext db = new DataBaseContext();
                db.Personel.Add(model);
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
            var r = db.Personel.Find(id);
            return View(r);
        }
        [HttpPost]
        [ActionName("Edit")]
        public IActionResult EditPost(Personel model)
        {
            DataBaseContext db = new DataBaseContext();
            db.Personel.Update(model);
            db.SaveChanges();
            return View();

        }
        #endregion
        [LoginFilter]
        #region Kayıt Silme
        public IActionResult Remove(int id)
        {
            DataBaseContext db = new DataBaseContext();
            var r = db.Personel.Find(id);
            return View(r);
        }
        [HttpPost]
        [ActionName("Remove")]
        public IActionResult RemovePost(Personel model)
        {
            DataBaseContext db = new DataBaseContext();
            db.Personel.Remove(model);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        #endregion
        [LoginFilter]
        #region PersonelGorevleri
        public IActionResult PersonelGorevleri(int id)
        {
            ViewBag.PersonelId = id;
            using (DataBaseContext db = new DataBaseContext())
            {
                var result = (from pg in db.PersonelGorev
                              join g in db.Gorev on pg.GorevId equals g.GorevId
                              join p in db.Personel on pg.PersonelId equals p.PersonelId
                              where p.PersonelId == id
                              select new PersonelGorevVM()
                              {
                                  Gorev = g,
                                  Personel = p,
                                  PersonelGorev = pg
                              }
                              ).ToList();

                return View(result);
            }


        }
        #endregion
        [LoginFilter]
        #region PersonelAlanlari
        public IActionResult PersonelAlanlari(int id)
        {
            ViewBag.PersonelId = id;
            using (DataBaseContext db = new DataBaseContext())
            {
                var result = (from pg in db.PersonelUzmanlik
                              join g in db.UzmanlikAlanlari on pg.UzmanlikId equals g.UzmanlikId
                              join p in db.Personel on pg.PersonelId equals p.PersonelId
                              where p.PersonelId == id
                              select new PersonelUzmanlikVM()
                              {
                                  UzmanlikAlanlari = g,
                                  Personel = p,
                                  PersonelUzmanlik = pg
                              }
                              ).ToList();

                return View(result);
            }


        }
        #endregion
    }
}
