using System;

namespace Angular2CoreBase.Common.Extensions
{
	public static class LongExtensions
	{
		public static DateTime GetDateTimeFromJavascriptTicks(this long ticks)
		{
			return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddTicks(ticks);
		}
	}
}
