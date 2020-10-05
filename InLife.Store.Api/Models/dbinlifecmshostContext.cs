using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace InLife.Store.Api.Models
{
    public partial class dbinlifecmshostContext : DbContext
    {
        public dbinlifecmshostContext()
        {
        }

        public dbinlifecmshostContext(DbContextOptions<dbinlifecmshostContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblActivityLogs> TblActivityLogs { get; set; }
        public virtual DbSet<TblEmailCredentials> TblEmailCredentials { get; set; }
        public virtual DbSet<TblExceptionLogs> TblExceptionLogs { get; set; }
        public virtual DbSet<TblFaq> TblFaq { get; set; }
        public virtual DbSet<TblFaqCategories> TblFaqCategories { get; set; }
        public virtual DbSet<TblFooterLinks> TblFooterLinks { get; set; }
        public virtual DbSet<TblGender> TblGender { get; set; }
        public virtual DbSet<TblHero> TblHero { get; set; }
        public virtual DbSet<TblKeyMetrics> TblKeyMetrics { get; set; }
        public virtual DbSet<TblPrimeCare> TblPrimeCare { get; set; }
        public virtual DbSet<TblPrimeHero> TblPrimeHero { get; set; }
        public virtual DbSet<TblProductDetails> TblProductDetails { get; set; }
        public virtual DbSet<TblProducts> TblProducts { get; set; }
        public virtual DbSet<TblUserRoles> TblUserRoles { get; set; }
        public virtual DbSet<TblUsers> TblUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=den1.mssql8.gear.host;Database=dbinlifecmshost;user id=dbinlifecmshost;password=Kv3DY95m~!5o;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblActivityLogs>(entity =>
            {
                entity.HasKey(e => e.ActivityLogId);

                entity.Property(e => e.ActionPerfomed).HasMaxLength(200);

                entity.Property(e => e.ActivityBy).HasMaxLength(300);

                entity.Property(e => e.ActivityDate).HasColumnType("datetime");

                entity.Property(e => e.ActivityDescription).HasMaxLength(500);

                entity.Property(e => e.IpAddress).HasMaxLength(100);
            });

            modelBuilder.Entity<TblEmailCredentials>(entity =>
            {
                entity.HasKey(e => e.EmailCredentialsId);

                entity.Property(e => e.EnableSsl).HasColumnName("EnableSSL");

                entity.Property(e => e.Password).HasMaxLength(300);

                entity.Property(e => e.Port).HasColumnName("port");

                entity.Property(e => e.Smtp).HasMaxLength(200);

                entity.Property(e => e.UserName).HasMaxLength(300);
            });

            modelBuilder.Entity<TblExceptionLogs>(entity =>
            {
                entity.HasKey(e => e.ExId);

                entity.Property(e => e.ExDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ExInner).HasDefaultValueSql("('')");

                entity.Property(e => e.ExMsg)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ExSource)
                    .IsRequired()
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ExUrl)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<TblFaq>(entity =>
            {
                entity.HasKey(e => e.FaqId);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.FaqAnswer)
                    .IsRequired()
                    .HasMaxLength(800)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.FaqQuestion)
                    .IsRequired()
                    .HasMaxLength(300)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblFaqCategories>(entity =>
            {
                entity.HasKey(e => e.FaqCatId);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.FaqCatDescription).HasMaxLength(150);

                entity.Property(e => e.FaqCategory)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblFooterLinks>(entity =>
            {
                entity.HasKey(e => e.FooterLinkId);

                entity.Property(e => e.ContactUsUrl).HasMaxLength(500);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CusCharterUrl).HasMaxLength(500);

                entity.Property(e => e.FbUrl).HasMaxLength(500);

                entity.Property(e => e.InsCommissionUrl).HasMaxLength(500);

                entity.Property(e => e.InstaUrl).HasMaxLength(500);

                entity.Property(e => e.LogoUrl).HasMaxLength(500);

                entity.Property(e => e.MainSiteUrl).HasMaxLength(500);

                entity.Property(e => e.PrivacyPolicyUrl).HasMaxLength(500);

                entity.Property(e => e.TermsConditionUrl).HasMaxLength(500);

                entity.Property(e => e.TweeterUrl).HasMaxLength(500);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.YouTubeUrl).HasMaxLength(500);
            });

            modelBuilder.Entity<TblGender>(entity =>
            {
                entity.HasKey(e => e.GenderId);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<TblHero>(entity =>
            {
                entity.HasKey(e => e.HeroId);

                entity.Property(e => e.BtnTxtLink)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ContentPostion).HasMaxLength(50);

                entity.Property(e => e.CreatedBy).HasDefaultValueSql("((0))");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Heading).HasMaxLength(500);

                entity.Property(e => e.HeadingColor).HasMaxLength(100);

                entity.Property(e => e.HeroBg)
                    .IsRequired()
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.HeroBtnTxt)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.HeroTitle)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.SubHeading).HasMaxLength(500);

                entity.Property(e => e.SubHeadingColor).HasMaxLength(100);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblKeyMetrics>(entity =>
            {
                entity.HasKey(e => e.KeyMetricsId);

                entity.Property(e => e.Ip).HasMaxLength(300);

                entity.Property(e => e.MachineName)
                    .HasColumnName("machineName")
                    .HasMaxLength(300);

                entity.Property(e => e.PageLeftAt).HasColumnType("datetime");

                entity.Property(e => e.PageName).HasMaxLength(300);

                entity.Property(e => e.PageViewedAt).HasColumnType("datetime");

                entity.Property(e => e.Sessions).HasMaxLength(300);
            });

            modelBuilder.Entity<TblPrimeCare>(entity =>
            {
                entity.HasKey(e => e.PrimeCareId);

                entity.Property(e => e.CreatedBy).HasDefaultValueSql("('')");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.PrimeCareFile)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.PrimeCareFileDescription).HasMaxLength(500);

                entity.Property(e => e.PrimeCareFileName).HasMaxLength(200);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblPrimeHero>(entity =>
            {
                entity.HasKey(e => e.PrimeHeroId);

                entity.Property(e => e.BtnTxtLink)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.ContentPostion).HasMaxLength(50);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Heading).HasMaxLength(500);

                entity.Property(e => e.HeadingColor).HasMaxLength(100);

                entity.Property(e => e.PrimeHeroBg)
                    .IsRequired()
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.PrimeHeroBtnTxt)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.PrimeHeroTitle)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.SubHeading).HasMaxLength(500);

                entity.Property(e => e.SubHeadingColor).HasMaxLength(100);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblProductDetails>(entity =>
            {
                entity.HasKey(e => e.ProductDetailId);

                entity.Property(e => e.ProductDetailId).HasColumnName("ProductDetailID");

                entity.Property(e => e.AccreditedHospitals).HasMaxLength(500);

                entity.Property(e => e.Afr)
                    .HasColumnName("AFR")
                    .HasMaxLength(500);

                entity.Property(e => e.AgeEligibility).HasMaxLength(500);

                entity.Property(e => e.Arp)
                    .HasColumnName("ARP")
                    .HasMaxLength(500);

                entity.Property(e => e.Arthoscopic).HasMaxLength(500);

                entity.Property(e => e.BenefitLimit).HasMaxLength(500);

                entity.Property(e => e.BenefitType).HasMaxLength(500);

                entity.Property(e => e.BuyNowBtnLink).HasMaxLength(500);

                entity.Property(e => e.CasesCovered).HasMaxLength(500);

                entity.Property(e => e.Combinability).HasMaxLength(500);

                entity.Property(e => e.Consultation).HasMaxLength(500);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Ct)
                    .HasColumnName("CT")
                    .HasMaxLength(500);

                entity.Property(e => e.DentalConsultation).HasMaxLength(500);

                entity.Property(e => e.DentalServicesBenefit).HasMaxLength(500);

                entity.Property(e => e.DocProFee).HasMaxLength(500);

                entity.Property(e => e.Ftfconsultation)
                    .HasColumnName("FTFConsultation")
                    .HasMaxLength(500);

                entity.Property(e => e.HospitalNetwork).HasMaxLength(500);

                entity.Property(e => e.IndividualOrGroup).HasMaxLength(500);

                entity.Property(e => e.LaboratoryDiagnosticPro).HasMaxLength(500);

                entity.Property(e => e.Laparoscopic).HasMaxLength(500);

                entity.Property(e => e.LearnMoreBtnLink).HasMaxLength(500);

                entity.Property(e => e.MedicalCoverage).HasMaxLength(500);

                entity.Property(e => e.MedicinesAsMedicallyNeeded).HasMaxLength(500);

                entity.Property(e => e.Mer)
                    .HasColumnName("MER")
                    .HasMaxLength(500);

                entity.Property(e => e.Mra)
                    .HasColumnName("MRA")
                    .HasMaxLength(500);

                entity.Property(e => e.Mri)
                    .HasColumnName("MRI")
                    .HasMaxLength(500);

                entity.Property(e => e.NonAccreditedHos).HasMaxLength(500);

                entity.Property(e => e.NumberOfAvailments).HasMaxLength(500);

                entity.Property(e => e.NumberOfRegistrations).HasMaxLength(500);

                entity.Property(e => e.OneTime).HasMaxLength(500);

                entity.Property(e => e.OtherMedical).HasMaxLength(500);

                entity.Property(e => e.PainManagement).HasMaxLength(500);

                entity.Property(e => e.PreExistingConCover).HasMaxLength(500);

                entity.Property(e => e.PrepaidPlan).HasMaxLength(500);

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.RegistrationOfSucceedingVouchers).HasMaxLength(500);

                entity.Property(e => e.RegistrationRules).HasMaxLength(500);

                entity.Property(e => e.ReimbursementNonAccreditedHos).HasMaxLength(500);

                entity.Property(e => e.RoomAccommodation).HasMaxLength(500);

                entity.Property(e => e.SpecialModalities).HasMaxLength(500);

                entity.Property(e => e.SurgerySurgonFees).HasMaxLength(500);

                entity.Property(e => e.Telemedicine).HasMaxLength(500);

                entity.Property(e => e.Therapetic).HasMaxLength(500);

                entity.Property(e => e.TopSixHospitalAccess).HasMaxLength(500);

                entity.Property(e => e.UnlimitedTeleMed).HasMaxLength(500);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.Usage).HasMaxLength(500);

                entity.Property(e => e.UseOfOperationRoom).HasMaxLength(500);

                entity.Property(e => e.Validity).HasMaxLength(500);

                entity.Property(e => e.Waiting).HasMaxLength(500);
            });

            modelBuilder.Entity<TblProducts>(entity =>
            {
                entity.HasKey(e => e.ProductId);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.PriceWithOffer).HasMaxLength(100);

                entity.Property(e => e.ProductCode)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ProductImg)
                    .IsRequired()
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ProductPrice)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ShortDescription).HasMaxLength(1000);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblUserRoles>(entity =>
            {
                entity.HasKey(e => e.UserRoleId);

                entity.Property(e => e.UserRole)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<TblUsers>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.ActivationCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ActivationDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Dob)
                    .HasColumnName("DOB")
                    .HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(300)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.LastName).HasMaxLength(150);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Phone).HasMaxLength(100);

                entity.Property(e => e.RestCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RestDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UserImg).HasMaxLength(500);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
