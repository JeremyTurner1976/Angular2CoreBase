using System;
namespace Angular2CoreBase.Ui.Controllers
{
	using Common.CommonModels.WeatherService;
	using Microsoft.AspNetCore.Http;
	using System.Net.Http;
	using System.Threading.Tasks;
	using Common.Extensions;
	using Common.Interfaces.WeatherService;
	using Data.Factories;
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
				return BadRequest(httpRequestException.GetErrorAsString());
			}
			catch (AggregateException aggregateException)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					aggregateException.GetAggregateErrorAsString());
			}
			catch (Exception exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					exception.GetErrorAsString());
			}
		}
	}
}