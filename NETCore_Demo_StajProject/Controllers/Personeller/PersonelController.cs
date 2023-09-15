using Microsoft.AspNetCore.Mvc;
using NETCore_Demo_StajProject.App_Filter;
using NETCore_Demo_StajProject.DAL;
using NETCore_Demo_StajProject.Models;
using NETCore_Demo_StajProject.Views;

namespace NETCore_Demo_StajProject.Controllers.Personeller
{
    public class PersonelController : Controller
    {


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

        #region Kayıt Güncelleme
        [LoginFilter]
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
			return RedirectToAction("Index", "Personel");

		}
        #endregion

        #region Kayıt Silme
        [LoginFilter]
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
			return RedirectToAction("Index", "Personel");

		}

        #endregion

        #region PersonelGorevleri
        [LoginFilter]
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

        #region PersonelAlanlari
        [LoginFilter]
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

        #region Geçmiş Randevular
        [LoginFilter]
		public IActionResult GecmisRandevular()
		{
			var personel = ProgramUtility.GetPersonel(HttpContext);
			ViewData["personel"] = personel;

			using (DataBaseContext db = new DataBaseContext())
			{
				var r = (from randevu in db.Randevu
						 join s in db.Salon on randevu.SalonId equals s.SalonId
						 join p in db.Personel on randevu.PersonelId equals p.PersonelId
						 join m in db.Musteri on randevu.MusteriId equals m.MusteriId
						 join op in db.Operasyonlar on randevu.OperasyonId equals op.OperasyonId
						 where randevu.PersonelId == personel.PersonelId && randevu.Tarih < DateTime.Now
						 select new RandevuVM()
						 {
							 Randevu = randevu,
							 Salon = s,
							 Musteri = m,
							 Personel = p,
							 Operasyonlar = op
						 }).ToList();

				return View(r);
			}
		}
        #endregion

        #region Aktif Randevular
        [LoginFilter]
		public IActionResult AktifRandevular()
		{
			var personel = ProgramUtility.GetPersonel(HttpContext);
			ViewData["personel"] = personel;

			using (DataBaseContext db = new DataBaseContext())
			{
				var r = (from randevu in db.Randevu
						 join s in db.Salon on randevu.SalonId equals s.SalonId
						 join p in db.Personel on randevu.PersonelId equals p.PersonelId
						 join m in db.Musteri on randevu.MusteriId equals m.MusteriId
						 join op in db.Operasyonlar on randevu.OperasyonId equals op.OperasyonId
						 where randevu.PersonelId == personel.PersonelId && randevu.Tarih > DateTime.Now
						 select new RandevuVM()
						 {
							 Randevu = randevu,
							 Salon = s,
							 Musteri = m,
							 Personel = p,
							 Operasyonlar = op
						 }).ToList();

				return View(r);
			}
		}
        #endregion



    }
}
