namespace Angular2CoreBase.Common.Interfaces.WeatherService
{
	using System;

	public interface IWeatherServiceSettings
	{
		Uri GetBaseUri();
		string GetCurrentWeatherRelativeUri();
		string GetFutureWeatherRelativeUri();
	}
}
