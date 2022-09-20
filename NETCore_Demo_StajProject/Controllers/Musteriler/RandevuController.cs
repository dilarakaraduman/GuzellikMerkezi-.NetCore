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
            DataBaseContext db = new DataBaseContext();
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

            var values = from SaatId e in Enum.GetValues(typeof(SaatId))
                         select new { Id = e, Name = e.ToString() };
            ViewBag.SaatId = new SelectList(values, "Id", "Name");

            // var k = GetEnumSelectList(SaatId);

            //ViewBag.SaatId = new SelectList(db.Randevu.ToList(), "SaatId");
            return View(r);
        }
        [HttpPost]
        [ActionName("Create")]
        public IActionResult CreatePost(Randevu model)
        {

            try
            {
                var query = (from r in db.Randevu
                             where model.MusteriId !=r.MusteriId &&
                             r.PersonelId == model.PersonelId && r.SaatId == model.SaatId && r.SalonId == model.SalonId
                             && r.Tarih == model.Tarih
                             select r
                             ).Any();

                if (query)
                {
                    ViewBag.PersonelId = new SelectList(db.Personel.ToList(), "PersonelId", "PersonelAd");
                    ViewBag.OperasyonId = new SelectList(db.Operasyonlar.ToList(), "OperasyonId", "OperasyonAd");
                    ViewBag.SalonId = new SelectList(db.Salon.ToList(), "SalonId", "SalonNo");

                    var values = from SaatId e in Enum.GetValues(typeof(SaatId))
                                 select new { Id = e, Name = e.ToString() };
                    ViewBag.SaatId = new SelectList(values, "Id", "Name");

                    ViewBag.Mesaj = "Randevu dolu.";
                    RedirectToAction("Create", "Randevu");

                    return View();
                }

                db.Randevu.Add(model);
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
            //id boş ise listeye döndür

            DataBaseContext db = new DataBaseContext();
            var musteriSession = HttpContext.Session.GetString(ProgramUtility.musteriSession);
            if (musteriSession == null) return RedirectToAction("Giris", "Home");
            Musteri musteri = new Musteri();
            musteri = JsonSerializer.Deserialize<Musteri>(musteriSession);
            ViewData["musteri"] = musteri;
            var r = db.Randevu.Find(id);


            ViewBag.PersonelId = new SelectList(db.Personel.ToList(), "PersonelId", "PersonelAd",r.PersonelId);
            ViewBag.OperasyonId = new SelectList(db.Operasyonlar.ToList(), "OperasyonId", "OperasyonAd",r.OperasyonId);
            ViewBag.SalonId = new SelectList(db.Salon.ToList(), "SalonId", "SalonNo",r.SalonId);

            var values = from SaatId e in Enum.GetValues(typeof(SaatId))
                         select new { Id = e, Name = e.ToString() };
            ViewBag.SaatId = new SelectList(values, "Id", "Name",r.SaatId);

           
            return View(r);
        }
        [HttpPost]
        [ActionName("Edit")]
        public IActionResult EditPost(Randevu model)
        {
            DataBaseContext db = new DataBaseContext();
            try
            {
                var query = (from r in db.Randevu
                             where r.MusteriId != model.MusteriId &&
                             r.PersonelId == model.PersonelId && r.SaatId == model.SaatId && r.SalonId == model.SalonId
                             && r.Tarih == model.Tarih
                             select r
                             ).Any();

                if (query)
                {
                    ViewBag.PersonelId = new SelectList(db.Personel.ToList(), "PersonelId", "PersonelAd",model.PersonelId);
                    ViewBag.OperasyonId = new SelectList(db.Operasyonlar.ToList(), "OperasyonId", "OperasyonAd",model.OperasyonId);
                    ViewBag.SalonId = new SelectList(db.Salon.ToList(), "SalonId", "SalonNo",model.SalonId);

                    var values = from SaatId e in Enum.GetValues(typeof(SaatId))
                                 select new { Id = e, Name = e.ToString() };
                    ViewBag.SaatId = new SelectList(values, "Id", "Name",model.SaatId);

                    ViewBag.Mesaj = "Randevu dolu.";
                    return View(model);
                }
                db.Randevu.Update(model);
                db.SaveChanges();
                return RedirectToAction("AktifRandevular","Randevu");
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
                         where randevu.PersonelId ==personel.PersonelId
                         select new RandevuVM()
                         {   Musteri=m,
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

