using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Angular2CoreBase.Common.Extensions;
using static Angular2CoreBase.Common.CustomAttributes.EnumAttributes;

namespace Angular2CoreBase.Common.CommonModels.WeatherService
{
	//https://github.com/darkskyapp/skycons

	public enum Icons
	{
		[FileName(IconFileNames.ClearSkyDay)]
		ClearSkyDay,
		[FileName(IconFileNames.ClearSkyNight)]
		ClearSkyNight,
		[FileName(IconFileNames.PartlyCloudyDay)]
		PartlyCloudDay,
		[FileName(IconFileNames.PartlyCloudyNight)]
		PartlyCloudyNight,
		[FileName(IconFileNames.ScatteredCloudsDay)]
		ScatteredCloudsDay,
		[FileName(IconFileNames.ScatteredCloudsNight)]
		ScatteredCloudsNight,
		[FileName(IconFileNames.BrokenCloudsDay)]
		BrokenCloudsDay,
		[FileName(IconFileNames.BrokenCloudsNight)]
		BrokenCloudsNight,
		[FileName(IconFileNames.ShoweringRainDay)]
		ShoweringRainDay,
		[FileName(IconFileNames.ShoweringRainNight)]
		ShoweringRainNight,
		[FileName(IconFileNames.RainDay)]
		RainDay,
		[FileName(IconFileNames.RainNight)]
		RainNight,
		[FileName(IconFileNames.ThunderStormDay)]
		ThunderStormDay,
		[FileName(IconFileNames.ThunderStormNight)]
		ThunderStormNight,
		[FileName(IconFileNames.SnowDay)]
		SnowDay,
		[FileName(IconFileNames.SnowNight)]
		SnowNight,
		[FileName(IconFileNames.MistDay)]
		MistDay,
		[FileName(IconFileNames.MistNight)]
		MistNight
	}

	//All are png extensions
	public static class IconFileNames
	{
		public const string ClearSkyDay = "01d";
		public const string ClearSkyNight = "01n";
		public const string PartlyCloudyDay = "02d";
		public const string PartlyCloudyNight = "02n";
		public const string ScatteredCloudsDay = "03d";
		public const string ScatteredCloudsNight = "03n";
		public const string BrokenCloudsDay = "04d";
		public const string BrokenCloudsNight = "04n";
		public const string ShoweringRainDay = "09d";
		public const string ShoweringRainNight = "09n";
		public const string RainDay = "10d";
		public const string RainNight = "10n";
		public const string ThunderStormDay = "11d";
		public const string ThunderStormNight = "11n";
		public const string SnowDay = "13d";
		public const string SnowNight = "13n";
		public const string MistDay = "50d";
		public const string MistNight = "50n";
	}
}
