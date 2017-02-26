namespace Angular2CoreBase.Common.CommonModels.ConfigSettings.LogSettings
{
	using CommonEnums.ConfigSettings;
	using Microsoft.Extensions.Logging;
	using Newtonsoft.Json;
	using Newtonsoft.Json.Converters;

	public class LogSetting
	{
		[JsonConverter(typeof(StringEnumConverter))]
		public LogType Type { get; set; }

		[JsonConverter(typeof(StringEnumConverter))]
		public LogLevel Level { get; set;  }

		public bool On { get; set; } = false;
	}
}
