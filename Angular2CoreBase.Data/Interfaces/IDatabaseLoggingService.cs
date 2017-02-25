using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angular2CoreBase.Data.Interfaces
{
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
