using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angular2CoreBase.Common.Interfaces
{
	public interface IWeatherServiceSettings
	{
		Uri GetBaseUri();
		string GetCurrentWeatherRelativeUri();
		string GetFutureWeatherRelativeUri();
	}
}
