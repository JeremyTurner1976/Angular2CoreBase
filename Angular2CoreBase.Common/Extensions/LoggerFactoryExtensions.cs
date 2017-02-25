using System;
using System.Collections.Generic;
using System.Linq;
namespace Angular2CoreBase.Common.Extensions
{
	using Interfaces;
	using Microsoft.Extensions.Logging;
	using Providers;

	public static class LoggerFactoryExtensions
	{
		public static ILoggerFactory AddEmailLogger(
			this ILoggerFactory factory,
			IEmailService mailService,
			Func<string, LogLevel, bool> filter = null)
		{
			factory.AddProvider(new EmailLoggerProvider(filter, mailService));
			return factory;
		}

		public static ILoggerFactory AddEmailLogger(
			this ILoggerFactory factory,
			IEmailService mailService, 
			LogLevel minLevel)
		{
			return AddEmailLogger(
				factory,
				mailService,
				(_, logLevel) => logLevel >= minLevel);
		}

		public static ILoggerFactory AddFileLogger(
			this ILoggerFactory factory,
			IFileService fileservice,
			Func<string, LogLevel, bool> filter = null)
		{
			factory.AddProvider(new FileLoggerProvider(filter, fileservice));
			return factory;
		}

		public static ILoggerFactory AddFileLogger(
			this ILoggerFactory factory,
			IFileService fileservice,
			LogLevel minLevel)
		{
			return AddFileLogger(
				factory,
				fileservice,
				(_, logLevel) => logLevel >= minLevel);
		}
	}
}
