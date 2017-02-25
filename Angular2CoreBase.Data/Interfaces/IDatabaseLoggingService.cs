namespace Angular2CoreBase.Data.Interfaces
{
	using System;
	using Microsoft.Extensions.Logging;

	public interface IDatabaseLoggingService
	{
		void LogError(
			Exception exception,
			string message,
			LogLevel logLevel = LogLevel.None);

		void LogMessage(string subject, string message);
	}
}