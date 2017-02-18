using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Angular2CoreBase.Common.CommonModels.WeatherService;
using Angular2CoreBase.Common.CommonModels.WeatherService.OpenWeather;
using Angular2CoreBase.Common.Interfaces;

namespace Angular2CoreBase.Common.Services.WeatherServices
{
	using Extensions;
	using Interfaces.WeatherService;

	//https://openweathermap.org/api
	public class OpenWeatherService : IWeatherService
	{
		public IWeatherServiceSettings WeatherServiceSettings { get; set; }

		public OpenWeatherService(IWeatherServiceSettings weatherServiceSettings)
		{
			WeatherServiceSettings = weatherServiceSettings;
		}

		/// <summary>
		/// This task gathers threaded weather data from 2 webservice calls asynchronously
		/// Please be sure to handle Aggregate Exceptions in the caller
		/// </summary>
		/// <param name="latitude">The latitude to gather data for</param>
		/// <param name="longitude">The longitude to gather data for</param>
		/// <returns>A <see cref="WeatherData"> Object from the Open Weather Service Api</see>/></returns>
		public async Task<WeatherData> GetWeatherData(double latitude, double longitude)
		{
			Task<DetailedWeather> asyncCurrentWeather = GetCurrentWeather(latitude, longitude);
			Task<ICollection<Forecast>> asyncFutureWeather = GetFutureWeather(latitude, longitude);

			await Task.WhenAll(asyncCurrentWeather, asyncFutureWeather);
			DetailedWeather currentWeather = asyncCurrentWeather.Result;
			Weather weather = currentWeather?.weather?.FirstOrDefault();
			ICollection<Forecast> forecasts = asyncFutureWeather.Result;

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
				WeatherServiceSettings.BaseUri,
				WeatherServiceSettings.CurrentWeatherRelativeUri);


			using (IWebService webService = new RestService())
			{
				HttpRequestMessage httpRequest =
					new HttpRequestMessage(HttpMethod.Get, clientUri);
				HttpResponseMessage response = await webService.SendAsync(httpRequest);
				response.EnsureSuccessStatusCode();
				return await response.ParseJsonResponse<DetailedWeather>();
			}
		}

		private async Task<ICollection<Forecast>> GetFutureWeather(double latitude, double longitude)
		{
			Uri clientUri = new Uri(
				WeatherServiceSettings.BaseUri,
				WeatherServiceSettings.FutureWeatherRelativeUri);

			using (IWebService webService = new RestService())
			{
				HttpRequestMessage httpRequest =
					new HttpRequestMessage(HttpMethod.Get, clientUri);
				HttpResponseMessage response = await webService.SendAsync(httpRequest);
				response.EnsureSuccessStatusCode();
				ThreeHourFiveDayForecast threeHourFiveDayForecast = 
					await response.ParseJsonResponse<ThreeHourFiveDayForecast>();

				return (from item in threeHourFiveDayForecast.forecasts
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
						WindDirection = (int) item.wind.degrees,
						SkyCon = GetSkyCon(weather.icon),
						CloudCover = item.clouds.cloudCover,
						RainVolume = item.rainTotal.threeHourTotal,
						SnowVolume = item.snowTotal.threeHourTotal
					}).ToList();
			}
		}

		//https://openweathermap.org/weather-conditions
		protected virtual string GetSkyCon(string icon)
		{
			switch (icon)
			{
				case "01d":
					return SkyCons.clearDay.ToClientSideString();
				case "01n":
					return SkyCons.clearNight.ToClientSideString();
				case "02d":
				case "04d":
					return SkyCons.partlyCloudyDay.ToClientSideString();
				case "02n":
				case "04n":
					return SkyCons.partlyCloudyNight.ToClientSideString();
				case "03d":
				case "03n":
					return SkyCons.cloudy.ToClientSideString();
				case "09d":
				case "09n":
					return SkyCons.rain.ToClientSideString();
				case "10d":
				case "10n":
					return SkyCons.sleet.ToClientSideString();
				case "13n":
				case "13d":
					return SkyCons.snow.ToClientSideString();
				case "11d":
				case "11n":
					return SkyCons.wind.ToClientSideString();
				case "50n":
				case "50d":
					return SkyCons.fog.ToClientSideString();
				default:
					throw new Exception(
						"Not all Code Paths return a value: " +
						icon);

			}
		}
	}
}