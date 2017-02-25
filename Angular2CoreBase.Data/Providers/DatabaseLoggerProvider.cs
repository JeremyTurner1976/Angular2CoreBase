namespace Angular2CoreBase.Data.Providers
{
	using System;
	using Interfaces;
	using Microsoft.Extensions.Logging;
	using Models;
	using Services.LoggingServices;

	public class DatabaseLoggerProvider : ILoggerProvider
	{
		private readonly ITrackedModelDecorator<Error> _errorDecorator;
		private readonly IRepository<Error> _errorRepository;
		private readonly Func<string, LogLevel, bool> _filter;

		public DatabaseLoggerProvider(Func<string, LogLevel, bool> filter, IRepository<Error> errorRepository,
			ITrackedModelDecorator<Error> errorDecorator)
		{
			_errorRepository = errorRepository;
			_filter = filter;
			_errorDecorator = errorDecorator;
		}

		public void Dispose()
		{
		}

		public ILogger CreateLogger(string categoryName)
		{
			return new DatabaseLoggingService(categoryName, _filter, _errorRepository, _errorDecorator);
		}
	}
}