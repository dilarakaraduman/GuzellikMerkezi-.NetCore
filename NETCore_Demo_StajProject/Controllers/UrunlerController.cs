using Microsoft.AspNetCore.Mvc;
using NETCore_Demo_StajProject.DAL;
using NETCore_Demo_StajProject.Models;
using NETCore_Demo_StajProject.Views;

namespace NETCore_Demo_StajProject.Controllers
{
    public class UrunlerController : Controller
    {
        public IActionResult Index()
        {

			DataBaseContext db = new DataBaseContext();
			var r =  db.Urun.ToList();
            return View(r);
        }
    }
}
