using Microsoft.AspNetCore.Mvc;

namespace SavoryTreats.Controllers
{
	public class HomeController : Controller
	{
		[HttpGet("/")]
		public ActionResult Index()
		{
			return View();
		}
	}
}