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
	}
}