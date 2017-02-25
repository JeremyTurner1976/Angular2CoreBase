using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angular2CoreBase.Data.Providers
{
	using Common.Interfaces;
	using Common.Services.LoggingServices;
	using Interfaces;
	using Microsoft.Extensions.Logging;
	using Models;
	using Services.LoggingServices;

	public class DatabaseLoggerProvider : ILoggerProvider
	{
		private readonly Func<string, LogLevel, bool> _filter;
		private readonly IRepository<Error> _errorRepository;
		private readonly ITrackedModelDecorator<Error> _errorDecorator;

		public DatabaseLoggerProvider(Func<string, LogLevel, bool> filter, IRepository<Error> errorRepository, ITrackedModelDecorator<Error> errorDecorator)
		{
			_errorRepository = errorRepository;
			_filter = filter;
			_errorDecorator = errorDecorator;
		}

		public ILogger CreateLogger(string categoryName)
		{
			return new DatabaseLoggingService(categoryName, _filter, _errorRepository, _errorDecorator);
		}

		public void Dispose()
		{
		}
	}
}
