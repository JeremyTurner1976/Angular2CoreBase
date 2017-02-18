using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angular2CoreBase.Common.Extensions
{
	using System.Net.Http;
	using Abstract;
	using CommonModels.WeatherService.OpenWeather;
	using Newtonsoft.Json;

	public static class HttpResponseExtensions
	{
		public static async Task<T> ParseJsonResponse<T>(this HttpResponseMessage response) 
			where T : WebServiceModelBase
		{
			string stringResult = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<T>(stringResult);
		}

	}
}
