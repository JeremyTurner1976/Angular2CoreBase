using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Angular2CoreBase.Common.Extensions;
using static Angular2CoreBase.Common.CustomAttributes.EnumAttributes;

namespace Angular2CoreBase.Common.CommonModels.WeatherService
{
	//https://github.com/darkskyapp/skycons

	public enum SkyCons
	{
		[ClientSideString(SkyConClientSideStrings.ClearDay)]
		ClearDay,
		[ClientSideString(SkyConClientSideStrings.ClearNight)]
		ClearNight,
		[ClientSideString(SkyConClientSideStrings.PartlyCloudyDay)]
		PartlyCloudyDay,
		[ClientSideString(SkyConClientSideStrings.PartlyCloudyNight)]
		PartlyCloudyNight,
		[ClientSideString(SkyConClientSideStrings.Cloudy)]
		Cloudy,
		[ClientSideString(SkyConClientSideStrings.Rain)]
		Rain,
		[ClientSideString(SkyConClientSideStrings.Sleet)]
		Sleet,
		[ClientSideString(SkyConClientSideStrings.Snow)]
		Snow,
		[ClientSideString(SkyConClientSideStrings.Wind)]
		Wind,
		[ClientSideString(SkyConClientSideStrings.Fog)]
		Fog
	}

	public static class SkyConClientSideStrings
	{
		public const string ClearDay = "clear-day";
		public const string ClearNight = "clear-night";
		public const string PartlyCloudyDay = "partly-cloudy-day";
		public const string PartlyCloudyNight = "partly-cloudy-night";

		public const string Cloudy = "cloudy";
		public const string Rain = "rain";
		public const string Sleet = "sleet";
		public const string Snow = "snow";
		public const string Wind = "wind";
		public const string Fog = "fog";
	}
}