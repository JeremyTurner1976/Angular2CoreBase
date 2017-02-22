using System.Threading.Tasks;

namespace Angular2CoreBase.Common.Extensions
{
	using System.Net.Http;
	using Interfaces;
	using Newtonsoft.Json;
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Http.Features;
	using System;

	public static class HttpExtensions
	{
		public static async Task<T> ParseJsonResponse<T>(this HttpResponseMessage response) 
			where T : IModel
		{
			string stringResult = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<T>(stringResult);
		}
	}
}
