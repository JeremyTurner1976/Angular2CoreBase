namespace Angular2CoreBase.Common.CommonModels.WeatherService.OpenWeather
{
	//http://json2csharp.com/
	using System;
	using System.Collections.Generic;
	using Interfaces;
	using Newtonsoft.Json;

	public class DetailedWeather
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

//OpenWeather Current https://openweathermap.org/current
/*
coord
	coord.lon City geo location, longitude
	coord.lat City geo location, latitude
weather (more info Weather condition codes)
	weather.id Weather condition id
	weather.main Group of weather parameters (Rain, Snow, Extreme etc.)
	weather.description Weather condition within the group
	weather.icon Weather icon id
	base Internal parameter
main
	main.temp Temperature. Unit Default: Kelvin, Metric: Celsius, Imperial: Fahrenheit.
	main.pressure Atmospheric pressure (on the sea level, if there is no sea_level or grnd_level data), hPa
	main.humidity Humidity, %
	main.temp_min Minimum temperature at the moment. This is deviation from current temp that is possible for large cities and megalopolises geographically expanded (use these parameter optionally). Unit Default: Kelvin, Metric: Celsius, Imperial: Fahrenheit.
	main.temp_max Maximum temperature at the moment. This is deviation from current temp that is possible for large cities and megalopolises geographically expanded (use these parameter optionally). Unit Default: Kelvin, Metric: Celsius, Imperial: Fahrenheit.
	main.sea_level Atmospheric pressure on the sea level, hPa
	main.grnd_level Atmospheric pressure on the ground level, hPa
wind
	wind.speed Wind speed. Unit Default: meter/sec, Metric: meter/sec, Imperial: miles/hour.
	wind.deg Wind direction, degrees (meteorological)
clouds
	clouds.all Cloudiness, %
rain
	rain.3h Rain volume for the last 3 hours
snow
	snow.3h Snow volume for the last 3 hours
	dt Time of data calculation, unix, UTC
sys
	sys.type Internal parameter
	sys.id Internal parameter
	sys.message Internal parameter
	sys.country Country code (GB, JP etc.)
	sys.sunrise Sunrise time, unix, UTC
	sys.sunset Sunset time, unix, UTC
	id City ID
	name City name
	cod Internal parameter
*/

//Open Weather - Weather Response
/*
	{
	   "coord": {
		  "lon": -114.07,
		  "lat": 48.06
	   },
	   "weather": [
		  {
			 "id": 803,
			 "main": "Clouds",
			 "description": "broken clouds",
			 "icon": "04n"
		  }
	   ],
	   "base": "stations",
	   "main": {
		  "temp": -0.8,
		  "pressure": 891.16,
		  "humidity": 67,
		  "temp_min": -0.8,
		  "temp_max": -0.8,
		  "sea_level": 1044.7,
		  "grnd_level": 891.16
	   },
	   "wind": {
		  "speed": 2.73,
		  "deg": 243.501
	   },
	   "clouds": {
		  "all": 76
	   },
	   "dt": 1486862107,
	   "sys": {
		  "message": 0.6324,
		  "country": "US",
		  "sunrise": 1486910824,
		  "sunset": 1486947276
	   },
	   "id": 5640284,
	   "name": "Bigfork",
	   "cod": 200
	}
 */
