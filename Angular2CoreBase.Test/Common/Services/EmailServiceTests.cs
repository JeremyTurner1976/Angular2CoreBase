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
	using Microsoft.Extensions.Logging;
	using Microsoft.Extensions.Options;
	using Moq;
	using Xunit;

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
			//TODO Open Email  From SMTP and verify?

			//Test Complex Email
			service.SendMail(CreateEmail());
			//Note: Although this only mails to the developer address, the email is successful. 
			//The email itself will show the set of failed recipients in the email thread
			//If smtp.gmail.com they show as "Too many failed recipients"

			//TODO Open Email From SMTP and verify?

			mockRepository.VerifyAll();
		}

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
			//TODO Verify Email Values Helper - Expect Three Addresses in Each Field, with 4 in the Two
			//Although this does only send to one address, this is successf

			//Test Complex Email
			service.SendMail(CreateEmail());

			Assert.Equal(startingFileCount + 2, Directory.GetFiles(emailPickupDirectory).Count());
			//TODO Verify Email Values Helper

			mockRepository.VerifyAll();
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
					"TestToAddressOne@test.com",
					"TestToAddressTwo@test.com",
					"TestToAddressThree@test.com"
				},
				CarbonCopies = new List<string>()
				{
					"TestCCAddressOne@test.com",
					"TestCCAddressTwo@test.com",
					"TestCCAddressThree@test.com"
				},
				BackupCarbonCopies = new List<string>()
				{
					"TestBCCAddressOne@test.com",
					"TestBCCAddressTwo@test.com",
					"TestBCCAddressThree@test.com"
				},
				Message = Message,
				Body = Body
			};


		}
	}
}