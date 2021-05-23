using System;
using System.Linq;
using System.Threading;
using System.Configuration;
using System.Security.Claims;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

using InLife.Store.Core.Constants;
using InLife.Store.Core.Models;

namespace InLife.Store.Infrastructure.Repository
{
	public interface IPrimeCareContext : IContext
	{
		DbSet<PrimeCareApplication> PrimeCareApplications { get; set; }
		DbSet<PrimeCarePerson> PrimeCarePersons { get; set; }
		DbSet<PrimeCareAddress> PrimeCareAddresses { get; set; }
	}

	public class PrimeCareContext : DbContext, IPrimeCareContext
	{
		public PrimeCareContext()
		{
		}

		public PrimeCareContext(DbContextOptions<PrimeCareContext> options)
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

			builder.Entity<PrimeCareApplication>(entity =>
			{
				// Table mapping
				entity.ToTable("Applications", Schema.PrimeCare);

				// PK - Id
				entity.HasKey(e => new { e.Id });

				// Shadow FK - CustomerId
				//entity.Property<Guid?>("CustomerId");

				// ReferenceId
				entity
					.Property(e => e.ReferenceId)
					.ValueGeneratedOnAdd()
					.Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Throw);

				// Applications >> Customer
				entity
					.HasOne(application => application.Customer)
					.WithMany(customer => customer.Applications)
					.HasForeignKey(e => e.CustomerId);

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

			builder.Entity<PrimeCarePerson>(entity =>
			{
				// Table mapping
				entity.ToTable("Persons", Schema.PrimeCare);

				// PK - Id
				entity.HasKey(e => new { e.Id });

				// Timestamp
				entity
					.Property(e => e.CreatedDate)
					.HasDefaultValue(DateTimeOffset.UtcNow)
					.ValueGeneratedOnAdd();

				// Shadow FK - HomeAddressId
				//entity.Property<Guid?>(c => c.HomeAddressId);

				// Customers == HomeAddress (Nullable)
				entity
					.HasOne(customer => customer.HomeAddress)
					.WithOne()
					.HasForeignKey<PrimeCarePerson>(c => c.HomeAddressId);

				// Customers == WorkAddress (Nullable)
				entity
					.HasOne(customer => customer.WorkAddress)
					.WithOne()
					.HasForeignKey<PrimeCarePerson>(c => c.WorkAddressId);

				// Customers == BirthAddress (Nullable)
				entity
					.HasOne(customer => customer.BirthAddress)
					.WithOne()
					.HasForeignKey<PrimeCarePerson>(c => c.BirthAddressId);
			});

			builder.Entity<PrimeCareAddress>(entity =>
			{
				// Table mapping
				entity.ToTable("Addresses", Schema.PrimeCare);

				// PK - Id
				entity.HasKey(e => new { e.Id });

				// Timestamp
				entity
					.Property(e => e.CreatedDate)
					.HasDefaultValue(DateTimeOffset.UtcNow)
					.ValueGeneratedOnAdd();
			});
		}

		public override int SaveChanges()
		{
			return base.SaveChanges();
		}

		public DbSet<PrimeCareApplication> PrimeCareApplications { get; set; }
		public DbSet<PrimeCarePerson> PrimeCarePersons { get; set; }
		public DbSet<PrimeCareAddress> PrimeCareAddresses { get; set; }
	}
}
