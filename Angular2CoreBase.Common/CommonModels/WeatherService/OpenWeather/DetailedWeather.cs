namespace Angular2CoreBase.Common.CommonModels.WeatherService.OpenWeather
{
	//http://json2csharp.com/
	//https://openweathermap.org/current
	using System.Collections.Generic;
	using Interfaces;
	using Newtonsoft.Json;

	public class DetailedWeather : ICommonModel
	{
		public List<Weather> weather { get; set; }

		public Main main { get; set; }

		public int visibility { get; set; }

		public Wind wind { get; set; }

		public Clouds clouds { get; set; }

		[JsonProperty(PropertyName = "sys")]
		public Sys systemInformation { get; set; }

		[JsonProperty(PropertyName = "name")]
		public string city { get; set; }

		[JsonProperty(PropertyName = "rain")]
		public Rain rainTotal { get; set; }

		[JsonProperty(PropertyName = "snow")]
		public Snow snowTotal { get; set; }
	}
}