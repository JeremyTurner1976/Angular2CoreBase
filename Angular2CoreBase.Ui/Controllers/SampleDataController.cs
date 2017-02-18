using System;
namespace Angular2CoreBase.Ui.Controllers
{
	using Common.CommonModels.WeatherService;
	using Common.Services.WeatherServices;
	using Data.Factories;
	using Microsoft.AspNetCore.Http;
	using System.Net.Http;
	using System.Threading.Tasks;
	using Microsoft.AspNetCore.Mvc;

	[Route("api/[controller]")]
	public class SampleDataController : Controller
	{
		private static string[] Summaries = new[]
		{
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		};

		[HttpGet("[action]")]
		public async Task<IActionResult> WeatherForecasts()
		{
			try
			{
				OpenWeatherService weatherService = new OpenWeatherService();
				WeatherData weatherForecast = await weatherService.GetWeatherData(1, 2);
				return Ok(weatherForecast);
			}
			catch (HttpRequestException httpRequestException)
			{
				return BadRequest(ErrorFactory.GetErrorAsString(httpRequestException));
			}
			catch (AggregateException aggregateException)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					ErrorFactory.GetAggregateErrorAsString(aggregateException));
			}
			catch (Exception exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					ErrorFactory.GetErrorAsString(exception));
			}
		}
	}
}