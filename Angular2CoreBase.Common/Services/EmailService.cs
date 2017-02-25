namespace Angular2CoreBase.Common.Services
{
	using System;
	using System.Diagnostics;
	using System.Net;
	using Interfaces;
	using MailKit.Net.Smtp;
	using MailKit.Security;
	using Microsoft.Extensions.Logging;
	using MimeKit;

	public class EmailService : IEmailService
	{
		//TODO EMAIL CLASS SETS FROM CONFIG (EmailClass gets populated and then shared out - many consumers (including context seeder)
		//Application Name in Config Also
		public async void SendMail(
			string to,
			string carbonCopy,
			string backupCarbonCopy,
			string subject,
			string message)
		{
			MimeMessage mimeMessage = new MimeMessage();
			mimeMessage.From.Add(new MailboxAddress("Angular 2 Core Base", "363015fdfa2f4211b9d42ee5cf@gmail.com"));

			mimeMessage.To.Add(new MailboxAddress(to));

			if (!string.IsNullOrWhiteSpace(carbonCopy))
				mimeMessage.To.Add(new MailboxAddress(carbonCopy));
			if (!string.IsNullOrWhiteSpace(backupCarbonCopy))
				mimeMessage.To.Add(new MailboxAddress(backupCarbonCopy));

			mimeMessage.Subject = subject;
			mimeMessage.Body = new TextPart("plain")
			{
				Text = message
			};

			/*
				var bodyBuilder = new BodyBuilder();
				bodyBuilder.HtmlBody = @"<b>This is bold and this is <i>italic</i></b>";
				message.Body = bodyBuilder.ToMessageBody();
			*/

			using (SmtpClient client = new SmtpClient())
			{
				await client.ConnectAsync("smtp.gmail.com", 587);
				client.AuthenticationMechanisms.Remove("XOAUTH2");
				// Note: since we don't have an OAuth2 token, disable 	
				// the XOAUTH2 authentication mechanism.     
				await client.AuthenticateAsync("363015fdfa2f4211b9d42ee5cf@gmail.com", "completelypublic");
				await client.SendAsync(mimeMessage);
				await client.DisconnectAsync(true);
			}
		}
	}
}