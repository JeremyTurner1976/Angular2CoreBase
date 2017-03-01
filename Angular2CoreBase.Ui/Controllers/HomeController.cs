namespace Angular2CoreBase.Ui.Controllers
{
	using Data.Interfaces;
	using Data.Models;
	using Microsoft.AspNetCore.Mvc;

	public class HomeController : Controller
	{
		private readonly IRepository<Error> errorRepository;
		private readonly IRepository<ApplicationUser> userRepository;

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
			//ErrorFactory.ThrowException();


			//Client Side work left

			//Implement a file getter page for developers (add webservice file Directory, output as html), and database errors
			//Implement weather service
			//Implement Client Side Errors
			//Implement toaster - Perhaps production error handler output there, but create much smaller html to display in the toaster (Message)

			//Implement Authentication

			return View();
		}

		public IActionResult Error()
		{
			return View();
		}
	}
}