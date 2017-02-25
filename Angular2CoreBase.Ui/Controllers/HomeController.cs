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
			//For testing database is working
			//int usersCount = userRepository.Count();
			//int errorsCount = errorRepository.Count();

			//For testing error loggers
			//ErrorFactory.GetThrownException();


			//Implement a file getter page for developers (add webservice file Directory), and database errors
			//Implement weather service
			//Implement Client Side Errors
			//Implement toaster

			//Implement Authentication

			return View();
		}

		public IActionResult Error()
		{
			return View();
		}
	}
}
