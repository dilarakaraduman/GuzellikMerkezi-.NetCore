using Microsoft.AspNetCore.Mvc;
using NETCore_Demo_StajProject.App_Filter;
using NETCore_Demo_StajProject.DAL;
using NETCore_Demo_StajProject.Models;

namespace NETCore_Demo_StajProject.Controllers.Personeller
{
    public class UzmanlikAlanlariController : Controller
    {
        [LoginFilter]
        #region Listeleme
        public IActionResult Index()
        {
            DataBaseContext db = new DataBaseContext();
            var r = db.UzmanlikAlanlari.ToList();
            return View(r);

        }
        #endregion
        [LoginFilter]
        #region Yeni Kayıt

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ActionName("Create")]
        public IActionResult CreatePost(UzmanlikAlanlari model)
        {
            try
            {

                DataBaseContext db = new DataBaseContext();
                db.UzmanlikAlanlari.Add(model);
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
            var r = db.UzmanlikAlanlari.Find(id);
            return View(r);
        }
        [HttpPost]
        [ActionName("Edit")]
        public IActionResult EditPost(UzmanlikAlanlari model)
        {
            DataBaseContext db = new DataBaseContext();
            db.UzmanlikAlanlari.Update(model);
            db.SaveChanges();
            return View();

        }
        #endregion
        [LoginFilter]
        #region Kayıt Silme
        public IActionResult Remove(int id)
        {
            DataBaseContext db = new DataBaseContext();
            var r = db.UzmanlikAlanlari.Find(id);
            return View(r);
        }
        [HttpPost]
        [ActionName("Remove")]
        public IActionResult RemovePost(UzmanlikAlanlari model)
        {
            DataBaseContext db = new DataBaseContext();
            db.UzmanlikAlanlari.Remove(model);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        #endregion
    }
}
