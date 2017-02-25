using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angular2CoreBase.Common.Providers
{
	using Interfaces;
	using Microsoft.Extensions.Logging;
	using Services.LoggingServices;

	public class EmailLoggerProvider : ILoggerProvider
	{
		private readonly Func<string, LogLevel, bool> _filter;
		private readonly IEmailService _mailService;

		public EmailLoggerProvider(Func<string, LogLevel, bool> filter, IEmailService mailService)
		{
			_mailService = mailService;
			_filter = filter;
		}

		public ILogger CreateLogger(string categoryName)
		{
			return new EmailLoggingService(categoryName, _filter, _mailService);
		}

		public void Dispose()
		{
		}
	}
}
