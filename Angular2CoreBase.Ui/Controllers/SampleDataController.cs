using System;
namespace Angular2CoreBase.Ui.Controllers
{
	using Common.CommonModels.WeatherService;
	using Data.Factories;
	using Microsoft.AspNetCore.Http;
	using System.Net.Http;
	using System.Threading.Tasks;
	using Common.Interfaces.WeatherService;
	using Microsoft.AspNetCore.Mvc;

	[Route("api/[controller]")]
	public class SampleDataController : Controller
	{
		public IWeatherService WeatherService { get; }

		public SampleDataController(IWeatherService weatherService)
		{
			WeatherService = weatherService;
		}

		[HttpGet("[action]")]
		[ProducesResponseType(typeof(WeatherData), 200)]
		public async Task<IActionResult> WeatherForecasts()
		{
			try
			{
				WeatherData weatherForecast = await WeatherService.GetWeatherData(1, 2);
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