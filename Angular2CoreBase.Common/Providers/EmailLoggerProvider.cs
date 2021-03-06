﻿namespace Angular2CoreBase.Common.Providers
{
	using System;
	using CommonModels.ConfigSettings;
	using Interfaces;
	using Microsoft.Extensions.Logging;
	using Microsoft.Extensions.Options;
	using Services.LoggingServices;

	public class EmailLoggerProvider : ILoggerProvider
	{
		private readonly IOptions<EmailSettings> _emailSettings;
		private readonly Func<string, LogLevel, bool> _filter;
		private readonly IEmailService _mailService;

		public EmailLoggerProvider(Func<string, LogLevel, bool> filter, IEmailService mailService,
			IOptions<EmailSettings> emailSettings)
		{
			_mailService = mailService;
			_filter = filter;
			_emailSettings = emailSettings;
		}

		public void Dispose()
		{
		}

		public ILogger CreateLogger(string categoryName)
		{
			return new EmailLoggingService(categoryName, _filter, _mailService, _emailSettings);
		}
	}
}