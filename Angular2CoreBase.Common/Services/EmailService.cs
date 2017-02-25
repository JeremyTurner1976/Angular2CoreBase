namespace Angular2CoreBase.Common.Services
{
	using System;
	using System.Diagnostics;
	using System.IO;
	using System.Net;
	using CommonEnums.FileService;
	using CommonModels.ConfigSettings;
	using Interfaces;
	using MailKit.Net.Smtp;
	using MailKit.Security;
	using Microsoft.Extensions.Logging;
	using Microsoft.Extensions.Options;
	using MimeKit;

	public class EmailService : IEmailService
	{
		private readonly ApplicationSettings _applicationSettings;
		private readonly EmailSettings _emailSettings;
		private readonly IFileService _fileService;

		public EmailService(
			IOptions<ApplicationSettings> applicationSettings, 
			IOptions<EmailSettings> emailSettings,
			IFileService fileService)
		{
			_applicationSettings = applicationSettings.Value;
			_emailSettings = emailSettings.Value;
			_fileService = fileService;
		}

		public async void SendMail(
			string to,
			string carbonCopy,
			string backupCarbonCopy,
			string subject,
			string message)
		{
			MimeMessage mimeMessage = new MimeMessage();
			mimeMessage.From.Add(new MailboxAddress(_applicationSettings.Name, _emailSettings.EmailAddress));

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

			if (_emailSettings.UsePickupDirectory)
			{
				using (StreamWriter data = 
					File.CreateText(
						Path.Combine(
							_fileService.GetDirectoryFolderLocation(DirectoryFolders.Email), 
							Guid.NewGuid() + "_Email.eml")))
				{
					mimeMessage.WriteTo(data.BaseStream);
				}
			}
			else
			{
				using (SmtpClient client = new SmtpClient())
				{
					await client.ConnectAsync(_emailSettings.Smtp, _emailSettings.Port);

					// Note: since we don't have an OAuth2 token, disable 	
					// the XOAUTH2 authentication mechanism.     
					client.AuthenticationMechanisms.Remove("XOAUTH2");

					await client.AuthenticateAsync(_emailSettings.EmailAddress, _emailSettings.EmailPassword);
					await client.SendAsync(mimeMessage);
					await client.DisconnectAsync(true);
				}
			}
		}
	}
}