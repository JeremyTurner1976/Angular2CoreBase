using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Angular2CoreBase.Data.Factories;
using Angular2CoreBase.Data.Models;

namespace Angular2CoreBase.Data.Database
{
	public static class CoreBaseContextSeeder
	{
		private const int dataPopulationCount = 25;

		public static void SeedData(this CoreBaseContext context)
		{
			if (!context.ApplicationUsers.Any())
			{
				for (var i = 0; i < dataPopulationCount; i++)
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
				for (var i = 0; i < dataPopulationCount; i++)
				{
					try
					{
						ErrorFactory.GetThrownException();
					}
					catch (Exception exception)
					{
						context.Errors.Add(
							ErrorFactory.GetErrorFromException(
								exception,
								ErrorLevels.Message,
								"Expected seeded error."));
					}
				}
				context.SaveChanges();
			}
		}
	}

}
