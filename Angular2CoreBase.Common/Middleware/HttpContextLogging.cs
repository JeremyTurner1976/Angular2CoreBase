using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Angular2CoreBase.Common.Middleware
{
	public class HttpContextLogging
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<HttpContextLogging> _logger;

		public HttpContextLogging(RequestDelegate next, ILogger<HttpContextLogging> logger)
		{
			_next = next;
			_logger = logger;
		}

		public async Task Invoke(HttpContext context)
		{
			DateTime startTime = DateTime.UtcNow;

			Stopwatch watch = Stopwatch.StartNew();
			await _next.Invoke(context);
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
				context.Connection.RemoteIpAddress.ToString(),
				context.Request.Path,
				startTime,
				watch.ElapsedMilliseconds,
				context.Request.Body,
				context.Response.Body);
		}
	}
}
