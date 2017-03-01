namespace Angular2CoreBase.Test.Common.Services
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using Angular2CoreBase.Common.CommonEnums.FileService;
	using Angular2CoreBase.Common.CommonModels;
	using Angular2CoreBase.Common.CommonModels.ConfigSettings;
	using Angular2CoreBase.Common.CommonModels.WeatherService.OpenWeather;
	using Angular2CoreBase.Common.Interfaces;
	using Angular2CoreBase.Common.Services;
	using Data;
	using Data.Database;
	using Data.Factories;
	using Data.Models;
	using Enums;
	using Microsoft.Extensions.Logging;
	using Microsoft.Extensions.Options;
	using MimeKit;
	using Moq;
	using Xunit;
	using Angular2CoreBase.Common.Extensions;

	public class EmailServiceTests : BaseTest
	{
		private readonly MockRepository mockRepository;

		private readonly Mock<IOptions<ApplicationSettings>> mockOptionsApplicationSettings;
		private Mock<IOptions<EmailSettings>> mockOptionsEmailSettings;
		private readonly IFileService fileService;

		private const string CarbonCopy = "";
		private const string BackupCarbonCopy = "";
		private const string Message = "Unit Test Generated Message";
		private const string Body = "This message is for testing stuff.";

		public EmailServiceTests()
		{
			mockRepository = new MockRepository(MockBehavior.Default);

			mockOptionsApplicationSettings = GetMockApplicationSettings(mockRepository);

			fileService = GetFileService();
		}

		/// <summary>
		/// This is more of an integration test, 
		/// simply ensuring that emails are sent via smtp to the developer.
		/// </summary>
		[Fact]
		public void TestSmtpEmailService()
		{
			mockOptionsEmailSettings = GetMockEmailSettings(mockRepository);
			EmailService service = CreateService();

			//Thus far, an exception would have been thrown if unsuccessful
			//no assert required, and the developer Email can confirm this

			//Test Simple Email
			service.SendMail(
				mockOptionsEmailSettings.Object.Value.DeveloperEmailAddress,
				CarbonCopy,
				BackupCarbonCopy,
				Message,
				Body);

			//Test Complex Email
			service.SendMail(CreateEmail());
			//Note: Although this only mails to the developer address, the email is successful. 
			//The email itself will show the set of failed recipients in the email thread
			//If smtp.gmail.com they show as "Too many failed recipients" after fail three


			mockRepository.VerifyAll();
		}

		/// <summary>
		/// This test will create pickup directory emails,
		/// and verify the contents of the message to be sent
		/// </summary>
		[Fact]
		public void TestConfigDirectoryEmailService()
		{
			mockOptionsEmailSettings = GetMockEmailSettings(mockRepository, true);
			EmailService service = CreateService();

			string emailPickupDirectory = fileService.GetDirectoryFolderLocation(DirectoryFolders.Email);
			int startingFileCount = Directory.GetFiles(emailPickupDirectory).Count();

			//Test Simple Email
			service.SendMail(
				mockOptionsEmailSettings.Object.Value.DeveloperEmailAddress,
				CarbonCopy,
				BackupCarbonCopy,
				Message,
				Body);

			Assert.Equal(startingFileCount + 1, Directory.GetFiles(emailPickupDirectory).Count());
			AssertEmailCorrect(
				new List<string>() {mockOptionsEmailSettings.Object.Value.DeveloperEmailAddress},
				new List<string>(),
				new List<string>(),
				Message,
				Body
			);

			//Test Complex Email
			Email testEmail = CreateEmail();
			service.SendMail(testEmail);

			Assert.Equal(startingFileCount + 2, Directory.GetFiles(emailPickupDirectory).Count());
			AssertEmailCorrect(
				testEmail.ToAddresses,
				testEmail.CarbonCopies,
				testEmail.BackupCarbonCopies,
				testEmail.Message,
				testEmail.Body
			);

			mockRepository.VerifyAll();
		}

		private void AssertEmailCorrect(
			List<string> toAddresses, 
			List<string> carbonCopies, 
			List<string> backupCarbonCopies,
			string message, 
			string body)
		{
			string latestEmailFile = fileService.GetDirectoryFileName(DirectoryFolders.Email);
			using (FileStream stream = File.OpenRead(latestEmailFile))
			{
				MimeMessage mimeMessage = MimeMessage.Load(stream);

				string concatenatedAddresses = 
					mimeMessage.To.Mailboxes.Aggregate(string.Empty, (current, item) => current + item.Address);
				foreach (string emailAddress in toAddresses)
				{
					Assert.True(concatenatedAddresses.Contains(emailAddress));
				}

				concatenatedAddresses =
					mimeMessage.Cc.Mailboxes.Aggregate(string.Empty, (current, item) => current + item.Address);
				foreach (string emailAddress in carbonCopies)
				{
					Assert.True(concatenatedAddresses.Contains(emailAddress));
				}

				concatenatedAddresses =
					mimeMessage.Bcc.Mailboxes.Aggregate(string.Empty, (current, item) => current + item.Address);
				foreach (string emailAddress in backupCarbonCopies)
				{
					Assert.True(concatenatedAddresses.Contains(emailAddress));
				}

				Assert.Equal(message, mimeMessage.Subject);
				Assert.True(mimeMessage.BodyParts.First().ToString().Contains(body));
			}
		}

		private EmailService CreateService()
		{
			return new EmailService(
				mockOptionsApplicationSettings.Object,
				mockOptionsEmailSettings.Object,
				fileService);
		}

		private Email CreateEmail()
		{
			return new Email()
			{
				ToAddresses = new List<string>()
				{
					mockOptionsEmailSettings.Object.Value.DeveloperEmailAddress,
					GetTestEmailString(EmailAddressTypes.To, 1),
					GetTestEmailString(EmailAddressTypes.To, 2),
					GetTestEmailString(EmailAddressTypes.To, 3),
				},
				CarbonCopies = new List<string>()
				{
					GetTestEmailString(EmailAddressTypes.Cc, 1),
					GetTestEmailString(EmailAddressTypes.Cc, 2),
					GetTestEmailString(EmailAddressTypes.Cc, 3),
				},
				BackupCarbonCopies = new List<string>()
				{
					GetTestEmailString(EmailAddressTypes.Bcc, 1),
					GetTestEmailString(EmailAddressTypes.Bcc, 2),
					GetTestEmailString(EmailAddressTypes.Bcc, 3),
				},
				Message = Message,
				Body = Body
			};


		}

		private string GetTestEmailString(EmailAddressTypes addressType, int testPosition)
		{
			return $"Test{addressType.ToNameString().ToUpper()}{testPosition}@test.com";
		}
	}
}
