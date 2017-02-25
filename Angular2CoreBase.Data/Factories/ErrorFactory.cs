namespace Angular2CoreBase.Data.Factories
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Runtime.InteropServices;
	using System.Text;
	using System.Threading.Tasks;
	using Common.Extensions;
	using Models;

	public static class ErrorFactory
	{
		public enum ErrorLevels
		{
			Message,
			Warning,
			Critical,
			Default
		};

		public static Error GetErrorFromException(Exception e, ErrorLevels errorLevel, string strAdditionalInformation)
		{
			Error error = new Error
			{
				Message = e.GetBaseException().Message,
				Source = e.GetBaseException().Source,
				ErrorLevel = Enum.GetName(typeof(ErrorLevels), errorLevel),
				AdditionalInformation = strAdditionalInformation,
				StackTrace = e.StackTrace + Environment.NewLine + (e.InnerException == null 
				? "             |No inner exception| " 
				: "             |Inner Exception| " + e.InnerException.GetErrorAsString()),
				CreatedDateTime = DateTime.UtcNow
			};

			return error;
		}

		public static bool GetThrownException()
		{
			int n = 0;
			int divideByZero = 1 / n;

			return false;
		}

		/// <summary>
		/// Throws an aggregate exception.
		/// </summary>
		/// <returns>An awaitable method that will cause an aggregate exception</returns>
		public static Task<string[][]> GetThrownAggregateException()
		{
			// Get a folder path whose directories should throw an UnauthorizedAccessException. 
			string path = Directory.GetParent(
				Environment.GetEnvironmentVariable(
					RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "LocalAppData" : "Home"))
				.FullName;
			;

			Task<string[]> task = Task<string[]>.Factory.StartNew(() => GetAllFiles(path));
			Task<string[]> taskTwo = Task<string[]>.Factory.StartNew(() => { throw new IndexOutOfRangeException(); });

			Task.WaitAll(task, taskTwo); //waits on all
			return Task.WhenAll(task, taskTwo);
		}


		//helper method for throwing an aggregate exception
		private static string[] GetAllFiles(string str)
		{
			// Should throw an UnauthorizedAccessException exception. 
			return Directory.GetFiles(str, "*.txt", SearchOption.AllDirectories);
		}
	}
}
