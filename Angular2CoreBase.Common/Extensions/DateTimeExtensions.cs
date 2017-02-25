namespace Angular2CoreBase.Common.Extensions
{
	using System;

	public static class DateTimeExtensions
	{
		public static string ToShortDateString(this DateTime dateTime)
		{
			return dateTime.ToString("g");
		}
	}
}