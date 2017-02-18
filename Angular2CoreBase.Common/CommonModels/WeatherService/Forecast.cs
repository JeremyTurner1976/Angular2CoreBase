﻿using System;

namespace Angular2CoreBase.Common.CommonModels.WeatherService
{
	using Interfaces;
	public class Forecast
	{
		public DateTime StartDateTime { get; set; }

		public DateTime EndDateTime { get; set; }

		public string Description { get; set; }

		public double Temperature { get; set; }

		public double MinimumTemperature { get; set; }

		public double MaximumTemperature { get; set; }

		public double Humidity { get; set; }

		public double AtmosphericPressure { get; set; }

		public double Windspeed { get; set; }

		public int WindDirection { get; set; }

		public string SkyCon { get; set; }

		public double CloudCover { get; set; }

		private bool Precipitation =>
			RainVolume > 0 || SnowVolume > 0;

		public double RainVolume { get; set; }

		public double SnowVolume { get; set; }

	}
}

