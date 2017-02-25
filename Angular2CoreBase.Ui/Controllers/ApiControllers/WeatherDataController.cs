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

	[Route("api/[controller]")]
	public class WeatherDataController : Controller
	{
		public IWeatherService WeatherService { get; }

		public WeatherDataController(IWeatherService weatherService)
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
				return BadRequest(httpRequestException.ToEnhancedString());
			}
			catch (AggregateException aggregateException)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					aggregateException.ToEnhancedString());
			}
			catch (Exception exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					exception.ToEnhancedString());
			}
		}
	}
}
