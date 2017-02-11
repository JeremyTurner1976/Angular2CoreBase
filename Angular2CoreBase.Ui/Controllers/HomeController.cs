using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Angular2CoreBase.Data;
using Angular2CoreBase.Data.Interfaces;
using Angular2CoreBase.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

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
			var test = userRepository.Count();

			return View();
		}

		public IActionResult Error()
		{
			return View();
		}
	}
}
