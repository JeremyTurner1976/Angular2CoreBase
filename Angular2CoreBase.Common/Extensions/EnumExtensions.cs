using System;

namespace Angular2CoreBase.Common.Extensions
{
	public static class EnumExtensions
	{
		public static string ToNameString(this Enum enumSource)
		{
			Type type = enumSource.GetType();
			return Enum.GetName(type, enumSource);
		}
	}
}
