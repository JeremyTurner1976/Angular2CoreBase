using System;
using System.Linq;
using Angular2CoreBase.Data.Models;

namespace Angular2CoreBase.Data.Database
{
	using Factories;
	using Microsoft.Extensions.Logging;

	public static class CoreBaseContextSeeder
	{
		private const int dataPopulationCount = 25;

		public static void SeedData(this CoreBaseContext context)
		{
			if (!context.ApplicationUsers.Any())
			{
				//Add a base application level user
				context.ApplicationUsers.Add(new ApplicationUser()
				{
					FirstName = "System",
					LastName = "Generated Super User",
					Email = "363015fdfa2f4211b9d42ee5cf@gmail.com",
					Password = "admin",
					DisplayName = "Administrator",
					Active = true
				});

				for (int i =1; i < dataPopulationCount; i++)
				{
					context.ApplicationUsers.Add(new ApplicationUser()
					{
						FirstName = Faker.Name.First(),
						LastName = Faker.Name.Last(),
						Email = Faker.Lorem.GetFirstWord() + "@" +
							Faker.Lorem.GetFirstWord() + ".com",
						Password = Faker.Lorem.GetFirstWord(),
						DisplayName = Faker.Name.FullName(),
						Active = true
					});
				}
				context.SaveChanges();
			}

			if (!context.Errors.Any())
			{
				for (int i = 0; i < dataPopulationCount; i++)
				{
					try
					{
						ErrorFactory.GetThrownException();
					}
					catch (Exception exception)
					{
						Error error = ErrorFactory.GetErrorFromException(
								exception,
								LogLevel.Information,
								"Expected seeded error.");
						error.CreatedDateTime = DateTime.UtcNow;
						error.CreatedByUserId = 1;

						context.Errors.Add(error);
					}
				}
				context.SaveChanges();
			}
		}
	}

}
