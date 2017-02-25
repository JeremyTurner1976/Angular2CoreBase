using System;

namespace Angular2CoreBase.Common.Extensions
{
	public static class DoubleExtensions
	{
		public static double ToPrecisionValue(this double value, int precision)
		{
			return Math.Round(value, precision);
		}
	}
}
