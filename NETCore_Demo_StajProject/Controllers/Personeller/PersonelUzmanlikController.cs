using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NETCore_Demo_StajProject.App_Filter;
using NETCore_Demo_StajProject.DAL;

namespace NETCore_Demo_StajProject.Controllers.Personeller
{
    public class PersonelUzmanlikController : Controller
    {
        DataBaseContext db = new DataBaseContext();
        [LoginFilter]
        #region Listeleme
        public IActionResult Index(int id)
        {
            var result = db.PersonelUzmanlik.Where(h => h.PersonelId == id).ToList();
            return View(result);
        }
        #endregion
        [LoginFilter]
        #region Yeni Kayıt

        public IActionResult UzmanlikCreate(int id)
        {
            PersonelUzmanlik r = new PersonelUzmanlik()
            {
                PersonelId = id
            };

            ViewBag.UzmanlikId = new SelectList(db.UzmanlikAlanlari.ToList(), "UzmanlikId", "UzmanlikAd");

            return View(r);
        }
        [HttpPost]
        [ActionName("Create")]
        public IActionResult UzmanlikCreatePost(PersonelUzmanlik model)
        {
            try
            {
                DataBaseContext db = new DataBaseContext();
                db.PersonelUzmanlik.Add(model);
                db.SaveChanges();
                return RedirectToAction("PersonelAlanlari", "Personel", new { id = model.PersonelId });
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
        public IActionResult Remove(int id)
        {
            DataBaseContext db = new DataBaseContext();
            var r = db.PersonelUzmanlik.Find(id);
            return View(r);
        }
        [HttpPost]
        [ActionName("Remove")]
        public IActionResult RemovePost(PersonelUzmanlik model)
        {
            try
            {
                DataBaseContext db = new DataBaseContext();

                db.PersonelUzmanlik.Remove(db.PersonelUzmanlik.Find(model.Id));
                db.SaveChanges();
                return RedirectToAction("PersonelAlanlari", "Personel", new { id = model.PersonelId });

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return View();
            #endregion
        }
    }
}