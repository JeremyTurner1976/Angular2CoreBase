using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
			Error error = new Error
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

			StringBuilder stringBuilder= new StringBuilder();
			bool first = true;

			while (e != null)
			{
				 stringBuilder.Append(first ? "<br/>" : "<br/><br/><br/>");
				first = false;
				 stringBuilder.Append("|Message| " + e.GetBaseException().Message + "<br/>");
				 stringBuilder.Append("|Source| " + e.GetBaseException().Source + "<br/>");
				 stringBuilder.AppendLine("[Stack Trace|<br/>");
				foreach (string item in GetStackStraceStrings(e.StackTrace))
				{
					 stringBuilder.AppendLine("&nbsp;&nbsp;&nbsp;" + item + "<br/>");
				}
				 stringBuilder.AppendLine("<br/>");
				 stringBuilder.Append(e.InnerException == null ? "<br/>|No Inner Exception|<br/>" : "<br/>|Inner Exception|: <br/>");
				e = e.InnerException;
			}

			return stringBuilder.ToString();
		}

		public static string GetErrorAsString(Exception e)
		{
			Debug.WriteLine("ErrorFactory.GetErrorAsString()");

			StringBuilder stringBuilder= new StringBuilder();
			bool first = true;

			while (e != null)
			{
				if (!first)
				{
					 stringBuilder.AppendLine();
					 stringBuilder.AppendLine();
				}
				first = false;
				 stringBuilder.AppendLine("|Message| " + e.GetBaseException().Message);
				 stringBuilder.AppendLine("|Time| " + DateTime.Now);
				 stringBuilder.AppendLine("|Source| " + e.GetBaseException().Source);
				 stringBuilder.AppendLine("[Stack Trace|");
				foreach (string item in GetStackStraceStrings(e.StackTrace))
				{
					 stringBuilder.AppendLine("  " + item);
				}
				 stringBuilder.AppendLine("");
				 stringBuilder.AppendLine(e.InnerException == null ? "|No Inner Exception|" : "\n|Inner Exception|: ");
				e = e.InnerException;
			}

			return stringBuilder.ToString();
		}

		public static string GetAggregateErrorAsHtml(AggregateException ae)
		{
			Debug.WriteLine("ErrorFactory.GetAggregateErrorAsHtml()");

			StringBuilder stringBuilder = new StringBuilder();
			 stringBuilder.AppendLine("|AGGREGATE ERROR|");
			 stringBuilder.AppendLine(GetErrorAsString(ae.GetBaseException()));
			 stringBuilder.AppendLine("");
			bool first = true;

			foreach (Exception e in ae.InnerExceptions)
			{
				 stringBuilder.Append(first ? "<br/>" : "<br/><br/><br/>");
				first = false;
				 stringBuilder.Append("|Message| " + e.GetBaseException().Message + "<br/>");
				 stringBuilder.Append("|Source| " + e.GetBaseException().Source + "<br/>");
				 stringBuilder.AppendLine("[Stack Trace|<br/>");
				foreach (string item in GetStackStraceStrings(e.StackTrace))
				{
					 stringBuilder.AppendLine("&nbsp;&nbsp;&nbsp;" + item + "<br/>");
				}
				 stringBuilder.AppendLine("<br/>");
				 stringBuilder.Append(e.InnerException == null ? "<br/>|No Inner Exception|<br/>" : "<br/>|Inner Exception|: <br/>");
			}


			return stringBuilder.ToString();
		}

		public static string GetAggregateErrorAsString(AggregateException ae)
		{
			Debug.WriteLine("ErrorFactory.GetAggregateErrorAsString()");

			StringBuilder stringBuilder = new StringBuilder();
			 stringBuilder.AppendLine("|AGGREGATE ERROR|");
			 stringBuilder.AppendLine(GetErrorAsString(ae.GetBaseException()));
			 stringBuilder.AppendLine("");

			foreach (Exception e in ae.InnerExceptions)
			{
				 stringBuilder.AppendLine(GetErrorAsString(e));
				 stringBuilder.AppendLine("");
			}

			return stringBuilder.ToString();
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

			Task<string[]> task = Task<string[]>.Factory.StartNew(() => GetAllFiles(path));
			Task<string[]> taskTwo = Task<string[]>.Factory.StartNew(() => { throw new IndexOutOfRangeException(); });

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
