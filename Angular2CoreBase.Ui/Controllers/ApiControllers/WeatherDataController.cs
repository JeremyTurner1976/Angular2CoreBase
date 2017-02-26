namespace Angular2CoreBase.Ui.Controllers.ApiControllers
{
	using System.Threading.Tasks;
	using Common.CommonModels.WeatherService;
	using Common.Interfaces.WeatherService;
	using Microsoft.AspNetCore.Mvc;

	[Route("api/v1/[controller]")]
	public class WeatherDataController : Controller
	{
		public IWeatherService WeatherService { get; }

		public WeatherDataController(IWeatherService weatherService)
		{
			WeatherService = weatherService;
		}

		[HttpGet("WeatherForecasts")]
		[ProducesResponseType(typeof (WeatherData), 200)]
		public async Task<IActionResult> WeatherForecasts()
		{
			WeatherData weatherForecast = await WeatherService.GetWeatherData(1, 2);
			return Ok(weatherForecast);
		}
	}
}