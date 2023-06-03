using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NETCore_Demo_StajProject.App_Filter;
using NETCore_Demo_StajProject.DAL;
using NETCore_Demo_StajProject.Models;
using NETCore_Demo_StajProject.Views;
using System.Text.Json;

namespace NETCore_Demo_StajProject.Controllers.Musteriler
{
	public class RandevuController : Controller
	{
		DataBaseContext db = new DataBaseContext();
		public IActionResult Index()
		{
			var musteri = ProgramUtility.GetMusteri(HttpContext);
			ViewData["musteri"] = musteri;
			DataBaseContext db = new DataBaseContext();
			var r = (from randevu in db.Randevu
					 join s in db.Salon on randevu.SalonId equals s.SalonId
					 join p in db.Personel on randevu.PersonelId equals p.PersonelId
					 join m in db.Musteri on randevu.MusteriId equals m.MusteriId
					 join op in db.Operasyonlar on randevu.OperasyonId equals op.OperasyonId
					 where randevu.MusteriId == musteri.MusteriId
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
		[LoginFilter]
		#region Yeni Kayıt
		public IActionResult Create()
		{
			var musteriSession = HttpContext.Session.GetString(ProgramUtility.musteriSession);
			if (musteriSession == null) return RedirectToAction("Giris", "Home");
			Musteri musteri = new Musteri();
			musteri = JsonSerializer.Deserialize<Musteri>(musteriSession);
			ViewData["musteri"] = musteri;

			Randevu r = new Randevu();

			r.MusteriId = musteri.MusteriId;
			r.Tarih = DateTime.Now;

			ViewBag.PersonelId = new SelectList(db.Personel.ToList(), "PersonelId", "PersonelAd");
			ViewBag.OperasyonId = new SelectList(db.Operasyonlar.ToList(), "OperasyonId", "OperasyonAd");
			ViewBag.SalonId = new SelectList(db.Salon.ToList(), "SalonId", "SalonNo");
			ViewBag.SaatId = new SelectList(db.Saat.ToList(), "SaatId", "SaatNo");

			return View(r);
		}
		[HttpPost]
		[ActionName("Create")]
		public IActionResult CreatePost(Randevu model)
		{
			try
			{
				DateTime now = DateTime.Now;
				TimeSpan desired_time = new TimeSpan(9, 30, 0);
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
					return RedirectToAction("Create");

				}
				else if(model.Tarih == DateTime.Now.Date && (second > count)){
                    TempData["ErrorMessage1"] = "Randevu geçmiş tarih.";
                    return RedirectToAction("Create");
                }
				else {
					db.Randevu.Add(model);
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
		[LoginFilter]
		#region Kayıt Güncelleme
		public IActionResult Edit(int id)
		{
			//id boş ise listeye döndür
			var musteriSession = HttpContext.Session.GetString(ProgramUtility.musteriSession);
			if (musteriSession == null) return RedirectToAction("Giris", "Home");
			Musteri musteri = new Musteri();
			musteri = JsonSerializer.Deserialize<Musteri>(musteriSession);
			ViewData["musteri"] = musteri;
			var r = db.Randevu.Find(id);

			ViewBag.PersonelId = new SelectList(db.Personel.ToList(), "PersonelId", "PersonelAd", r.PersonelId);
			ViewBag.OperasyonId = new SelectList(db.Operasyonlar.ToList(), "OperasyonId", "OperasyonAd", r.OperasyonId);
			ViewBag.SalonId = new SelectList(db.Salon.ToList(), "SalonId", "SalonNo", r.SalonId);
			ViewBag.SaatId = new SelectList(db.Saat.ToList(), "SaatId", "SaatNo");
			return View(r);
		}
		[HttpPost]
		[ActionName("Edit")]
		public IActionResult EditPost(Randevu model)
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
					return RedirectToAction("Create");

				}
				else if (model.Tarih == DateTime.Now.Date && (second > count))
				{
					TempData["ErrorMessage1"] = "Randevu geçmiş tarih.";
					return RedirectToAction("Create");
				}
				else
				{
					db.Randevu.Add(model);
					db.SaveChanges();
					return RedirectToAction("Index");
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

		}
		#endregion
		[LoginFilter]
		#region Kayıt Silme
		public IActionResult Remove(int id)
		{
			DataBaseContext db = new DataBaseContext();
			var r = db.Randevu.Find(id);
			return View(r);
		}
		[HttpPost]
		[ActionName("Remove")]
		public IActionResult RemovePost(Randevu model)
		{
			DataBaseContext db = new DataBaseContext();
			db.Randevu.Remove(model);
			db.SaveChanges();
			return RedirectToAction("Index");

		}

		#endregion

		[LoginFilter]
		public IActionResult GecmisRandevular()
		{
			var musteri = ProgramUtility.GetMusteri(HttpContext);
			ViewData["musteri"] = musteri;

			using (DataBaseContext db = new DataBaseContext())
			{
				var r = (from randevu in db.Randevu
						 join s in db.Salon on randevu.SalonId equals s.SalonId
						 join p in db.Personel on randevu.PersonelId equals p.PersonelId
						 join m in db.Musteri on randevu.MusteriId equals m.MusteriId
						 join op in db.Operasyonlar on randevu.OperasyonId equals op.OperasyonId
						 where randevu.MusteriId == musteri.MusteriId && randevu.Tarih < DateTime.Now
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
		[LoginFilter]
		public IActionResult AktifRandevular()
		{
			var musteri = ProgramUtility.GetMusteri(HttpContext);
			ViewData["musteri"] = musteri;

			using (DataBaseContext db = new DataBaseContext())
			{
				var r = (from randevu in db.Randevu
						 join s in db.Salon on randevu.SalonId equals s.SalonId
						 join p in db.Personel on randevu.PersonelId equals p.PersonelId
						 join m in db.Musteri on randevu.MusteriId equals m.MusteriId
						 join op in db.Operasyonlar on randevu.OperasyonId equals op.OperasyonId
						 where randevu.MusteriId == musteri.MusteriId && randevu.Tarih > DateTime.Now
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
		[LoginFilter]
		public IActionResult RandevuGoruntule()
		{

			var personel = ProgramUtility.GetPersonel(HttpContext);
			ViewData["personel"] = personel;

			using (DataBaseContext db = new DataBaseContext())
			{
				var r = (from randevu in db.Randevu
						 join s in db.Salon on randevu.SalonId equals s.SalonId
						 join p in db.Personel on randevu.PersonelId equals p.PersonelId
						 join op in db.Operasyonlar on randevu.OperasyonId equals op.OperasyonId
						 join m in db.Musteri on randevu.MusteriId equals m.MusteriId
						 where randevu.PersonelId == personel.PersonelId
						 select new RandevuVM()
						 {
							 Musteri = m,
							 Randevu = randevu,
							 Salon = s,
							 Personel = p,
							 Operasyonlar = op
						 }).ToList();

				return View(r);
			}
		}
	}
}

