using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NETCore_Demo_StajProject.App_Filter;
using NETCore_Demo_StajProject.DAL;

namespace NETCore_Demo_StajProject.Controllers.Personeller
{
    public class PersonelGorevController : Controller
    {
        DataBaseContext db = new DataBaseContext();
        [LoginFilter]
        #region Listeleme
        public IActionResult Index(int id)
        {
            var result = db.PersonelGorev.Where(h => h.PersonelId == id).ToList();
            return View(result);
        }
        #endregion
        [LoginFilter]
        #region Yeni Kayıt

        public IActionResult PersonelCreate(int id)
        {
            PersonelGorev r = new PersonelGorev()
            {
                PersonelId = id
            };

            ViewBag.GorevId = new SelectList(db.Gorev.ToList(), "GorevId", "GorevAd");

            return View(r);
        }
        [HttpPost]
        [ActionName("Create")]
        public IActionResult PersonelCreate(PersonelGorev model)
        {
            try
            {
                DataBaseContext db = new DataBaseContext();
                db.PersonelGorev.Add(model);
                db.SaveChanges();
                return RedirectToAction("PersonelGorevleri", "Personel", new { id = model.PersonelId });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return View();
        }
        #endregion
        [LoginFilter]
        #region Kayıt Silme
        [ActionName("Remove")]
        [HttpPost]
        public IActionResult RemoveP(int id)
        {
            DataBaseContext db = new DataBaseContext();
            var r = db.PersonelGorev.Find(id);
            db.PersonelGorev.Remove(r);
            db.SaveChanges();
            int kayitsilindimi = db.SaveChanges();
            return RedirectToAction("PersonelGorevleri", "Personel", new { id = r.PersonelId });
        }
        public IActionResult Remove(int id)
        {
            DataBaseContext db = new DataBaseContext();
            var r = db.PersonelGorev.Find(id);
            return View(r);
        }
        #endregion




    }

}