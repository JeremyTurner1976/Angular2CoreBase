namespace Angular2CoreBase.Data.Database
{
	using System;
	using Factories;
	using Faker;
	using Microsoft.Extensions.Logging;
	using Models;
	using System.Linq;

	public static class CoreBaseContextSeeder
	{
		private const int dataPopulationCount = 25;

		public static void SeedData(this CoreBaseContext context)
		{
			if (!context.ApplicationUsers.Any())
			{
				//Add a base application level user
				context.ApplicationUsers.Add(new ApplicationUser
				{
					FirstName = "System",
					LastName = "Generated Super User",
					Email = "363015fdfa2f4211b9d42ee5cf@gmail.com",
					Password = "admin",
					DisplayName = "Administrator",
					Active = true
				});

				for (int i = 1; i < dataPopulationCount; i++)
				{
					context.ApplicationUsers.Add(new ApplicationUser
					{
						FirstName = Name.First(),
						LastName = Name.Last(),
						Email = Lorem.GetFirstWord() + "@" +
						        Lorem.GetFirstWord() + ".com",
						Password = Lorem.GetFirstWord(),
						DisplayName = Name.FullName(),
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