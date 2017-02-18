using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Angular2CoreBase.Common.CommonModels.WeatherService;
using Angular2CoreBase.Common.CommonModels.WeatherService.OpenWeather;
using Angular2CoreBase.Common.Interfaces;
using Newtonsoft.Json;

namespace Angular2CoreBase.Common.Services.WeatherServices
{
	using Extensions;

	//https://openweathermap.org/api
	public class OpenWeatherService : IWeatherService
	{
		//TODO get url from DB, or configs?
		//Dependency Injection is correct, Providers based on configs is an anti pattern

		public string City { get; set; } = "Bigfork,Mt";
		public string AppId { get; set; } = "f6c810b72d69b9224b8ddde39e19f6f9";
		public string UnitsOfMeasurement { get; set; } = "imperial"; //metric
		public string CurrentWeatherUri { get; set; } = "/data/2.5/weather";
		public string FutureWeatherUri { get; set; } = "/data/2.5/forecast";
		public Uri BaseUri { get; set; } = new Uri("http://api.openweathermap.org");

		/// <summary>
		/// This task gathers threaded weather data from 2 webservice calls asynchronously
		/// Please be sure to handle Aggregate Exceptions in the caller
		/// </summary>
		/// <param name="latitude">The latitude to gather data for</param>
		/// <param name="longitude">The longitude to gather data for</param>
		/// <returns>A <see cref="WeatherData"> Object from the Open Weather Service Api</see>/></returns>
		public async Task<WeatherData> GetWeatherData(double latitude, double longitude)
		{
			Task<DetailedWeather> taskOne = GetCurrentWeather(latitude, longitude);
			Task<ICollection<Forecast>> taskTwo = GetFutureWeather(latitude, longitude);

			await Task.WhenAll(taskOne, taskTwo);
			DetailedWeather currentWeather = taskOne.Result;
			Weather weather = currentWeather?.weather?.FirstOrDefault();
			ICollection<Forecast> forecasts = taskTwo.Result;

			if (currentWeather != null && weather != null && forecasts != null)
			{
				return new WeatherData()
				{
					Description = weather.description,
					Sunrise = currentWeather.systemInformation.sunriseTime,
					Sunset = currentWeather.systemInformation.sunsetTime,
					City = currentWeather.city,
					Country = currentWeather.systemInformation.country,
					WeatherForecasts = forecasts
				};
			}

			if (currentWeather == null)
			{
				throw new ArgumentNullException(nameof(currentWeather));
			}
			if (weather == null)
			{
				throw new ArgumentNullException(nameof(weather));
			}

			throw new ArgumentNullException(nameof(forecasts));
		}

		private async Task<DetailedWeather> GetCurrentWeather(double latitude, double longitude)
		{
			Uri clientUri = new Uri(
				BaseUri,
				CurrentWeatherUri +
				        "?q=" + City +
				        "&appid=" + AppId +
				        "&units=" + UnitsOfMeasurement);


			using (IRestService webService = new RestService())
			{
				HttpResponseMessage response = await webService.SendAsync(
					new HttpRequestMessage(
						HttpMethod.Get,
						clientUri));

				response.EnsureSuccessStatusCode();

				string stringResult = await response.Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<DetailedWeather>(stringResult);
			}
		}

		private async Task<ICollection<Forecast>> GetFutureWeather(double latitude, double longitude)
		{
			Uri clientUri = new Uri(
				BaseUri,
				FutureWeatherUri +
						"?q=" + City +
						"&appid=" + AppId +
						"&units=" + UnitsOfMeasurement);

			using (IRestService webService = new RestService())
			{
				HttpResponseMessage response = await webService.SendAsync(
					new HttpRequestMessage(
						HttpMethod.Get,
						clientUri));

				response.EnsureSuccessStatusCode();

				string stringResult = await response.Content.ReadAsStringAsync();
				ThreeHourFiveDayForecast forecast =
									JsonConvert.DeserializeObject<ThreeHourFiveDayForecast>(stringResult);

				return (from item in forecast.forecasts
						let weather = item.weather.FirstOrDefault()
						select new Forecast()
						{
							StartDateTime = item.startDateTime,
							EndDateTime = item.startDateTime.AddHours(3).AddSeconds(-1),
							Description = weather.description,
							Temperature = item.main.temperature,
							MinimumTemperature = item.main.minimumTemperature,
							MaximumTemperature = item.main.maximumTemperature,
							Humidity = item.main.humidity,
							AtmosphericPressure = item.main.pressure,
							Windspeed = item.wind.speed,
							WindDirection = (int)item.wind.degrees,
							SkyCon = GetSkyCon(weather.icon),
							CloudCover = item.clouds.cloudCover,
							RainVolume = item.rainTotal.threeHourTotal,
							SnowVolume = item.snowTotal.threeHourTotal
						}).ToList();
			}
		}

		private string GetSkyCon(string icon)
		{
			return "Still needs doing";
		}
	}
}