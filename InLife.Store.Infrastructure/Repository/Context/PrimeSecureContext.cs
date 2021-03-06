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
	public interface IPrimeSecureContext : IContext
	{
		DbSet<PrimeSecureApplication> PrimeSecureApplications { get; set; }
		DbSet<PrimeSecurePerson> PrimeSecurePersons { get; set; }
		DbSet<PrimeSecureAddress> PrimeSecureAddresses { get; set; }
	}

	public class PrimeSecureContext : DbContext, IPrimeSecureContext
	{
		public PrimeSecureContext()
		{
		}

		public PrimeSecureContext(DbContextOptions<PrimeSecureContext> options)
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

			builder.Entity<PrimeSecureApplication>(entity =>
			{
				// Schema.Table mapping
				entity
					.ToTable("Applications", Schema.PrimeSecure)
					.HasKey(e => new { e.Id });

				// Timestamp
				entity
					.Property(e => e.CreatedDate)
					.HasDefaultValue(DateTimeOffset.UtcNow)
					.ValueGeneratedOnAdd();

				// ReferenceId
				entity
					.Property(e => e.ReferenceId)
					.ValueGeneratedOnAdd()
					.Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Throw);

				// Shadow FK - CustomerId
				//entity.Property<Guid?>("CustomerId");

				// Applications >> Customer
				entity
					.HasOne(application => application.Customer)
					.WithMany(customer => customer.Applications)
					.HasForeignKey(e => e.CustomerId);

				// Decimal Types
				entity
					.Property(e => e.PlanFaceAmount)
					.HasColumnType("decimal(18,4)");
				entity
					.Property(e => e.PlanPremium)
					.HasColumnType("decimal(18,4)");
				entity
					.Property(e => e.IncomeAmount)
					.HasColumnType("decimal(18,4)");
			});

			builder.Entity<PrimeSecurePerson>(entity =>
			{
				// Table mapping
				entity
					.ToTable("Persons", Schema.PrimeSecure)
					.HasKey(e => new { e.Id });

				// Timestamp
				entity
					.Property(e => e.CreatedDate)
					.HasDefaultValue(DateTimeOffset.UtcNow)
					.ValueGeneratedOnAdd();

				// Customers == HomeAddress (Nullable)
				entity
					.HasOne(customer => customer.HomeAddress)
					.WithOne()
					.HasForeignKey<PrimeSecurePerson>(c => c.HomeAddressId);

				// Customers == WorkAddress (Nullable)
				entity
					.HasOne(customer => customer.WorkAddress)
					.WithOne()
					.HasForeignKey<PrimeSecurePerson>(c => c.WorkAddressId);

				// Customers == BirthAddress (Nullable)
				entity
					.HasOne(customer => customer.BirthAddress)
					.WithOne()
					.HasForeignKey<PrimeSecurePerson>(c => c.BirthAddressId);
			});

			builder.Entity<PrimeSecureAddress>(entity =>
			{
				// Table mapping
				entity
					.ToTable("Addresses", Schema.PrimeSecure)
					.HasKey(e => new { e.Id });

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

		public DbSet<PrimeSecureApplication> PrimeSecureApplications { get; set; }
		public DbSet<PrimeSecurePerson> PrimeSecurePersons { get; set; }
		public DbSet<PrimeSecureAddress> PrimeSecureAddresses { get; set; }
	}
}
