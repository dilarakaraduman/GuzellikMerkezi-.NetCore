using Microsoft.AspNetCore.Mvc;
using NETCore_Demo_StajProject.App_Filter;
using NETCore_Demo_StajProject.DAL;
using NETCore_Demo_StajProject.Models;



namespace NETCore_Demo_StajProject.Controllers.Personeller
{
    public class GorevController : Controller
    {
        [LoginFilter]
        #region Listeleme
        public IActionResult Index()
        {
            DataBaseContext db = new DataBaseContext();
            var r = db.Gorev.ToList();
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
        public IActionResult CreatePost(Gorev model)
        {
            try
            {
                DataBaseContext db = new DataBaseContext();
                db.Gorev.Add(model);
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
            var r = db.Gorev.Find(id);
            return View(r);
        }
        [HttpPost]
        [ActionName("Edit")]
        public IActionResult EditPost(Gorev model)
        {
            DataBaseContext db = new DataBaseContext();
            db.Gorev.Update(model);
            db.SaveChanges();
            return View();

        }
        #endregion
        [LoginFilter]
        #region Kayıt Silme
        public IActionResult Remove(int id)
        {
            DataBaseContext db = new DataBaseContext();
            var r = db.Gorev.Find(id);
            return View(r);
        }
        [HttpPost]
        [ActionName("Remove")]
        public IActionResult RemovePost(Gorev model)
        {
            DataBaseContext db = new DataBaseContext();
            db.Gorev.Remove(model);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        #endregion

    }

}




