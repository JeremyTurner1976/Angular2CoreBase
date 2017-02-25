namespace Angular2CoreBase.Data.Extensions
{
	using System;
	using Interfaces;
	using Microsoft.Extensions.Logging;
	using Models;
	using Providers;

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