using System;

namespace Angular2CoreBase.Common.Extensions
{
	public static class DateTimeExtensions
	{
		public static string ToShortDateString(this DateTime dateTime)
		{
			return dateTime.ToString("g");
		}
	}
}
