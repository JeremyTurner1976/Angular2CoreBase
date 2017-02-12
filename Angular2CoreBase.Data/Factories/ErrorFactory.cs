using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Angular2CoreBase.Data.Models;

namespace Angular2CoreBase.Data.Factories
{
public enum ErrorLevels
	{
		Message,
		Warning,
		Critical,
		Default
	};

	public static class ErrorFactory
	{
		public static Error GetErrorFromException(Exception e, ErrorLevels errorLevel, string strAdditionalInformation)
		{
			var error = new Error
			{
				Message = e.GetBaseException().Message,
				Source = e.GetBaseException().Source,
				ErrorLevel = Enum.GetName(typeof(ErrorLevels), errorLevel),
				AdditionalInformation = strAdditionalInformation,
				StackTrace = e.StackTrace + Environment.NewLine + (e.InnerException == null ? "             |No inner exception| " : "             |Inner Exception| " + GetErrorAsString(e.InnerException)),
				Created = DateTime.UtcNow
			};

			return error;
		}

		public static string GetErrorAsHtml(Exception e)
		{
			Debug.WriteLine("ErrorFactory.GetErrorAsHtml()");

			var sb = new StringBuilder();
			bool first = true;

			while (e != null)
			{
				sb.Append(first ? "<br/>" : "<br/><br/><br/>");
				first = false;
				sb.Append("|Message| " + e.GetBaseException().Message + "<br/>");
				sb.Append("|Source| " + e.GetBaseException().Source + "<br/>");
				sb.AppendLine("[Stack Trace|<br/>");
				foreach (var item in GetStackStraceStrings(e.StackTrace))
				{
					sb.AppendLine("&nbsp;&nbsp;&nbsp;" + item + "<br/>");
				}
				sb.AppendLine("<br/>");
				sb.Append(e.InnerException == null ? "<br/>|No Inner Exception|<br/>" : "<br/>|Inner Exception|: <br/>");
				e = e.InnerException;
			}

			return sb.ToString();
		}

		public static string GetErrorAsString(Exception e)
		{
			Debug.WriteLine("ErrorFactory.GetErrorAsString()");

			var sb = new StringBuilder();
			bool first = true;

			while (e != null)
			{
				if (!first)
				{
					sb.AppendLine();
					sb.AppendLine();
				}
				first = false;
				sb.AppendLine("|Message| " + e.GetBaseException().Message);
				sb.AppendLine("|Time| " + DateTime.Now);
				sb.AppendLine("|Source| " + e.GetBaseException().Source);
				sb.AppendLine("[Stack Trace|");
				foreach (string item in GetStackStraceStrings(e.StackTrace))
				{
					sb.AppendLine("  " + item);
				}
				sb.AppendLine("");
				sb.AppendLine(e.InnerException == null ? "|No Inner Exception|" : "\n|Inner Exception|: ");
				e = e.InnerException;
			}

			return sb.ToString();
		}

		public static string GetAggregateErrorAsString(AggregateException ae)
		{
			Debug.WriteLine("ErrorFactory.GetAggregateErrorAsString()");

			var sb = new StringBuilder();
			sb.AppendLine("|AGGREGATE ERROR|");
			sb.AppendLine(GetErrorAsString(ae.GetBaseException()));
			sb.AppendLine("");

			foreach (var e in ae.InnerExceptions)
			{
				sb.AppendLine(GetErrorAsString(e));
				sb.AppendLine("");
			}

			return sb.ToString();
		}

		//helper method for throwing an aggregate exception
		private static string[] GetAllFiles(string str)
		{
			Debug.WriteLine("ErrorFactory.GetAllFiles()");

			// Should throw an UnauthorizedAccessException exception. 
			return Directory.GetFiles(str, "*.txt", SearchOption.AllDirectories);
		}

		public static bool GetThrownException()
		{
			Debug.WriteLine("ErrorFactory.GetThrownException()");

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
			Debug.WriteLine("ErrorFactory.GetThrownAggregateException()");

			// Get a folder path whose directories should throw an UnauthorizedAccessException. 
			string path = Directory.GetParent(
				Environment.GetEnvironmentVariable(
					RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "LocalAppData" : "Home"))
				.FullName;
			;

			var task = Task<string[]>.Factory.StartNew(() => GetAllFiles(path));
			var taskTwo = Task<string[]>.Factory.StartNew(() => { throw new IndexOutOfRangeException(); });

			Task.WaitAll(task, taskTwo); //waits on all
			return Task.WhenAll(task, taskTwo);
		}


		private static IEnumerable<string> GetStackStraceStrings(string strStackTrace)
		{
			Debug.WriteLine("ErrorFactory.GetStackStraceStrings()");

			return strStackTrace == null ? new[] { "" } :
				strStackTrace.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
		}
	}
}
