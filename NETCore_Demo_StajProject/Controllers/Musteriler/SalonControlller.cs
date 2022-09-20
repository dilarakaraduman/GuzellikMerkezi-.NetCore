using Microsoft.AspNetCore.Mvc;
using NETCore_Demo_StajProject.App_Filter;
using NETCore_Demo_StajProject.DAL;
using NETCore_Demo_StajProject.Models;
namespace NETCore_Demo_StajProject.Controllers.Musteriler
{
    public class SalonControlller : Controller
    {
        public IActionResult Index()
        {
            DataBaseContext db = new DataBaseContext();
            var r = db.Salon.ToList();
            return View(r);

        }
        [LoginFilter]
        #region Yeni Kayıt

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ActionName("Create")]
        public IActionResult CreatePost(Salon model)
        {
            try
            {
                DataBaseContext db = new DataBaseContext();
                db.Salon.Add(model);
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
            var r = db.Salon.Find(id);
            return View(r);
        }
        [HttpPost]
        [ActionName("Edit")]
        public IActionResult EditPost(Salon model)
        {
            DataBaseContext db = new DataBaseContext();
            db.Salon.Update(model);
            db.SaveChanges();
            return View();

        }
        #endregion
        [LoginFilter]
        #region Kayıt Silme
        public IActionResult Remove(int id)
        {
            DataBaseContext db = new DataBaseContext();
            var r = db.Salon.Find(id);
            return View(r);
        }
        [HttpPost]
        [ActionName("Remove")]
        public IActionResult RemovePost(Salon model)
        {
            DataBaseContext db = new DataBaseContext();
            db.Salon.Remove(model);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        #endregion




    }
}





  
