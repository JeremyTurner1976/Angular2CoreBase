namespace Angular2CoreBase.Common.CommonModels.WeatherService.OpenWeather
{
	using System;
	using Extensions;
	using Interfaces;
	using Interfaces.WeatherService;

	public class OpenWeatherServiceSettings : IWeatherServiceSettings
	{
		//https://openweathermap.org/current
		//https://openweathermap.org/forecast5

		//TODO Remove this when I have Latitude/Longitude
		public string City { get; } = "Bigfork,Mt";

		public double? Latitude { get; set; } = 42.3601;
		public double? Longitude { get; set; } = -71.0589;

		private string AppId { get; } = "cc9bb5f7256330c1e1a0f5ba66ccadcd";
		private string CurrentWeatherUri { get; } = "/weather";
		private string FutureWeatherUri { get; } = "/forecast";

		public string ApiRoot { get; } = "/data/";
		public double? Version { get; set; } = 2.5;
		public enum UnitsOfMeasurement { imperial, metric }
		public UnitsOfMeasurement UnitOfMeasurement { get; set; } = UnitsOfMeasurement.imperial;

		public Uri BaseUri
			=> new Uri("http://api.openweathermap.org");

		public string CurrentWeatherRelativeUri
			=> ApiRoot +
				$"{Version}" +
				CurrentWeatherUri +
				"?q=" + City +
				"&appid=" + AppId +
				"&units=" + UnitOfMeasurement.ToNameString();

		public string FutureWeatherRelativeUri
			=> ApiRoot +
				$"{Version}" +
				FutureWeatherUri +
				"?q=" + City +
				"&appid=" + AppId +
				"&units=" + UnitOfMeasurement.ToNameString();

	}
}