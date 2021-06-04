using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using SavoryTreats.Models;

namespace Factory.Controllers
{
	[Authorize]
	public class TreatsController : Controller
	{
		private readonly SavoryTreatsContext _db;
		private readonly UserManager<ApplicationUser> _userManager;

		public TreatsController(UserManager<ApplicationUser> userManager, SavoryTreatsContext db)
		{
			_userManager = userManager;
			_db = db;
		}

		private Treat GetTreatFromId(int id)
		{
			return _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
		}

		private async Task<ApplicationUser> GetCurrentUser()
		{
			string userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			return await _userManager.FindByIdAsync(userId);
		}

		[AllowAnonymous]
		public ActionResult Index()
		{
			List<Treat> treats = _db.Treats.ToList();
			return View(treats);
		}

		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Create(Treat treat, int flavorId)
		{
			_db.Treats.Add(treat);

			if (flavorId != 0)
			{
				_db.FlavorTreats.Add(new FlavorTreat() { FlavorId = flavorId, TreatId = treat.TreatId });
			}

			_db.SaveChanges();
			return RedirectToAction("Index");
		}

		[AllowAnonymous]
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

		public ActionResult AddFlavor(int id)
		{
			Treat thisTreat = GetTreatFromId(id);
			ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "Name");
			return View(thisTreat);
		}

		[HttpPost]
		public ActionResult AddFlavor(Treat treat, int flavorId)
		{
			bool duplicate = _db.FlavorTreats.Any(join => join.FlavorId == flavorId && join.TreatId == treat.TreatId);

			if (flavorId != 0 && !duplicate)
			{
				_db.FlavorTreats.Add(new FlavorTreat() { FlavorId = flavorId, TreatId = treat.TreatId });
			}

			_db.SaveChanges();
			return RedirectToAction("Details", new { id = treat.TreatId });
		}

		[HttpPost]
		public ActionResult DeleteFlavor(int joinId)
		{
			FlavorTreat joinEntry = _db.FlavorTreats.FirstOrDefault(entry => entry.FlavorTreatId == joinId);
			_db.FlavorTreats.Remove(joinEntry);
			_db.SaveChanges();
			return RedirectToAction("Details", new { id = joinEntry.TreatId });
		}
	}
}