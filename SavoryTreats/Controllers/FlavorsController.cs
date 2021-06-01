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
	}
}