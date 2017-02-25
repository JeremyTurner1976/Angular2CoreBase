namespace Angular2CoreBase.Common.Services
{
	using System;
	using System.Diagnostics;
	using Interfaces;
	using Microsoft.Extensions.Logging;

	public class EmailService : IEmailService
	{
		public void SendMail(
			string to, 
			string carbonCopy, 
			string backupCarbonCopy, 
			string subject,
			string message)
		{
			//todo send email
			Debug.WriteLine("TODO -> Email Service");
		}
	}
}
