﻿using System;
namespace Angular2CoreBase.Common.CommonModels.WeatherService.OpenWeather
{
	using Newtonsoft.Json;
	using System.Collections.Generic;
	using Extensions;

	public class Main
	{
		[JsonProperty(PropertyName = "temp")]
		public double temperature { get; set; }

		[JsonProperty(PropertyName = "temp_min")]
		public double minimumTemperature { get; set; }

		[JsonProperty(PropertyName = "temp_max")]
		public double maximumTemperature { get; set; }

		[JsonProperty(PropertyName = "pressure")]
		public double pressure { get; set; }

		public int humidity { get; set; }
	}

	public class Weather
	{
		public string description { get; set; }

		public string icon { get; set; }
	}

	public class Clouds
	{
		[JsonProperty(PropertyName = "all")]
		public int cloudCover { get; set; }
	}

	public class Wind
	{
		public double speed { get; set; }

		[JsonProperty(PropertyName = "deg")]
		public double degrees { get; set; }
	}


	public class Rain
	{
		[JsonProperty(PropertyName = "3h")]
		public double threeHourTotal { get; set; }
	}

	public class Snow
	{
		[JsonProperty(PropertyName = "3h")]
		public double threeHourTotal { get; set; }
	}

	public class List
	{
		[JsonProperty(PropertyName = "dt")]
		public long dateTime { get; set; }

		public DateTime startDateTime
			=> dateTime.GetDateTimeFromTicks();

		public Main main { get; set; }

		public List<Weather> weather { get; set; }

		public Clouds clouds { get; set; }

		public Wind wind { get; set; }

		[JsonProperty(PropertyName = "rain")]
		public Rain rainTotal { get; set; }

		[JsonProperty(PropertyName = "snow")]
		public Snow snowTotal { get; set; }
	}

	public class Coord
	{
		[JsonProperty(PropertyName = "lat")]
		public double latitude { get; set; }

		[JsonProperty(PropertyName = "lon")]
		public double longitude { get; set; }
	}

	public class Sys
	{
		public string country { get; set; }

		public long Sunrise { get; set; }

		public long Sunset { get; set; }

		public DateTime sunriseTime
			=> Sunrise.GetDateTimeFromTicks();

		public DateTime sunsetTime
			=> Sunset.GetDateTimeFromTicks();
	}
}