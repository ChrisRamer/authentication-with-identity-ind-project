using Microsoft.AspNetCore.Mvc;
using System.Linq;
using SavoryTreats.Models;

namespace SavoryTreats.Controllers
{
	public class HomeController : Controller
	{
		private readonly SavoryTreatsContext _db;

		public HomeController(SavoryTreatsContext db)
		{
			_db = db;
		}

		[HttpGet("/")]
		public ActionResult Index()
		{
			ViewBag.Flavors = _db.Flavors.ToList();
			ViewBag.Treats = _db.Treats.ToList();
			return View();
		}
	}
}