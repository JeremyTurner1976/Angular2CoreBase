using System.Threading.Tasks;
using Angular2CoreBase.Common.CommonModels.WeatherService;

namespace Angular2CoreBase.Common.Interfaces
{
	public interface IWeatherService
	{
		Task<WeatherData> GetWeatherData(double latitude, double longitude);
	}
}
