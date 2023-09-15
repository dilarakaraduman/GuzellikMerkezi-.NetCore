using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NETCore_Demo_StajProject.App_Filter;
using NETCore_Demo_StajProject.DAL;
using NETCore_Demo_StajProject.Models;
using NETCore_Demo_StajProject.Views;
using System.Text.Json;

namespace NETCore_Demo_StajProject.Controllers
{
	public class AdminController : Controller
	{
		// GET: AdminController
		DataBaseContext db = new DataBaseContext();

		#region Müşteri Kayıt Listeleme
		[LoginFilter]
		public IActionResult Index()
		{
            DataBaseContext db = new DataBaseContext();
            var x = db.Musteri.ToList();
			return View(x);

		}
		#endregion

		#region Personel Kayıt Listeleme
		[LoginFilter]
		public IActionResult PersonelIndex()
		{
			var r = db.Personel.ToList();
			return View(r);

        }
		#endregion

		#region Musteri Yeni Kayıt
		[LoginFilter]
		public IActionResult MusteriCreate()
		{
			return View();
		}
		[HttpPost]
		[ActionName("Create")]
		public IActionResult MusteriCreatePost(Musteri model)
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
			}
			return RedirectToAction("Index","Admin");
		}
		#endregion

		#region Personel Yeni Kayıt
		[LoginFilter]
		public IActionResult PersonelCreate()
		{
			return View();
		}
		[HttpPost]
		[ActionName("PersonelCreate")]
		public IActionResult PersonelCreatePost(Personel model)
		{
			try
			{
				db.Personel.Add(model);
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			return View("PersonelIndex","Admin");
		}
		#endregion

		#region Müşteri Kayıt Güncelleme
		[LoginFilter]
		public IActionResult MusteriEdit(int id)
		{
			var r = db.Musteri.Find(id);
			return View(r);
		}
		[HttpPost]
		[ActionName("MusteriEdit")]
		public IActionResult MusteriEditPost(Musteri model)
		{
			db.Musteri.Update(model);
			db.SaveChanges();
			return RedirectToAction("Index","Admin");

		}
		#endregion

		#region Personel Kayıt Güncelleme
		[LoginFilter]
		public IActionResult PersonelEdit(int id)
		{
			DataBaseContext db = new DataBaseContext();
			var r = db.Personel.Find(id);
			return View(r);
		}
		[HttpPost]
		[ActionName("PersonelEdit")]
		public IActionResult PersonelEditPost(Personel model)
		{
			db.Personel.Update(model);
			db.SaveChanges();
			return RedirectToAction("PersonelIndex", "Admin");

		}
		#endregion

		#region Müşteri Kayıt Silme
		[LoginFilter]
		public IActionResult Remove(int id)
		{
			var r = db.Musteri.Find(id);
			return View(r);
		}
		[HttpPost]
		[ActionName("Remove")]
		public IActionResult RemovePost(Musteri model)
		{
			try
			{
                db.Musteri.Remove(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
			catch
			{
                TempData["ErrorMessage2"] = "İlk önce müşterinin randevularını silin";
                return RedirectToAction("Remove");
            }

		}

		#endregion

		#region Personel Kayıt Silme
		[LoginFilter]
		public IActionResult PersonelRemove(int id)
		{
			DataBaseContext db = new DataBaseContext();
			var r = db.Personel.Find(id);
			return View(r);
		}
		[HttpPost]
		[ActionName("PersonelRemove")]
		public IActionResult PersonelRemovePost(Personel model)
		{
			try
			{
                db.Personel.Remove(model);
                db.SaveChanges();
                return RedirectToAction("Index", "PersonelIndex");
            }
			catch
			{
                TempData["ErrorMessage2"] = "İlk önce personelin randevularını silin";
                return RedirectToAction("PersonelRemove");
            }

		}

		#endregion

		#region Personel Randevu Gorüntüle
		[LoginFilter]
		public IActionResult PersonelRandevuGoruntule(int id)
		{
			var r = db.Personel.Find(id);
			var x = (from randevu in db.Randevu
					 join s in db.Salon on randevu.SalonId equals s.SalonId
					 join p in db.Personel on randevu.PersonelId equals p.PersonelId
					 join m in db.Musteri on randevu.MusteriId equals m.MusteriId
					 join op in db.Operasyonlar on randevu.OperasyonId equals op.OperasyonId
					 where randevu.PersonelId == r.PersonelId
					 select new RandevuVM()
					 {
						 Randevu = randevu,
						 Salon = s,
						 Musteri = m,
						 Personel = p,
						 Operasyonlar = op

					 }).ToList();

			return View(x);

		}
		#endregion

		#region Musteri Randevu Gorüntüle
		[LoginFilter]
		public IActionResult MusteriRandevuGoruntule(int id)
		{
			var r = db.Musteri.Find(id);
			var x = (from randevu in db.Randevu
					 join s in db.Salon on randevu.SalonId equals s.SalonId
					 join p in db.Personel on randevu.PersonelId equals p.PersonelId
					 join m in db.Musteri on randevu.MusteriId equals m.MusteriId
					 join op in db.Operasyonlar on randevu.OperasyonId equals op.OperasyonId
					 where randevu.MusteriId == r.MusteriId
					 select new RandevuVM()
					 {
						 Randevu = randevu,
						 Salon = s,
						 Musteri = m,
						 Personel = p,
						 Operasyonlar = op

					 }).ToList();

			return View(x);

		}
		#endregion

		#region Randevu Kayıt Güncelleme
		[LoginFilter]
		public IActionResult RandevuEdit(int id)
		{

			var r = db.Randevu.Find(id);

			ViewBag.MusteriId = new SelectList(db.Musteri.ToList(), "MusteriId", "MusteriAd", r.MusteriId);
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
					return RedirectToAction("Index", "Admin");
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
			DataBaseContext db = new DataBaseContext();
			var r = db.Randevu.Find(id);
			return View(r);
		}
		[HttpPost]
		[ActionName("RandevuRemove")]
		public IActionResult RandevuRemovePost(Randevu model)
		{
			db.Randevu.Remove(model);
			db.SaveChanges();
			return RedirectToAction("Index", "Admin");

		}

		#endregion

		#region Ürün Listeleme
		[LoginFilter]
		public IActionResult UrunListele()
        {
            var r =db.Urun.ToList();
            return View(r);
        }
		#endregion

		#region Yeni Ürün
		[LoginFilter]
		public IActionResult UrunCreate()
		{
			return View();
		}
		[HttpPost]
		[ActionName("UrunCreate")]
		public IActionResult UrunCreatePost(Urun model)
		{
			try
			{
				db.Urun.Add(model);
				db.SaveChanges();
				return RedirectToAction("UrunListele","Admin");
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			return RedirectToAction("UrunListele", "Admin");
		}
		#endregion

		#region Ürün Güncelleme
		[LoginFilter]
		public IActionResult UrunEdit(int id)
		{
			var r = db.Urun.Find(id);
			return View(r);
		}
		[HttpPost]
		[ActionName("UrunEdit")]
		public IActionResult UrunEditPost(Urun model)
		{
			db.Urun.Update(model);
			db.SaveChanges();
			return RedirectToAction("UrunListele", "Admin");

		}
		#endregion

		#region Ürün  Silme
		[LoginFilter]
		public IActionResult UrunRemove(int id)
		{
			var r = db.Urun.Find(id);
			return View(r);
		}
		[HttpPost]
		[ActionName("UrunRemove")]
		public IActionResult UrunRemovePost(Urun model)
		{
				db.Urun.Remove(model);
				db.SaveChanges();
				return RedirectToAction("UrunListele","Admin");
		}
		#endregion

	}
}
