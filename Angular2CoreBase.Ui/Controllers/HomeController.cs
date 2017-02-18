using Angular2CoreBase.Data.Interfaces;
using Angular2CoreBase.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Angular2CoreBase.Ui.Controllers
{
	public class HomeController : Controller
	{
		private readonly IRepository<ApplicationUser> userRepository;
		private readonly IRepository<Error> errorRepository;

		public HomeController(
			IRepository<ApplicationUser> userRepository,
			IRepository<Error> errorRepository)
		{
			this.userRepository = userRepository;
			this.errorRepository = errorRepository;
		}

		public IActionResult Index()
		{
			int usersCount = userRepository.Count();
			int errorsCount = errorRepository.Count();

			return View();
		}

		public IActionResult Error()
		{
			return View();
		}
	}
}
