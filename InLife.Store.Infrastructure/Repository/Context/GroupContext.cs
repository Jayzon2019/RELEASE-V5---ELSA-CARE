using System;
using System.Linq;
using System.Threading;
using System.Configuration;
using System.Security.Claims;
using System.Data.SqlClient;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

using InLife.Store.Core.Constants;
using InLife.Store.Core.Models;

namespace InLife.Store.Infrastructure.Repository
{
	public interface IGroupContext : IContext
	{
		DbSet<GroupApplication> GroupApplications { get; set; }
		DbSet<GroupFile> GroupFiles { get; set; }
	}

	public class GroupContext : DbContext, IGroupContext
	{
		public GroupContext()
		{
		}

		public GroupContext(DbContextOptions<GroupContext> options)
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

			builder.Entity<GroupApplication>(entity =>
			{
				// Schema.Table mapping
				entity
					.ToTable("Applications", Schema.Group)
					.HasKey(e => new { e.Id });

				entity
					.Ignore(e => e.CreatedDateLocal)
					.Ignore(e => e.CompletedDateLocal)
					.Ignore(e => e.ExportedDateLocal);

				// Shadow FK - Files
				//entity.Property<Guid?>(a => a.EmployeeCensusFileId);
				//entity.Property<Guid?>(a => a.AdminFormFileId);
				//entity.Property<Guid?>(a => a.RepresentativeFileId);
				//entity.Property<Guid?>(a => a.BirDocumentFileId);
				//entity.Property<Guid?>(a => a.BusinessRegistrationDocumentFileId);
				//entity.Property<Guid?>(a => a.IncorporationDocumentFileId);
				//entity.Property<Guid?>(a => a.AuthorizationDocumentFileId);
				//entity.Property<Guid?>(a => a.IndividualApplicationsFileId);
				//entity.Property<Guid?>(a => a.PaymentProofFileId);

				// Application == File (Nullable)
				//entity
				//	.HasOne(a => a.EmployeeCensusFile)
				//	.WithOne()
				//	.HasForeignKey<GroupApplication>(a => a.EmployeeCensusFileId);
				//entity
				//	.HasOne(a => a.AdminFormFile)
				//	.WithOne()
				//	.HasForeignKey<GroupApplication>(a => a.AdminFormFileId);
				//entity
				//	.HasOne(a => a.RepresentativeFile)
				//	.WithOne()
				//	.HasForeignKey<GroupApplication>(a => a.RepresentativeFileId);
				//entity
				//	.HasOne(a => a.BirDocumentFile)
				//	.WithOne()
				//	.HasForeignKey<GroupApplication>(a => a.BirDocumentFileId);
				//entity
				//	.HasOne(a => a.BusinessRegistrationDocumentFile)
				//	.WithOne()
				//	.HasForeignKey<GroupApplication>(a => a.BusinessRegistrationDocumentFileId);
				//entity
				//	.HasOne(a => a.IncorporationDocumentFile)
				//	.WithOne()
				//	.HasForeignKey<GroupApplication>(a => a.IncorporationDocumentFileId);
				//entity
				//	.HasOne(a => a.AuthorizationDocumentFile)
				//	.WithOne()
				//	.HasForeignKey<GroupApplication>(a => a.AuthorizationDocumentFileId);
				//entity
				//	.HasOne(a => a.IndividualApplicationsFile)
				//	.WithOne()
				//	.HasForeignKey<GroupApplication>(a => a.IndividualApplicationsFileId);
				//entity
				//	.HasOne(a => a.PaymentProofFile)
				//	.WithOne()
				//	.HasForeignKey<GroupApplication>(a => a.PaymentProofFileId);

				// Timestamp
				entity
					.Property(e => e.CreatedDate)
					.HasDefaultValue(DateTimeOffset.UtcNow)
					.ValueGeneratedOnAdd();

				// Decimal Types
				entity
					.Property(e => e.PlanFaceAmount)
					.HasColumnType("decimal(18,4)");
				entity
					.Property(e => e.PlanPremium)
					.HasColumnType("decimal(18,4)");
			});

			builder.Entity<GroupFile>(entity =>
			{
				// Table mapping
				entity
					.ToTable("Files", Schema.Group)
					.HasKey(e => new { e.Id });

				// MediaType - Enumeration
				entity
					.Ignore(e => e.MediaType)
					.Property(e => e.MediaTypeId)
					.HasColumnName("MediaType");
			});
		}

		public override int SaveChanges()
		{
			return base.SaveChanges();
		}

		public DbSet<GroupApplication> GroupApplications { get; set; }
		public DbSet<GroupFile> GroupFiles { get; set; }
	}
}
