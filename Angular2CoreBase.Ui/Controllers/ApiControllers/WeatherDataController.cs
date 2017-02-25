namespace Angular2CoreBase.Ui.Controllers.ApiControllers
{
	using System;
	using System.Net.Http;
	using System.Threading.Tasks;
	using Common.CommonModels.WeatherService;
	using Common.Interfaces.WeatherService;
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Mvc;
	using Common.Extensions;
	using Microsoft.Extensions.Logging;

	[Route("api/[controller]")]
	public class WeatherDataController : Controller
	{
		public IWeatherService WeatherService { get; }

		private readonly ILogger<WeatherDataController> _logger;

		public WeatherDataController(IWeatherService weatherService, ILogger<WeatherDataController> logger)
		{
			WeatherService = weatherService;
			_logger = logger;
		}

		[HttpGet("[action]")]
		[ProducesResponseType(typeof(WeatherData), 200)]
		public async Task<IActionResult> WeatherForecasts()
		{
			WeatherData weatherForecast = await WeatherService.GetWeatherData(1, 2);
			return Ok(weatherForecast);
		}
	}
}
