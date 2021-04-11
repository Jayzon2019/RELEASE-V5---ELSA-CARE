using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Configuration;
using System.Security.Claims;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

using InLife.Store.Core.Models;
using DocumentFormat.OpenXml.Spreadsheet;
using InLife.Store.Core.Models.ContentEntities;

namespace InLife.Store.Infrastructure.Repository
{
	public interface IApplicationContext : IContext
	{
		// Identity
		DbSet<User> Users { get; set; }
		DbSet<User_UserRole> Users_UserRoles { get; set; }
		DbSet<UserSession> UserSessions { get; set; }

		// TODO: CMS ENTITIES FOR CLEANUP
		// Content
		DbSet<ActivityLog> ActivityLog { get; set; }
		DbSet<EmailCredential> EmailCredential { get; set; }
		DbSet<ExceptionLog> ExceptionLog { get; set; }
		DbSet<KeyMetric> KeyMetric { get; set; }

		DbSet<Faq> Faq { get; set; }
		DbSet<FaqCategory> FaqCategory { get; set; }
		DbSet<FooterLink> FooterLink { get; set; }
		DbSet<Hero> Hero { get; set; }
		DbSet<PrimeCare> PrimeCare { get; set; }
		DbSet<PrimeHero> PrimeHero { get; set; }
		DbSet<ProductDetail> ProductDetail { get; set; }
		DbSet<Product> Product { get; set; }
		DbSet<GroupsT> GroupsT { get; set; }
		DbSet<Feedback> Feedback { get; set; }
		DbSet<ApplyDocuments> ApplyDocuments { get; set; }
		DbSet<PaymentStatus> PaymentStatus { get; set; }

		// Key Generator
		DbSet<ReferenceCode> ReferenceCodes { get; set; }
	}

	public class ApplicationContext : DbContext, IApplicationContext
	{
		public ApplicationContext()
		{
		}

		public ApplicationContext(DbContextOptions<ApplicationContext> options)
		   : base(options)
		{

		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				// Local dev setup
				optionsBuilder.UseSqlServer("Server=localhost;Database=InLife.Store;Trusted_Connection=True;MultipleActiveResultSets=true;");
			}
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			#region Identity

			builder.Entity<User>(entity =>
			{
				// Table mapping
				entity.ToTable("Users");

				// PK - Id
				entity.HasKey(e => new { e.Id });
			});

			builder.Entity<User_UserRole>(entity =>
			{
				// Table mapping
				entity.ToTable("Users_UserRoles");

				// Users - UserRoles - Enumeration
				entity.Ignore(e => e.Role);
				entity.Property(e => e.UserRoleId).HasColumnName("RoleId");

				// Users >< Roles
				entity
					.HasKey(junction => new { junction.UserId, junction.UserRoleId });
				entity
					.HasOne(junction => junction.User)
					.WithMany(user => user.Roles)
					.HasForeignKey(junction => junction.UserId);
			});

			builder.Entity<UserSession>(entity =>
			{
				// Table mapping
				entity.ToTable("UserSessions");

				// PK - Id
				entity.HasKey(e => new { e.Id });

				// Shadow FK - UserId
				entity.Property<string>("UserId");

				// Quotes >> Customer
				entity
					.HasOne(session => session.User)
					.WithMany(user => user.Sessions)
					.HasForeignKey("UserId");
			});

			#endregion

			#region Content

			// TODO: CMS ENTITIES FOR CLEANUP
			builder.Entity<ActivityLog>(entity =>
			{
				entity.ToTable("TblActivityLogs");
				entity.HasKey(e => new { e.Id });

				entity.Property(e => e.Action).HasColumnName("ActionPerfomed");
				entity.Property(e => e.Description).HasColumnName("ActivityDescription");

				entity.Property(e => e.TransactionByName).HasColumnName("ActivityBy");
				entity.Property(e => e.TransactionDate).HasColumnName("ActivityDate");
				entity.Property(e => e.TransactionRemoteAddress).HasColumnName("IpAddress");

				entity.Ignore(e => e.TransactionBy);
				entity.Property<string>("TransactionById").HasColumnName("ActivityById");
				entity
					.HasOne(e => e.TransactionBy)
					.WithMany()
					.HasForeignKey("TransactionById");
			});

			builder.Entity<EmailCredential>(entity =>
			{
				entity.ToTable("TblEmailCredentials");
				entity.HasKey(e => new { e.Id });
			});

			builder.Entity<ExceptionLog>(entity =>
			{
				entity.ToTable("TblExceptionLogs");
				entity.HasKey(e => new { e.Id });
			});

			builder.Entity<KeyMetric>(entity =>
			{
				entity.ToTable("TblKeyMetrics");
				entity.HasKey(e => new { e.Id });
			});

			builder.Entity<Faq>(entity =>
			{
				entity.ToTable("TblFaqs");
				entity.HasKey(e => new { e.Id });

				// Shadow FK - CategoryId
				entity.Property<int>("CategoryId");
				// FAQs >> FAQ Category
				entity
					.HasOne(e => e.Category)
					.WithMany(category => category.Faqs)
					.HasForeignKey("CategoryId");

				entity.Ignore(e => e.CreatedBy);
				entity.Property<string>("CreatedById").HasColumnName("CreatedBy");
				entity
					.HasOne(e => e.CreatedBy)
					.WithMany()
					.HasForeignKey("CreatedById");

				entity.Ignore(e => e.UpdatedBy);
				entity.Property<string>("UpdatedById").HasColumnName("UpdatedBy");
				entity
					.HasOne(e => e.UpdatedBy)
					.WithMany()
					.HasForeignKey("UpdatedById");
			});

			builder.Entity<FaqCategory>(entity =>
			{
				entity.ToTable("TblFaqCategories");
				entity.HasKey(e => new { e.Id });

				entity.Ignore(e => e.CreatedBy);
				entity.Property<string>("CreatedById").HasColumnName("CreatedBy");
				entity
					.HasOne(e => e.CreatedBy)
					.WithMany()
					.HasForeignKey("CreatedById");

				entity.Ignore(e => e.UpdatedBy);
				entity.Property<string>("UpdatedById").HasColumnName("UpdatedBy");
				entity
					.HasOne(e => e.UpdatedBy)
					.WithMany()
					.HasForeignKey("UpdatedById");
			});

			builder.Entity<FooterLink>(entity =>
			{
				entity.ToTable("TblFooterLinks");
				entity.HasKey(e => new { e.Id });

				entity.Ignore(e => e.CreatedBy);
				entity.Property<string>("CreatedById").HasColumnName("CreatedBy");
				entity
					.HasOne(e => e.CreatedBy)
					.WithMany()
					.HasForeignKey("CreatedById");

				entity.Ignore(e => e.UpdatedBy);
				entity.Property<string>("UpdatedById").HasColumnName("UpdatedBy");
				entity
					.HasOne(e => e.UpdatedBy)
					.WithMany()
					.HasForeignKey("UpdatedById");
			});

			builder.Entity<Hero>(entity =>
			{
				entity.ToTable("TblHeroes");
				entity.HasKey(e => new { e.Id });

				entity.Ignore(e => e.CreatedBy);
				entity.Property<string>("CreatedById").HasColumnName("CreatedBy");
				entity
					.HasOne(e => e.CreatedBy)
					.WithMany()
					.HasForeignKey("CreatedById");

				entity.Ignore(e => e.UpdatedBy);
				entity.Property<string>("UpdatedById").HasColumnName("UpdatedBy");
				entity
					.HasOne(e => e.UpdatedBy)
					.WithMany()
					.HasForeignKey("UpdatedById");
			});

			builder.Entity<PrimeCare>(entity =>
			{
				entity.ToTable("TblPrimeCare");
				entity.HasKey(e => new { e.Id });

				entity.Ignore(e => e.CreatedBy);
				entity.Property<string>("CreatedById").HasColumnName("CreatedBy");
				entity
					.HasOne(e => e.CreatedBy)
					.WithMany()
					.HasForeignKey("CreatedById");

				entity.Ignore(e => e.UpdatedBy);
				entity.Property<string>("UpdatedById").HasColumnName("UpdatedBy");
				entity
					.HasOne(e => e.UpdatedBy)
					.WithMany()
					.HasForeignKey("UpdatedById");
			});

			builder.Entity<PrimeHero>(entity =>
			{
				entity.ToTable("TblPrimeHeroes");
				entity.HasKey(e => new { e.Id });

				entity.Ignore(e => e.CreatedBy);
				entity.Property<string>("CreatedById").HasColumnName("CreatedBy");
				entity
					.HasOne(e => e.CreatedBy)
					.WithMany()
					.HasForeignKey("CreatedById");

				entity.Ignore(e => e.UpdatedBy);
				entity.Property<string>("UpdatedById").HasColumnName("UpdatedBy");
				entity
					.HasOne(e => e.UpdatedBy)
					.WithMany()
					.HasForeignKey("UpdatedById");
			});

			builder.Entity<ProductDetail>(entity =>
			{
				entity.ToTable("TblProductDetails");
				entity.HasKey(e => new { e.Id });

				// Shadow FK - ProductId
				entity.Property<int>("ProductId");
				// Details >> Product
				entity
					.HasOne(e => e.Product)
					.WithMany(product => product.Details)
					.HasForeignKey("ProductId");

				entity.Ignore(e => e.CreatedBy);
				entity.Property<string>("CreatedById").HasColumnName("CreatedBy");
				entity
					.HasOne(e => e.CreatedBy)
					.WithMany()
					.HasForeignKey("CreatedById");

				entity.Ignore(e => e.UpdatedBy);
				entity.Property<string>("UpdatedById").HasColumnName("UpdatedBy");
				entity
					.HasOne(e => e.UpdatedBy)
					.WithMany()
					.HasForeignKey("UpdatedById");
			});

			builder.Entity<Product>(entity =>
			{
				entity.ToTable("TblProducts");
				entity.HasKey(e => new { e.Id });

				entity.Ignore(e => e.CreatedBy);
				entity.Property<string>("CreatedById").HasColumnName("CreatedBy");
				entity
					.HasOne(e => e.CreatedBy)
					.WithMany()
					.HasForeignKey("CreatedById");

				entity.Ignore(e => e.UpdatedBy);
				entity.Property<string>("UpdatedById").HasColumnName("UpdatedBy");
				entity
					.HasOne(e => e.UpdatedBy)
					.WithMany()
					.HasForeignKey("UpdatedById");
			});

			builder.Entity<Feedback>(entity =>
			{
				entity.ToTable("TblFeedback");
				entity.HasKey(e => new { e.Id });
				entity.Ignore(e => e.CreatedBy);
				entity.Ignore(e => e.UpdatedBy);
			});

			#endregion

			builder.Entity<GroupsT>(entity =>
			{
				entity.ToTable("TblGroupsQuote");
				entity.HasKey(e => new {e.Id});
			});
			builder.Entity<ApplyDocuments>(entity =>
			{
				entity.ToTable("TblUploadDocumens");
				entity.HasKey(e => new { e.Id });
			});

			builder.Entity<PaymentStatus>(entity =>
			{
				entity.ToTable("TblPaymentStatus");
				entity.HasKey(e => new { e.Id });
			});

			// Key Generation
			builder.Entity<ReferenceCode>(entity =>
			{
				entity
					.ToTable("ReferenceCodes")
					.HasKey(e => new { e.Id });

				entity
					.Property(e => e.CreatedDate)
					.HasDefaultValue(DateTime.UtcNow)
					.ValueGeneratedOnAdd();
			});
		}

		public override int SaveChanges()
		{
			/*
			var modifiedEntries = ChangeTracker.Entries()
				.Where(x => x.Entity is IAuditableEntity
					&& (x.State == EntityState.Added || x.State == EntityState.Modified));

			//var currentUser = ClaimsPrincipal.Current;


			foreach (var entry in modifiedEntries)
			{
				IAuditableEntity entity = entry.Entity as IAuditableEntity;
				if (entity != null)
				{
					//string identityName = Thread.CurrentPrincipal.Identity.Name;

					//TODO:
					//	- Replace identityName with User Id
					//	- Replace userAgent with HttpRequest.UserAgent
					//	- Replace remoteAddress with Request.UserHostAddress
					//string identityName = _currentPrincipalAccessor.CurrentPrincipal.FindFirstValue(ClaimTypes.GivenName);
					//int userId = 0;
					//string userAgent = _currentApplicationUser.CurrentRequest.Headers["User-Agent"].ToString();
					//string remoteAddress = _currentPrincipalAccessor.RemoteAdress.ToString();
					//DateTime timestamp = DateTime.UtcNow;

					if (entry.State == EntityState.Deleted)
					{
						//TODO:
						//Change to soft delete for audit trail then let a DB trigger
						//handle the actual delete after inserting to the audit table

						//entity.CreatedBy = identityName;
						//entity.CreatedDate = timestamp;
					}
					else
					{
						//base.Entry(entity).Property(x => x.TransactionBy).IsModified = true;
						//base.Entry(entity).Property(x => x.TransactionDate).IsModified = true;
					}

					//entity.TransactionBy = "";
					//entity.TransactionDate = DateTime.UtcNow;
					//entity.TransactionAgent = "";
					//entity.TransactionRemoteAddress = "";
				}
			}
			*/
			return base.SaveChanges();
		}

		// Identity
		public DbSet<User> Users { get; set; }
		public DbSet<User_UserRole> Users_UserRoles { get; set; }
		public DbSet<UserSession> UserSessions { get; set; }

		// TODO: CMS ENTITIES FOR CLEANUP
		// Content
		public DbSet<ActivityLog> ActivityLog { get; set; }
		public DbSet<EmailCredential> EmailCredential { get; set; }
		public DbSet<ExceptionLog> ExceptionLog { get; set; }
		public DbSet<KeyMetric> KeyMetric { get; set; }

		public DbSet<Faq> Faq { get; set; }
		public DbSet<FaqCategory> FaqCategory { get; set; }
		public DbSet<FooterLink> FooterLink { get; set; }
		public DbSet<Hero> Hero { get; set; }
		public DbSet<PrimeCare> PrimeCare { get; set; }
		public DbSet<PrimeHero> PrimeHero { get; set; }
		public DbSet<ProductDetail> ProductDetail { get; set; }
		public DbSet<Product> Product { get; set; }
		public DbSet<Feedback> Feedback { get; set; }
		public DbSet<GroupsT> GroupsT { get; set; }
		public DbSet<ApplyDocuments> ApplyDocumnets { get; set; }
		public DbSet<PaymentStatus> PaymentStatus { get; set; }
		public DbSet<ApplyDocuments> ApplyDocuments { get; set; }

		public DbSet<ReferenceCode> ReferenceCodes { get; set; }
	}
}
