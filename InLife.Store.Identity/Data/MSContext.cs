using InLife.Store.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InLife.Store.Identity.Data
{
	public class MSContext : IdentityDbContext<ApplicationUser>
	{
		public MSContext(DbContextOptions<MSContext> options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<ApplicationUser>().ToTable("Users");
		}
	}
}
