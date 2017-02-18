using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Angular2CoreBase.Common.Extensions;

namespace Angular2CoreBase.Common.CommonModels.WeatherService
{
	//https://github.com/darkskyapp/skycons

	public enum SkyCons
	{
		[SkyConString("clear-day")]
		clearDay,
		[SkyConString("clear-night")]
		clearNight,
		[SkyConString("partly-cloudy-day")]
		partlyCloudyDay,
		[SkyConString("partly-cloudy-night")]
		partlyCloudyNight,
		cloudy,
		rain,
		sleet,
		snow,
		wind,
		fog
	}

	internal class SkyConString : Attribute
	{
		public string ClientSideIdentifier;

		public SkyConString(string value)
		{
			ClientSideIdentifier = value;
		}
	}

	internal static class SkyConExtensions
	{
		public static string ToClientSideString(this SkyCons skycon)
		{
			Type type = skycon.GetType();
			MemberInfo[] memInfo = type.GetMember(skycon.ToString());

			if (memInfo != null && memInfo.Length > 0)
			{
				IEnumerable<Attribute> attrs = memInfo[0].GetCustomAttributes(typeof(SkyConString), false);

				Attribute[] attributes = attrs as Attribute[] ?? attrs.ToArray();
				if (attrs != null && attributes.Any())
				{
					return ((SkyConString)attributes[0]).ClientSideIdentifier;
				}
			}

			return skycon.ToNameString();
		}
	}
}
