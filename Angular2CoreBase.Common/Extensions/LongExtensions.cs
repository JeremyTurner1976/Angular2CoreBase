using System;

namespace Angular2CoreBase.Common.Extensions
{
	public static class LongExtensions
	{
		public static DateTime GetDateTimeFromTicks(this long ticks)
		{
			return new DateTime(ticks);
		}
	}
}
