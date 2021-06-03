using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SavoryTreats.Models;
using SavoryTreats.ViewModels;

namespace SavoryTreats.Controllers
{
	public class AccountController : Controller
	{
		private readonly SavoryTreatsContext _db;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, SavoryTreatsContext db)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_db = db;
		}

		public ActionResult Index()
		{
			return View();
		}

		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<ActionResult> Register(RegisterViewModel model)
		{
			ApplicationUser user = new ApplicationUser { UserName = model.Email };
			IdentityResult result = await _userManager.CreateAsync(user, model.Password);

			if (result.Succeeded)
			{
				return RedirectToAction("Index");
			}
			else
			{
				return View();
			}
		}
	}
}