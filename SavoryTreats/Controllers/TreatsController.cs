using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using SavoryTreats.Models;

namespace Factory.Controllers
{
	public class TreatsController : Controller
	{
		private readonly SavoryTreatsContext _db;

		public TreatsController(SavoryTreatsContext db)
		{
			_db = db;
		}

		private Treat GetTreatFromId(int id)
		{
			return _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
		}

		public ActionResult Index()
		{
			List<Treat> model = _db.Treats.ToList();
			return View(model);
		}

		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Create(Treat treat)
		{
			_db.Treats.Add(treat);
			_db.SaveChanges();
			return RedirectToAction("Index");
		}

		public ActionResult Details(int id)
		{
			Treat thisTreat = GetTreatFromId(id);
			return View(thisTreat);
		}
	}
}