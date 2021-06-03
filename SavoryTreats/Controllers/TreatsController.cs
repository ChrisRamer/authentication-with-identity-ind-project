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

		public ActionResult Edit(int id)
		{
			Treat thisTreat = GetTreatFromId(id);
			ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "Name");
			return View(thisTreat);
		}

		[HttpPost]
		public ActionResult Edit(Treat treat, int flavorId)
		{
			bool duplicate = _db.FlavorTreats.Any(join => join.FlavorId == flavorId && join.TreatId == treat.TreatId);

			if (flavorId != 0 && !duplicate)
			{
				_db.FlavorTreats.Add(new FlavorTreat() { FlavorId = flavorId, TreatId = treat.TreatId });
			}

			_db.Entry(treat).State = EntityState.Modified;
			_db.SaveChanges();
			return RedirectToAction("Details", new { id = treat.TreatId });
		}

		public ActionResult Delete(int id)
		{
			Treat thisTreat = GetTreatFromId(id);
			return View(thisTreat);
		}

		[HttpPost, ActionName("Delete")]
		public ActionResult DeleteConfirmed(int id)
		{
			Treat thisTreat = GetTreatFromId(id);
			_db.Treats.Remove(thisTreat);
			_db.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}