using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angular2CoreBase.Common.Extensions
{
	using Interfaces;
	using Microsoft.Extensions.Logging;
	using Providers;

	public static class ILoggerFactoryExtensions
	{
		public static ILoggerFactory AddEmail(this ILoggerFactory factory,
									  IEmailService mailService,
									  Func<string, LogLevel, bool> filter = null)
		{
			factory.AddProvider(new EmailLoggerProvider(filter, mailService));
			return factory;
		}

		public static ILoggerFactory AddEmail(this ILoggerFactory factory,
			IEmailService mailService, 
			LogLevel minLevel)
		{
			return AddEmail(
				factory,
				mailService,
				(_, logLevel) => logLevel >= minLevel);
		}

		public static ILoggerFactory AddFile(this ILoggerFactory factory,
							  IFileService fileservice,
							  Func<string, LogLevel, bool> filter = null)
		{
			factory.AddProvider(new FileLoggerProvider(filter, fileservice));
			return factory;
		}

		public static ILoggerFactory AddFile(this ILoggerFactory factory,
			IFileService fileservice,
			LogLevel minLevel)
		{
			return AddFile(
				factory,
				fileservice,
				(_, logLevel) => logLevel >= minLevel);
		}
	}
}
