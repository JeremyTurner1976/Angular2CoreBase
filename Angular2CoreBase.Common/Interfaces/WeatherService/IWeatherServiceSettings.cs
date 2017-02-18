namespace Angular2CoreBase.Common.Interfaces.WeatherService
{
	using System;

	public interface IWeatherServiceSettings : IWebServiceSettings
	{
		double? Latitude { get; set; }
		double? Longitude { get; set; }

		string CurrentWeatherRelativeUri { get; }
		string FutureWeatherRelativeUri { get; }
	}
}
