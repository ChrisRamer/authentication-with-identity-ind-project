using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using SavoryTreats.Models;

namespace SavoryTreats.Controllers
{
	public class FlavorsController : Controller
	{
		private readonly SavoryTreatsContext _db;

		public FlavorsController(SavoryTreatsContext db)
		{
			_db = db;
		}

		private Flavor GetFlavorFromId(int id)
		{
			return _db.Flavors.FirstOrDefault(Flavor => Flavor.FlavorId == id);
		}

		public ActionResult Index()
		{
			List<Flavor> model = _db.Flavors.ToList();
			return View(model);
		}

		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Create(Flavor flavor)
		{
			_db.Flavors.Add(flavor);
			_db.SaveChanges();
			return RedirectToAction("Index");
		}

		public ActionResult Details(int id)
		{
			Flavor thisFlavor = _db.Flavors
				.Include(flavor => flavor.Treats)
				.ThenInclude(join => join.Treat)
				.FirstOrDefault(flavor => flavor.FlavorId == id);
			return View(thisFlavor);
		}

		public ActionResult Edit(int id)
		{
			Flavor thisFlavor = GetFlavorFromId(id);
			return View(thisFlavor);
		}

		[HttpPost]
		public ActionResult Edit(Flavor flavor)
		{
			_db.Entry(flavor).State = EntityState.Modified;
			_db.SaveChanges();
			return RedirectToAction("Details", new { id = flavor.FlavorId });
		}

		public ActionResult Delete(int id)
		{
			Flavor thisFlavor = GetFlavorFromId(id);
			return View(thisFlavor);
		}

		[HttpPost, ActionName("Delete")]
		public ActionResult DeleteConfirmed(int id)
		{
			Flavor thisFlavor = GetFlavorFromId(id);
			_db.Flavors.Remove(thisFlavor);
			_db.SaveChanges();
			return RedirectToAction("Index");
		}

		public ActionResult AddTreat(int id)
		{
			Flavor thisFlavor = GetFlavorFromId(id);
			ViewBag.TreatId = new SelectList(_db.Treats, "TreatId", "Name");
			return View(thisFlavor);
		}

		[HttpPost]
		public ActionResult AddTreat(Flavor flavor, int treatId)
		{
			bool duplicate = _db.FlavorTreats.Any(join => join.FlavorId == flavor.FlavorId && join.TreatId == treatId);

			if (treatId != 0 && !duplicate)
			{
				_db.FlavorTreats.Add(new FlavorTreat() { FlavorId = flavor.FlavorId, TreatId = treatId });
			}

			_db.SaveChanges();
			return RedirectToAction("Details", new { id = flavor.FlavorId });
		}
	}
}