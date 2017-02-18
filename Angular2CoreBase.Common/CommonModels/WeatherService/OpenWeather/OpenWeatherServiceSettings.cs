namespace Angular2CoreBase.Common.Services.WeatherServices
{
	using System;
	using Extensions;
	using Interfaces;

	public class OpenWeatherServiceSettings : IWeatherServiceSettings
	{
		//TODO Remove this when I have Latitude/Longitude
		public string City { get; } = "Bigfork,Mt";

		private string AppId { get; } = "cc9bb5f7256330c1e1a0f5ba66ccadcd";
		private string CurrentWeatherUri { get; } = "/weather";
		private string FutureWeatherUri { get; } = "/forecast";
		public string ApiRoot { get; } = "/data/";

		public enum UnitsOfMeasurement { imperial, metric }
		public UnitsOfMeasurement UnitOfMeasurement { get; set; } = UnitsOfMeasurement.imperial;
		public double Version { get; set; } = 2.5;


		public Uri GetBaseUri()
		{
			return new Uri("http://api.openweathermap.org");
		}

		public string GetCurrentWeatherRelativeUri()
		{
			return ApiRoot +
			       $"{Version}" +
			       CurrentWeatherUri +
			       "?q=" + City +
			       "&appid=" + AppId +
			       "&units=" + UnitOfMeasurement.ToNameString();
		}

		public string GetFutureWeatherRelativeUri()
		{
			return ApiRoot +
					$"{Version}" +
					FutureWeatherUri +
					"?q=" + City +
					"&appid=" + AppId +
					"&units=" + UnitOfMeasurement.ToNameString();
		}
	}
}