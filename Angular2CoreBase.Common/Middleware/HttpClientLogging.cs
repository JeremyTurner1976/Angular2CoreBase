using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Angular2CoreBase.Common.Middleware
{
	//https://docs.microsoft.com/en-us/aspnet/web-api/overview/advanced/httpclient-message-handlers
	public class HttpClientLogging : DelegatingHandler
	{
		private readonly ILogger<HttpClientLogging> _logger;

		public HttpClientLogging(ILogger<HttpClientLogging> logger)
		{
			_logger = logger;
		}

		protected override async Task<HttpResponseMessage> SendAsync(
			HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
		{

			DateTime startTime = DateTime.UtcNow;

			Stopwatch watch = Stopwatch.StartNew();
			var response = await base.SendAsync(request, cancellationToken);
			watch.Stop();

			string logTemplate = @"
				Client IP: {clientIP}
				Request path: {requestPath}
				Start time: {startTime}
				Duration: {duration} ms
				Request: {request}
				Response: {response}";

			_logger.LogInformation(
				logTemplate,
				request.RequestUri,
				response.StatusCode,
				startTime,
				watch.ElapsedMilliseconds,
				request,
				response);

			return response;
		}
	}
}
