using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angular2CoreBase.Common.Providers
{
	using Interfaces;
	using Microsoft.Extensions.Logging;
	using Services.LoggingServices;

	public class FileLoggerProvider : ILoggerProvider
	{
		private readonly Func<string, LogLevel, bool> _filter;
		private readonly IFileService _fileService;

		public FileLoggerProvider(Func<string, LogLevel, bool> filter, IFileService fileService)
		{
			_fileService = fileService;
			_filter = filter;
		}

		public ILogger CreateLogger(string categoryName)
		{
			return new FileLoggingService(categoryName, _filter, _fileService);
		}

		public void Dispose()
		{
		}
	}
}
