namespace Angular2CoreBase.Data.Services.LoggingServices
{
	using System;
	using Common.Abstract;
	using Common.CommonEnums.FileService;
	using Common.Extensions;
	using Factories;
	using Interfaces;
	using Microsoft.Extensions.Logging;
	using Models;

	public class DatabaseLoggingService : BaseLogger, IDatabaseLoggingService
	{
		private readonly ITrackedModelDecorator<Error> _errorDecorator;
		private readonly IRepository<Error> _errorRepository;

		public DatabaseLoggingService(string categoryName, Func<string, LogLevel, bool> filter,
			IRepository<Error> errorRepository,
			ITrackedModelDecorator<Error> errorDecorator)
		{
			CategoryName = categoryName;
			Filter = filter;
			_errorRepository = errorRepository;
			_errorDecorator = errorDecorator;
		}

		public override void LogError(
			Exception exception,
			string message,
			LogLevel logLevel = LogLevel.None)
		{
			LogErrorAndSave(exception, message, logLevel);
		}

		public override void LogMessage(string subject, string message)
		{
			Error error = ErrorFactory.GetErrorFromDetails(subject, message, LogLevel.Information.ToNameString());
			_errorRepository.Add(error);
			_errorRepository.SaveChanges();
		}


		public override void Log(LogLevel logLevel, int eventId, object state, Exception exception,
			Func<object, Exception, string> formatter)
		{
			string message = VerifyAndGenerateMessage(logLevel, state, exception, formatter);

			if (string.IsNullOrWhiteSpace(message))
			{
				return;
			}

			LogErrorAndSave(exception, message, logLevel);
		}

		public override void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception,
			Func<TState, Exception, string> formatter)
		{
			string message = VerifyAndGenerateMessage(logLevel, state, exception, formatter);

			if (string.IsNullOrWhiteSpace(message))
			{
				return;
			}

			LogErrorAndSave(exception, message, logLevel);
		}

		private void LogErrorAndSave(
			Exception exception,
			string message,
			LogLevel logLevel = LogLevel.None)
		{
			Error error = ErrorFactory.GetErrorFromException(exception, logLevel, message);
			//TODO handle application users and id, for now just logging to the System Admin 
			error = _errorDecorator.GetDecoratedModel(error, 1);
			_errorRepository.Add(error);
			_errorRepository.SaveChanges();
		}

		public DirectoryFolders GetFolder(LogLevel logLevel)
			=> logLevel <= LogLevel.Error
				? DirectoryFolders.Logs
				: DirectoryFolders.Errors;
	}
}