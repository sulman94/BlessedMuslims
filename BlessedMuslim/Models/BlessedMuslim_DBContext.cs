﻿using System;
using System.IO;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace BlessedMuslim.Models
{
    public partial class BlessedMuslim_DBContext : DbContext
    {
        public BlessedMuslim_DBContext()
        {
        }

        public BlessedMuslim_DBContext(DbContextOptions<BlessedMuslim_DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Areas> Areas { get; set; }
        public virtual DbSet<BusinessCategories> BusinessCategories { get; set; }
        public virtual DbSet<Charity> Charity { get; set; }
        public virtual DbSet<Cities> Cities { get; set; }
        public virtual DbSet<Config> Config { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<DsrApplicationForm> DsrApplicationForm { get; set; }
        public virtual DbSet<HubAreas> HubAreas { get; set; }
        public virtual DbSet<HubMaster> HubMaster { get; set; }
        public virtual DbSet<MasterAddresses> MasterAddresses { get; set; }
        public virtual DbSet<PaymentDetails> PaymentDetails { get; set; }
        public virtual DbSet<Retailers> Retailers { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<States> States { get; set; }
        public virtual DbSet<UkPostalCodes> UkPostalCodes { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<RetailerContracts> RetailerContracts { get; set; }
        public virtual DbSet<ContractPayments> ContractPayments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                .AddJsonFile("appsettings.json")
                .Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("MBDbConstr"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");
            modelBuilder.Entity<RetailerContracts>(entity =>
            {
                entity.Property(e => e.AccNo)
                    .HasColumnName("Acc_No")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CommissionPercentage).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.ContractAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ContractDate).HasColumnType("datetime");

                entity.Property(e => e.ContractPeriod)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ContractSignedBy)
                    .HasMaxLength(450)
                    .IsUnicode(false);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.SortCode)
                    .HasColumnName("Sort_code")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.StartDate).HasColumnType("datetime");
            });
            modelBuilder.Entity<Areas>(entity =>
            {
                entity.Property(e => e.AreaCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.AreaName).HasMaxLength(500);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Areas)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_Areas_Cities");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Areas)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_Areas_Country");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.Areas)
                    .HasForeignKey(d => d.StateId)
                    .HasConstraintName("FK_Areas_States");
            });

            modelBuilder.Entity<BusinessCategories>(entity =>
            {
                entity.Property(e => e.Ldesc)
                    .HasColumnName("LDesc")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Sdesc)
                    .HasColumnName("SDesc")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Charity>(entity =>
            {
                entity.Property(e => e.AccountNumber)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AddressLine1)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.AddressLine2)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.BankName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.BusinessName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CharityNumber)
                   .IsRequired()
                   .HasMaxLength(50)
                   .IsUnicode(false);

                entity.Property(e => e.CharityId)
                    .IsRequired()
                    .HasColumnName("CharityID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ContactPerson)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.JoiningDate).HasColumnType("datetime");

                entity.Property(e => e.Landline)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MobileNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PostCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SortCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Cities>(entity =>
            {
                entity.Property(e => e.CityName).HasMaxLength(250);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_Cities_Country");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.StateId)
                    .HasConstraintName("FK_Cities_States");
            });

            modelBuilder.Entity<Config>(entity =>
            {
                entity.Property(e => e.ConfigCode)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.ConfigValue).IsUnicode(false);
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.CountryCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CountryName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<DsrApplicationForm>(entity =>
            {
                entity.ToTable("DSR.ApplicationForm");

                entity.Property(e => e.AccountNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AddressLine1)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.AddressLine2)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.ApprovedDate).HasColumnType("datetime");

                entity.Property(e => e.ContactNumber)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.DateOfJoining).HasColumnType("datetime");

                entity.Property(e => e.Dob)
                    .HasColumnName("DOB")
                    .HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Idphoto)
                    .HasColumnName("IDPhoto")
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Phone1)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Phone2)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Photo).IsUnicode(false);

                entity.Property(e => e.PostCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ReferenceCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.RejectedDate).HasColumnType("datetime");

                entity.Property(e => e.Remarks)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.RepId)
                    .HasColumnName("RepID")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.SortCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SubmitDate).HasColumnType("datetime");

                entity.HasOne(d => d.Area)
                    .WithMany(p => p.DsrApplicationForm)
                    .HasForeignKey(d => d.AreaId)
                    .HasConstraintName("FK_ApplicationForm_Areas");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.DsrApplicationForm)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_ApplicationForm_Users");
            });

            modelBuilder.Entity<HubAreas>(entity =>
            {
                entity.Property(e => e.HubId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<HubMaster>(entity =>
            {
                entity.Property(e => e.HubDesc)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.HubId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MasterAddresses>(entity =>
            {
                entity.Property(e => e.AddressName)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PaymentDetails>(entity =>
            {
                entity.Property(e => e.AmountDue).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.AmountPaid).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Attachment).IsUnicode(false);

                entity.Property(e => e.Details)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Comments)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.PaymentMode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PaymentStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TransactionNumber).HasMaxLength(450);
           
            });

            modelBuilder.Entity<Retailers>(entity =>
            {
                entity.Property(e => e.AddressLine1)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.AddressLine2)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.BusinessName)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.Comments)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ContactPerson)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.MobileNumber)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.PostCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ReferenceCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.RegDate).HasColumnType("datetime");

                entity.Property(e => e.ShopPhone)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.HasOne(d => d.CityCodeNavigation)
                    .WithMany(p => p.Retailers)
                    .HasForeignKey(d => d.CityCode)
                    .HasConstraintName("FK_Retailers_Cities");

                entity.HasOne(d => d.RegByNavigation)
                    .WithMany(p => p.Retailers)
                    .HasForeignKey(d => d.RegBy)
                    .HasConstraintName("FK_Retailers_Users");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<States>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.StateName).HasMaxLength(200);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.States)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_States_Country");
            });

            modelBuilder.Entity<UkPostalCodes>(entity =>
            {
                entity.ToTable("UK_PostalCodes");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AreaCode)
                    .IsRequired()
                    .HasColumnName("Area_Code")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AreaName)
                    .HasColumnName("Area_Name")
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.Property(e => e.County)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.District)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PostCode)
                    .IsRequired()
                    .HasColumnName("Post_Code")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Region).IsUnicode(false);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HubId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastLoginDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Users_Role");
            });

            modelBuilder.Entity<ContractPayments>(entity =>
            {
                entity.ToTable("ContractPayments", "dbo");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Comments)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RefNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TransactionDate).HasColumnType("datetime");
            });
        }
    }
}
