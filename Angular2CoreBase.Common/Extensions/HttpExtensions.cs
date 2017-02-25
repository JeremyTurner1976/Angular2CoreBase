namespace Angular2CoreBase.Common.Extensions
{
	using System.Net.Http;
	using System.Threading.Tasks;
	using Interfaces;
	using Newtonsoft.Json;

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