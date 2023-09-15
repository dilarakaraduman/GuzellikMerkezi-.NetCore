using Microsoft.AspNetCore.Mvc;
using NETCore_Demo_StajProject.App_Filter;
using NETCore_Demo_StajProject.DAL;
using NETCore_Demo_StajProject.Models;



namespace NETCore_Demo_StajProject.Controllers.Personeller
{
    public class GorevController : Controller
    {
        DataBaseContext db = new DataBaseContext();
        #region Listeleme
        public IActionResult Index()
        {
            var r = db.Gorev.ToList();
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
       
        #region Kayıt Güncelleme
        [LoginFilter]
        public IActionResult Edit(int id)
        {
            var r = db.Gorev.Find(id);
            return View(r);
        }
        [HttpPost]
        [ActionName("Edit")]
        public IActionResult EditPost(Gorev model)
        {
            db.Gorev.Update(model);
            db.SaveChanges();
            return RedirectToAction("Index","Gorev");

        }
        #endregion
       
        #region Kayıt Silme
        [LoginFilter]
        public IActionResult Remove(int id)
        {
            
            var r = db.Gorev.Find(id);
            return View(r);
        }
        [HttpPost]
        [ActionName("Remove")]
        public IActionResult RemovePost(Gorev model)
        {
            try
            {
               
                db.Gorev.Remove(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["ErrorMessage2"] = "İlk önce personelin görevlerini silin";
                return RedirectToAction("Remove");
            }

        }

        #endregion

    }

}




