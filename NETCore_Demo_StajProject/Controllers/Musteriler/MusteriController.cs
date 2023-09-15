using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NETCore_Demo_StajProject.App_Filter;
using NETCore_Demo_StajProject.DAL;
using NETCore_Demo_StajProject.Models;
using NETCore_Demo_StajProject.Views;
using System.Text.Json;

namespace NETCore_Demo_StajProject.Controllers.Musteriler
{
    public class MusteriController : Controller
    {
        DataBaseContext db = new DataBaseContext();

        #region Yeni Kayıt

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ActionName("Create")]
        public IActionResult CreatePost(Musteri model)
        {
            try
            {
                
                db.Musteri.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            };
            return RedirectToAction("Index");
        }
		#endregion

		#region Müşteri-Kayıt Güncelleme
		[LoginFilter]
		public IActionResult MusteriEdit(int id)
		{
			DataBaseContext db = new DataBaseContext();
			var r = db.Musteri.Find(id);
			return View(r);
		}
		[HttpPost]
		[ActionName("MusteriEdit")]
		public IActionResult MusteriEditPost(Musteri model)
		{
			DataBaseContext db = new DataBaseContext();
			db.Musteri.Update(model);
			db.SaveChanges();
			return RedirectToAction("Index", "Randevu");

		}
		#endregion

		#region Kayıt Silme
		[LoginFilter]
		public IActionResult Remove(int id)
        {
            DataBaseContext db = new DataBaseContext();
            var r = db.Musteri.Find(id);
            return View(r);
        }
        [HttpPost]
        [ActionName("Remove")]
        public IActionResult RemovePost(Musteri model)
        {
                DataBaseContext db = new DataBaseContext();
                db.Musteri.Remove(model);
                db.SaveChanges();
                return RedirectToAction("Index");

        }

		#endregion

		#region Randevu Kayıt Güncelleme
		[LoginFilter]
		public IActionResult RandevuEdit(int id)
		{
            var musteriSession = HttpContext.Session.GetString(ProgramUtility.musteriSession);
            if (musteriSession == null) return RedirectToAction("Giris", "Home");
            Musteri musteri = new Musteri();
            musteri = JsonSerializer.Deserialize<Musteri>(musteriSession);

            ViewData["musteri"] = musteri;
            var r = db.Randevu.Find(id);

			ViewBag.MusteriId = r.MusteriId;
			ViewBag.PersonelId = new SelectList(db.Personel.ToList(), "PersonelId", "PersonelAd", r.PersonelId);
			ViewBag.OperasyonId = new SelectList(db.Operasyonlar.ToList(), "OperasyonId", "OperasyonAd", r.OperasyonId);
			ViewBag.SalonId = new SelectList(db.Salon.ToList(), "SalonId", "SalonNo", r.SalonId);
			ViewBag.SaatId = new SelectList(db.Saat.ToList(), "SaatId", "SaatNo");
			return View(r);
		}
		[HttpPost]
		[ActionName("RandevuEdit")]
		public IActionResult RandevuEditPost(Randevu model)
		{
			try
			{
				DateTime now = DateTime.Now;
				TimeSpan desired_time = new TimeSpan(9, 00, 0);
				DateTime desired_datetime = DateTime.Today.Add(desired_time);
				TimeSpan elapsed_time = now - desired_datetime;
				float second = (float)elapsed_time.TotalSeconds;

				var result = db.Saat.Where(s => s.SaatId == model.saatId).Select(s => s.SaatNo).FirstOrDefault();

				DateTime desired_Datetime = DateTime.Today.Add(result);
				TimeSpan elapsed_Time = desired_Datetime - desired_datetime;
				float count = (float)elapsed_Time.TotalSeconds;

				var query = (from r in db.Randevu
							 where r.PersonelId == model.PersonelId && r.saatId == model.saatId && r.SalonId == model.SalonId
							 && r.Tarih == model.Tarih
							 select r
								 ).Any();

				if (query)
				{
					TempData["ErrorMessage"] = "Randevu dolu.";
					return RedirectToAction("RandevuEdit");

				}
				else if (model.Tarih == DateTime.Now.Date && (second > count))
				{
					TempData["ErrorMessage1"] = "Randevu geçmiş tarih.";
					return RedirectToAction("RandevuEdit");
				}
				else
				{
					db.Randevu.Update(model);
					db.SaveChanges();
					return RedirectToAction("AktifRandevular", "Randevu");
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

		}
		#endregion

		#region Randevu Kayıt Silme
		[LoginFilter]
		public IActionResult RandevuRemove(int id)
		{
			
			var r = db.Randevu.Find(id);
			return View(r);
		}
		[HttpPost]
		[ActionName("RandevuRemove")]
		public IActionResult RandevuRemovePost(Randevu model)
		{
			db.Randevu.Remove(model);
			db.SaveChanges();
            return RedirectToAction("AktifRandevular", "Randevu");

        }

		#endregion


	}
}