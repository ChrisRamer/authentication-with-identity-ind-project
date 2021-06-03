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

namespace SavoryTreats.Controllers
{
	[Authorize]
	public class FlavorsController : Controller
	{
		private readonly SavoryTreatsContext _db;
		private readonly UserManager<ApplicationUser> _userManager;

		public FlavorsController(UserManager<ApplicationUser> userManager, SavoryTreatsContext db)
		{
			_userManager = userManager;
			_db = db;
		}

		private Flavor GetFlavorFromId(int id)
		{
			return _db.Flavors.FirstOrDefault(Flavor => Flavor.FlavorId == id);
		}

		private async Task<ApplicationUser> GetCurrentUser()
		{
			string userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			return await _userManager.FindByIdAsync(userId);
		}

		public async Task<ActionResult> Index()
		{
			ApplicationUser currentUser = await GetCurrentUser();
			List<Flavor> userFlavors = _db.Flavors.Where(entry => entry.User.Id == currentUser.Id).ToList();
			return View(userFlavors);
		}

		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<ActionResult> Create(Flavor flavor)
		{
			flavor.User = await GetCurrentUser();
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

		[HttpPost]
		public ActionResult DeleteTreat(int joinId)
		{
			FlavorTreat joinEntry = _db.FlavorTreats.FirstOrDefault(entry => entry.FlavorTreatId == joinId);
			_db.FlavorTreats.Remove(joinEntry);
			_db.SaveChanges();
			return RedirectToAction("Details", new { id = joinEntry.FlavorId });
		}
	}
}