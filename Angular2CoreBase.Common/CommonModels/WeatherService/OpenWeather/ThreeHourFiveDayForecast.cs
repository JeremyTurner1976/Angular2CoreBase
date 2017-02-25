namespace Angular2CoreBase.Common.CommonModels.WeatherService.OpenWeather
{
	//http://json2csharp.com/
	//https://openweathermap.org/forecast5
	using System.Collections.Generic;
	using Interfaces;
	using Newtonsoft.Json;

	public class ThreeHourFiveDayForecast : IModel
	{
		public double message { get; set; }

		[JsonProperty(PropertyName = "cnt")]
		public int listCount { get; set; }

		[JsonProperty(PropertyName = "list")]
		public List<List> forecasts { get; set; }
	}
}