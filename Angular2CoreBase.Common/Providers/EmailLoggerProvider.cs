using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angular2CoreBase.Common.Providers
{
	using CommonModels.ConfigSettings;
	using Interfaces;
	using Microsoft.Extensions.Logging;
	using Microsoft.Extensions.Options;
	using Services.LoggingServices;

	public class EmailLoggerProvider : ILoggerProvider
	{
		private readonly Func<string, LogLevel, bool> _filter;
		private readonly IEmailService _mailService;
		private readonly IOptions<EmailSettings> _emailSettings;

		public EmailLoggerProvider(Func<string, LogLevel, bool> filter, IEmailService mailService, IOptions<EmailSettings> emailSettings)
		{
			_mailService = mailService;
			_filter = filter;
			_emailSettings = emailSettings;
		}

		public ILogger CreateLogger(string categoryName)
		{
			return new EmailLoggingService(categoryName, _filter, _mailService, _emailSettings);
		}

		public void Dispose()
		{
		}
	}
}
