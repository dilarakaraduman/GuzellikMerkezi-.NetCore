using Microsoft.AspNetCore.Mvc;
using NETCore_Demo_StajProject.App_Filter;
using NETCore_Demo_StajProject.DAL;
using NETCore_Demo_StajProject.Models;

namespace NETCore_Demo_StajProject.Controllers.Personeller
{
    public class UzmanlikAlanlariController : Controller
    {
        DataBaseContext db = new DataBaseContext();
        #region Listeleme
        [LoginFilter]
        public IActionResult Index()
        {
            var r = db.UzmanlikAlanlari.ToList();
            return View(r);

        }
        #endregion

        #region Yeni Kayıt
        [LoginFilter]
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

        #region Kayıt Güncelleme
        [LoginFilter]
        public IActionResult Edit(int id)
        {
            var r = db.UzmanlikAlanlari.Find(id);
            return View(r);
        }
        [HttpPost]
        [ActionName("Edit")]
        public IActionResult EditPost(UzmanlikAlanlari model)
        {  
            db.UzmanlikAlanlari.Update(model);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        #endregion

        #region Kayıt Silme
        [LoginFilter]
        public IActionResult Remove(int id)
        {
            var r = db.UzmanlikAlanlari.Find(id);
            return View(r);
        }
        [HttpPost]
        [ActionName("Remove")]
        public IActionResult RemovePost(UzmanlikAlanlari model)
        {
            try
            {
                db.UzmanlikAlanlari.Remove(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["ErrorMessage2"] = "İlk önce personellerin uzmanlık alanını silin";
                return RedirectToAction("Remove");
            }
           

        }

        #endregion
    }
}
