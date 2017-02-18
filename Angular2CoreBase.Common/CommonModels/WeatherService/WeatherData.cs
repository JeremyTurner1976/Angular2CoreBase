using System;
using System.Collections.Generic;

namespace Angular2CoreBase.Common.CommonModels.WeatherService
{
	using Interfaces;

	public class WeatherData
	{
		public string Description { get; set; }

		public DateTime Sunrise { get; set; }

		public DateTime Sunset { get; set; }

		public string City { get; set; }

		public string Country { get; set; }

		public ICollection<Forecast> WeatherForecasts { get; set; } 
	}
}
