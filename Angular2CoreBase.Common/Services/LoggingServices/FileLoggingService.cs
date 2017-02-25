namespace Angular2CoreBase.Common.Services.LoggingServices
{
	using System;
	using System.Text;
	using Abstract;
	using CommonEnums.FileService;

	using Interfaces;
	using Microsoft.Extensions.Logging;

	public class FileLoggingService : BaseLogger
	{
		private readonly IFileService _fileService;

		public FileLoggingService(string categoryName, Func<string, LogLevel, bool> filter, IFileService fileService)
		{
			CategoryName = categoryName;
			Filter = filter;
			_fileService = fileService;
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
			DirectoryFolders folder = GetFolder(logLevel);
			_fileService.SaveTextToDirectoryFile(folder, Environment.NewLine + subject + Environment.NewLine +  message);

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
			DirectoryFolders folder = GetFolder(logLevel);
			_fileService.SaveTextToDirectoryFile(folder, GetFormattedFileOutput(subject, message));
		}

		private string GetFormattedFileOutput(string subject, string message)
			=> Environment.NewLine + DateTime.Now + "  " + subject + Environment.NewLine + message + Environment.NewLine;

		public override void LogError(
			Exception exception,
			string message,
			LogLevel logLevel = LogLevel.None)
		{
			_fileService.SaveTextToDirectoryFile(DirectoryFolders.Errors, GetFormattedFileOutput(errorSubject, message));
		}

		public override void LogMessage(string subject, string message)
		{
			_fileService.SaveTextToDirectoryFile(DirectoryFolders.Logs, GetFormattedFileOutput(messageSubject, message)); 
		}

		public DirectoryFolders GetFolder(LogLevel logLevel)
			=> logLevel < LogLevel.Error
				? DirectoryFolders.Logs
				: DirectoryFolders.Errors;
	}
}
