using Angular2CoreBase.Data.Interfaces;
using Angular2CoreBase.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Angular2CoreBase.Ui.Controllers
{
	using Common.Services;
	using Common.Services.LoggingServices;
	using Data.Factories;

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

			var thrownException = ErrorFactory.GetThrownException();

			return View();
		}

		public IActionResult Error()
		{
			return View();
		}
	}
}
