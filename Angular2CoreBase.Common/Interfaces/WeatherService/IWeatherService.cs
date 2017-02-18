namespace Angular2CoreBase.Common.Interfaces.WeatherService
{
	using System.Threading.Tasks;
	using CommonModels.WeatherService;

	public interface IWeatherService
	{
		Task<WeatherData> GetWeatherData(double latitude, double longitude);
	}
}
