namespace Angular2CoreBase.Common.Services.WeatherServices
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Net.Http;
	using System.Threading.Tasks;
	using CommonModels.WeatherService;
	using CommonModels.WeatherService.DarkSkyWeather;
	using Extensions;
	using Interfaces;
	using Interfaces.WeatherService;

	//GET https://api.darksky.net/forecast/0123456789abcdef9876543210fedcba/42.3601,-71.0589
	public class DarkSkyWeatherService : IWeatherService
	{
		public IWeatherServiceSettings WeatherServiceSettings { get; set; }

		public DarkSkyWeatherService(IWeatherServiceSettings weatherServiceSettings)
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
			DarkSkyWeather darkSkyWeather = await GetWeather(latitude, longitude);
			if (darkSkyWeather != null)
			{
				return new WeatherData()
				{
					Description = darkSkyWeather.currently.summary,
					Sunrise = darkSkyWeather.daily.data.FirstOrDefault().sunrise,
					Sunset = darkSkyWeather.daily.data.FirstOrDefault().sunset,
					WeatherForecasts = GetDarkSkyForecasts(darkSkyWeather)
				};
			}

			throw new ArgumentNullException(nameof(darkSkyWeather));
		}

		private async Task<DarkSkyWeather> GetWeather(double latitude, double longitude)
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
				return await response.ParseJsonResponse<DarkSkyWeather>();
			}
		}

		private ICollection<Forecast> GetDarkSkyForecasts(DarkSkyWeather darkSkyWeather)
		{
			return new List<Forecast>();
		}
	}
}
