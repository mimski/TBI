﻿// <auto-generated />
using System;
using Loanda.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Loanda.Data.Migrations
{
    [DbContext(typeof(LoandaContext))]
    partial class LoandaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("public")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Loanda.Entities.ApplicantEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("date");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("date");

                    b.Property<string>("EGN")
                        .IsRequired();

                    b.Property<string>("EmailAddress")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(120);

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(120);

                    b.Property<string>("MiddleName")
                        .HasMaxLength(120);

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("date");

                    b.Property<string>("PhoneNumber")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Applicants");
                });

            modelBuilder.Entity("Loanda.Entities.ApplicationStatusEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("ApplicationStatuses");

                    b.HasData(
                        new
                        {
                            Id = -1,
                            Name = "Open"
                        },
                        new
                        {
                            Id = -2,
                            Name = "Closed"
                        });
                });

            modelBuilder.Entity("Loanda.Entities.EmailAttachmentEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AttachmentName")
                        .IsRequired();

                    b.Property<double>("FileSizeInMb");

                    b.Property<long>("ReceivedEmailId");

                    b.HasKey("Id");

                    b.HasIndex("ReceivedEmailId");

                    b.ToTable("EmailAttachments");
                });

            modelBuilder.Entity("Loanda.Entities.EmailStatusEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("EmailStatuses");

                    b.HasData(
                        new
                        {
                            Id = -1,
                            Name = "Not Reviewed"
                        },
                        new
                        {
                            Id = -2,
                            Name = "New"
                        },
                        new
                        {
                            Id = -3,
                            Name = "Invalid"
                        });
                });

            modelBuilder.Entity("Loanda.Entities.LoanApplicationEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ApplicantId");

                    b.Property<int>("ApplicationStatusId");

                    b.Property<string>("ClosedById");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("date");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("date");

                    b.Property<long>("EmailId");

                    b.Property<bool?>("IsApproved")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(null);

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<decimal>("LoanAmount");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("date");

                    b.Property<string>("OpenedById");

                    b.HasKey("Id");

                    b.HasIndex("ApplicantId");

                    b.HasIndex("ApplicationStatusId");

                    b.HasIndex("ClosedById");

                    b.HasIndex("EmailId")
                        .IsUnique();

                    b.HasIndex("OpenedById");

                    b.ToTable("LoanApplications");
                });

            modelBuilder.Entity("Loanda.Entities.ReceivedEmailEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("ApplicantId");

                    b.Property<double>("AttachmentsTotalSizeInMB");

                    b.Property<string>("Body");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("date");

                    b.Property<string>("DateReceived");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("date");

                    b.Property<int?>("EmailStatusId")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(-1);

                    b.Property<string>("GmailEmailId");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("date");

                    b.Property<string>("ProcessedById");

                    b.Property<string>("SenderEmail");

                    b.Property<string>("SenderName");

                    b.Property<string>("Subject");

                    b.Property<int>("TotalAttachments");

                    b.HasKey("Id");

                    b.HasIndex("ApplicantId");

                    b.HasIndex("EmailStatusId");

                    b.HasIndex("ProcessedById");

                    b.ToTable("ReceivedEmails");
                });

            modelBuilder.Entity("Loanda.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsFirstLogin");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");

                    b.HasData(
                        new
                        {
                            Id = "010dbb8b-5eb3-47ef-9c53-22018551b422",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "578ddff5-8f49-48f0-a5c8-69b2bbb20552",
                            Email = "admin@admin.com",
                            EmailConfirmed = false,
                            IsDeleted = false,
                            IsFirstLogin = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN@ADMIN.COM",
                            NormalizedUserName = "ADMIN100@ABV.BG",
                            PasswordHash = "AQAAAAEAACcQAAAAED1sUbN+sPoFdrWJk/ICn2vfzPbC9s6bV/ae5bApThXRg9oKJOUmtPfdAVyfpIsVRw==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "CFQ3UGEFW7IBYB5GSVN2XPABAZW4DMYC",
                            TwoFactorEnabled = false,
                            UserName = "Admin100@abv.bg"
                        },
                        new
                        {
                            Id = "31d4807f-7f5f-4ffa-90c1-a131e2d3855e",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "715dad2a-9a3f-4a7d-bca1-e40799bb172c",
                            Email = "user_pesho@abv.bg",
                            EmailConfirmed = false,
                            IsDeleted = false,
                            IsFirstLogin = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "USER_PESHO@ABV.BG",
                            NormalizedUserName = "USER_PESHO@ABV.BG",
                            PasswordHash = "AQAAAAEAACcQAAAAECewgbwibVC/7nEpYLbJB26wOJyT9i8Dfcx6WFFCTnGy5xqwptVYNBIZEWK37eaaMA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "WNDRYHCTXU3MSZ7NYBDFJQDL5VU2LBXS",
                            TwoFactorEnabled = false,
                            UserName = "user_pesho@abv.bg"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");

                    b.HasData(
                        new
                        {
                            Id = "959596e5-93e4-4272-8cfb-6e71a4254370",
                            ConcurrencyStamp = "20d35162-b35c-4b2e-80c1-81a15bc1b2f3",
                            Name = "Administrator",
                            NormalizedName = "ADMINISTRATOR"
                        },
                        new
                        {
                            Id = "5197310d-5d42-4337-bb59-2fd06e6a8fcd",
                            ConcurrencyStamp = "a3bc9d45-276b-442f-bc6b-b1a5763df30d",
                            Name = "User",
                            NormalizedName = "USER"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");

                    b.HasData(
                        new
                        {
                            UserId = "010dbb8b-5eb3-47ef-9c53-22018551b422",
                            RoleId = "959596e5-93e4-4272-8cfb-6e71a4254370"
                        },
                        new
                        {
                            UserId = "31d4807f-7f5f-4ffa-90c1-a131e2d3855e",
                            RoleId = "5197310d-5d42-4337-bb59-2fd06e6a8fcd"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Loanda.Entities.EmailAttachmentEntity", b =>
                {
                    b.HasOne("Loanda.Entities.ReceivedEmailEntity", "ReceivedEmail")
                        .WithMany("EmailAttachments")
                        .HasForeignKey("ReceivedEmailId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Loanda.Entities.LoanApplicationEntity", b =>
                {
                    b.HasOne("Loanda.Entities.ApplicantEntity", "Applicant")
                        .WithMany("LoanApplication")
                        .HasForeignKey("ApplicantId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Loanda.Entities.ApplicationStatusEntity", "ApplicationStatus")
                        .WithMany("LoanApplications")
                        .HasForeignKey("ApplicationStatusId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Loanda.Entities.User", "ClosedBy")
                        .WithMany("ClosedLoanApplication")
                        .HasForeignKey("ClosedById")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Loanda.Entities.ReceivedEmailEntity", "ReceivedEmail")
                        .WithOne("LoanApplication")
                        .HasForeignKey("Loanda.Entities.LoanApplicationEntity", "EmailId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Loanda.Entities.User", "OpenedBy")
                        .WithMany("OpenLoanApplications")
                        .HasForeignKey("OpenedById")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Loanda.Entities.ReceivedEmailEntity", b =>
                {
                    b.HasOne("Loanda.Entities.ApplicantEntity", "Applicant")
                        .WithMany()
                        .HasForeignKey("ApplicantId");

                    b.HasOne("Loanda.Entities.EmailStatusEntity", "EmailStatus")
                        .WithMany()
                        .HasForeignKey("EmailStatusId");

                    b.HasOne("Loanda.Entities.User", "ProcessedBy")
                        .WithMany("ProcessedEmails")
                        .HasForeignKey("ProcessedById")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Loanda.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Loanda.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Loanda.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Loanda.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
