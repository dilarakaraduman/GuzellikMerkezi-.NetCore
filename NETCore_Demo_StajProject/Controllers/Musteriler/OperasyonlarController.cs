using Microsoft.AspNetCore.Mvc;
using NETCore_Demo_StajProject.App_Filter;
using NETCore_Demo_StajProject.DAL;
using NETCore_Demo_StajProject.Models;

namespace NETCore_Demo_StajProject.Controllers.Musteriler
{
    public class OperasyonlarController : Controller
    {
        #region Operasyonlar Listele
        DataBaseContext db = new DataBaseContext();
        public IActionResult Index()
        {
            
            var r = db.Operasyonlar.ToList();
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
        public IActionResult CreatePost(Operasyonlar model)
        {
            try
            {
                DataBaseContext db = new DataBaseContext();
                if (model != null) {
                    db.Operasyonlar.Add(model);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
               
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
            DataBaseContext db = new DataBaseContext();
            var r = db.Operasyonlar.Find(id);
            return View(r);
        }
        [HttpPost]
        [ActionName("Edit")]
        public IActionResult EditPost(Operasyonlar model)
        {
            DataBaseContext db = new DataBaseContext();
            db.Operasyonlar.Update(model);
            db.SaveChanges();
            return RedirectToAction("Index","Operasyonlar");

        }
        #endregion

        #region Kayıt Silme
        [LoginFilter]
        public IActionResult Remove(int id)
        {
            DataBaseContext db = new DataBaseContext();
            var r = db.Operasyonlar.Find(id);
            return View(r);
        }
        [HttpPost]
        [ActionName("Remove")]
        public IActionResult RemovePost(Operasyonlar model)
        {
            try
            {
				db.Operasyonlar.Remove(model);
				db.SaveChanges();
				return RedirectToAction("Index");
			}
            catch
            {
				TempData["ErrorMessage2"] = "İlk önce müşterinin ve personelin randevularını silin";
				return RedirectToAction("Remove");
			}

        }

        #endregion
    }
}










