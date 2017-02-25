namespace Angular2CoreBase.Common.Services.LoggingServices
{
	using System;
	using Abstract;
	using Extensions;
	using Interfaces;
	using Microsoft.Extensions.Logging;

	public class EmailLoggingService : BaseLogger
	{
		private readonly IEmailService _mailService;

		public EmailLoggingService(string categoryName, Func<string, LogLevel, bool> filter, IEmailService mailService)
		{
			CategoryName = categoryName;
			Filter = filter;
			_mailService = mailService;
		}


		public override void Log(LogLevel logLevel, int eventId, object state, Exception exception,
			Func<object, Exception, string> formatter)
		{
			string message = VerifyAndGenerateMessage(logLevel, state, exception, formatter);

			if (string.IsNullOrWhiteSpace(message))
			{
				return;
			}

			string subject = GetSubject(logLevel);
			_mailService.SendMail("363015fdfa2f4211b9d42ee5cf@gmail.com", null, null, subject, message);

		}

		public override void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception,
			Func<TState, Exception, string> formatter)
		{
			string message = VerifyAndGenerateMessage(logLevel, state, exception, formatter);

			if (string.IsNullOrWhiteSpace(message))
			{
				return;
			}

			string subject = GetSubject(logLevel);
			_mailService.SendMail("363015fdfa2f4211b9d42ee5cf@gmail.com", null, null, subject, message);
		}

		public override void LogError(
			Exception exception,
			string message,
			LogLevel logLevel = LogLevel.None)
		{
			_mailService.SendMail("363015fdfa2f4211b9d42ee5cf@gmail.com", null, null, errorSubject, message);
		}

		public override void LogMessage(string subject, string message)
		{
			_mailService.SendMail("363015fdfa2f4211b9d42ee5cf@gmail.com", null, null, subject, message);
		}
	}
}
