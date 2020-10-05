using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

using InLife.Store.Identity.Models;

namespace InLife.Store.Identity.Data
{
	public interface IContext
	{

	}

	public class ApplicationContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>, IContext
	{
		public ApplicationContext(DbContextOptions<ApplicationContext> options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<ApplicationUser>().ToTable("Users");
			builder.Entity<ApplicationRole>().ToTable("UserRoles");
			builder.Entity<IdentityUserRole<string>>().ToTable("Users_UserRoles");
			builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
			builder.Entity<IdentityRoleClaim<string>>().ToTable("UserRoleClaims");
			builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
			builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
		}
	}
}
