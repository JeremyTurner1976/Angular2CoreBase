using Angular2CoreBase.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Angular2CoreBase.Data.Database
{
	public class CoreBaseContext : DbContext
	{
		public CoreBaseContext(DbContextOptions<CoreBaseContext> options)
			: base(options)
		{
		}


		public DbSet<Error> Errors { get; set; }
		public DbSet<ApplicationUser> ApplicationUsers { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity(typeof(Error))
				.HasOne(typeof(ApplicationUser), "CreatedByUser")
				.WithMany()
				.HasForeignKey("CreatedByUserId")
				.OnDelete(DeleteBehavior.Restrict); // TURN OFF DEFAULT CASCADE

			modelBuilder.Entity(typeof(Error))
				.HasOne(typeof(ApplicationUser), "UpdatedByUser")
				.WithMany()
				.HasForeignKey("UpdatedByUserId")
				.OnDelete(DeleteBehavior.Restrict); // TURN OFF DEFAULT CASCADE

			base.OnModelCreating(modelBuilder);
		}

	}
}
