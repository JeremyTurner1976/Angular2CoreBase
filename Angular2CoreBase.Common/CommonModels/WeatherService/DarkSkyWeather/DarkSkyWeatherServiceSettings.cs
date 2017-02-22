using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angular2CoreBase.Common.CommonModels.WeatherService.DarkSkyWeather
{

	//TODO Icon -> SkyCons (Both sides)
	//Map forecasts

	//notes on ui side, look at base page for ideas
	//Very rich weather services
	//promises for lat long geoservice
	
	using Interfaces.WeatherService;
	public class DarkSkyWeatherServiceSettings : IWeatherServiceSettings
	{
		//GET https://api.darksky.net/forecast/0123456789abcdef9876543210fedcba/42.3601,-71.0589

		private string AppId { get; } = "e12ee9c793d46ae9083d29a252496c7b";
		public Uri BaseUri { get; } = new Uri("https://api.darksky.net");
		public string ApiRoot { get; } = "/forecast/";

		public string ExtendHourly { get; } = "?extend=hourly";
		public double? Version { get; } = null;
		public double? Latitude { get; set; } = 42.3601;
		public double? Longitude { get; set; } = -71.0589;

		public string CurrentWeatherRelativeUri
			=> ApiRoot + AppId +
			   "/" + Latitude + "," + Longitude +
			ExtendHourly;

		public string FutureWeatherRelativeUri { get; set; }
	}
}
