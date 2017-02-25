namespace Angular2CoreBase.Data.Extensions
{
	using System;
	using Data.Interfaces;
	using Data.Models;
	using Data.Providers;
	using Microsoft.Extensions.Logging;

	public static class DatabaseLoggerFactoryExtensions
	{
		public static ILoggerFactory AddDatabaseLogger(
			this ILoggerFactory factory,
			IRepository<Error> errorRepository,
			ITrackedModelDecorator<Error> errorDecorator,
			Func<string, LogLevel, bool> filter = null)
		{
			factory.AddProvider(new DatabaseLoggerProvider(filter, errorRepository, errorDecorator));
			return factory;
		}

		public static ILoggerFactory AddDatabaseLogger
			(this ILoggerFactory factory,
			IRepository<Error> errorRepository,
			ITrackedModelDecorator<Error> errorDecorator,
			LogLevel minLevel)
		{
			return AddDatabaseLogger(
				factory,
				errorRepository,
				errorDecorator,
				(_, logLevel) => logLevel >= minLevel);
		}
	}
}
