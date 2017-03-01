using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angular2CoreBase.Test
{
	using Angular2CoreBase.Common.CommonModels.ConfigSettings;
	using Angular2CoreBase.Common.Interfaces;
	using Angular2CoreBase.Common.Services;
	using Data;
	using Data.Database;
	using Data.Interfaces;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.Options;
	using Moq;
	using Remotion.Linq.Clauses;

	//https://blog.jetbrains.com/dotnet/2017/02/21/resharper-net-core-unit-testing/
	public class BaseTest
	{
		//Repositories
		public IRepository<T> GetCoreBaseRepository<T>() where T : class, ICommonModel
		{
			return new CoreBaseRepository<T>(GetCoreBaseContext());
		} 


		//Settings Objects
		public Mock<IOptions<ApplicationSettings>> GetMockApplicationSettings(MockRepository repository)
		{
			Mock<IOptions<ApplicationSettings>> mock = repository.Create<IOptions<ApplicationSettings>>();
			IOptions<ApplicationSettings> applicationSettings =
				Options.Create(
					new ApplicationSettings()
					{
						Name = "Unit Testing AngularCore2BaseApp"
					});

			mock.Setup(x => x.Value).Returns(applicationSettings.Value);
			return mock;
		}

		public Mock<IOptions<EmailSettings>> GetMockEmailSettings(MockRepository repository, bool usePickupDirectory = false)
		{
			Mock<IOptions<EmailSettings>> mock = repository.Create<IOptions<EmailSettings>>();
			IOptions<EmailSettings> emailSettings =
				Options.Create(
					new EmailSettings()
					{
						DeveloperEmailAddress = "testemailguidhere@gmail.com",
						CarbobCopyEmailAddress = "",
						BackupCarbonCopyEmailAddress = "",
						EmailAddress = "testemailguidhere@gmail.com",
						EmailPassword = "completely-public",
						Port = 587,
						Smtp = "smtp.gmail.com",
						UsePickupDirectory = usePickupDirectory
					});

			mock.Setup(x => x.Value).Returns(emailSettings.Value);
			return mock;
		}

		//Services
		public IFileService GetFileService()
		{
			return new FileService();
		}

		//Helpers
		private CoreBaseContext GetCoreBaseContext()
		{
			// Initialize DbContext in memory
			DbContextOptionsBuilder<CoreBaseContext> optionsBuilder
				= new DbContextOptionsBuilder<CoreBaseContext>();
			optionsBuilder.UseInMemoryDatabase();
			return new CoreBaseContext(optionsBuilder.Options);
		}

	}
}


//Sample in Memory Db
/*
CoreBaseContext dbContext = GetCoreBaseContext();
CoreBaseRepository<Error> errors = new CoreBaseRepository<Error>(dbContext);
int errorsCount = errors.Count();
Assert.Equal(errorsCount, 0);

Error error = null;

try
{
	ErrorFactory.ThrowException();
}
catch (Exception exception)
{
	error = ErrorFactory.GetErrorFromException(
		exception,
		LogLevel.Information,
		"Expected test generated error.");
	error.CreatedDateTime = DateTime.UtcNow;
	error.CreatedByUserId = 1;
}


Assert.NotNull(error);

errors.Add(error);
errors.SaveChanges();

int errorsCountTwo = errors.Count();
Assert.Equal(errorsCountTwo, 1);
*/
