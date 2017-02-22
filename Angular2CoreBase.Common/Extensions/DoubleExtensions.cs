using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
