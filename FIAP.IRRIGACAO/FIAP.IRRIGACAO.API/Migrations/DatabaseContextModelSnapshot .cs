﻿// <auto-generated />
using System;
using FIAP.IRRIGACAO.API.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Oracle.EntityFrameworkCore.Metadata;

#nullable disable

namespace FIAP.IRRIGACAO.API.Migrations
{
    [DbContext(typeof(OracleContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            OracleModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FIAP.IRRIGACAO.API.Models.ApplicationUser", b =>
            {
                b.Property<string>("Id")
                    .HasColumnType("NVARCHAR2(450)");

                b.Property<int>("AccessFailedCount")
                    .HasColumnType("NUMBER(10)");

                b.Property<string>("ConcurrencyStamp")
                    .IsConcurrencyToken()
                    .HasColumnType("NVARCHAR2(2000)");

                b.Property<string>("Email")
                    .HasMaxLength(256)
                    .HasColumnType("NVARCHAR2(256)");

                b.Property<string>("EmailConfirmed")
                    .IsRequired()
                    .HasColumnType("NVARCHAR2(10)");

                b.Property<string>("LockoutEnabled")
                    .IsRequired()
                    .HasColumnType("NVARCHAR2(10)");

                b.Property<DateTimeOffset?>("LockoutEnd")
                    .HasColumnType("TIMESTAMP(7) WITH TIME ZONE");

                b.Property<string>("NormalizedEmail")
                    .HasMaxLength(256)
                    .HasColumnType("NVARCHAR2(256)");

                b.Property<string>("NormalizedUserName")
                    .HasMaxLength(256)
                    .HasColumnType("NVARCHAR2(256)");

                b.Property<string>("PasswordHash")
                    .HasColumnType("NVARCHAR2(2000)");

                b.Property<string>("PhoneNumber")
                    .HasColumnType("NVARCHAR2(2000)");

                b.Property<string>("PhoneNumberConfirmed")
                    .IsRequired()
                    .HasColumnType("NVARCHAR2(10)");

                b.Property<string>("SecurityStamp")
                    .HasColumnType("NVARCHAR2(2000)");

                b.Property<string>("TwoFactorEnabled")
                    .IsRequired()
                    .HasColumnType("NVARCHAR2(10)");

                b.Property<string>("UserName")
                    .HasMaxLength(256)
                    .HasColumnType("NVARCHAR2(256)");

                b.HasKey("Id");

                b.HasIndex("NormalizedEmail")
                    .HasDatabaseName("EmailIndex");

                b.HasIndex("NormalizedUserName")
                    .IsUnique()
                    .HasDatabaseName("UserNameIndex")
                    .HasFilter("\"NormalizedUserName\" IS NOT NULL");

                b.ToTable("AspNetUsers", (string)null);
            });

            modelBuilder.Entity("FIAP.IRRIGACAO.API.Models.FaucetModel", b =>
            {
                b.Property<long>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("NUMBER(19)");

                OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                b.Property<string>("IsEnabled")
                    .IsRequired()
                    .HasColumnType("NVARCHAR2(2000)");

                b.Property<long>("LocationId")
                    .HasColumnType("NUMBER(19)");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnType("NVARCHAR2(2000)");

                b.HasKey("Id");

                b.HasIndex("LocationId");

                b.ToTable("Faucet", (string)null);
            });

            modelBuilder.Entity("FIAP.IRRIGACAO.API.Models.LocationModel", b =>
            {
                b.Property<long>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("NUMBER(19)");

                OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnType("NVARCHAR2(2000)");

                b.HasKey("Id");

                b.ToTable("Location", (string)null);
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
            {
                b.Property<string>("Id")
                    .HasColumnType("NVARCHAR2(450)");

                b.Property<string>("ConcurrencyStamp")
                    .IsConcurrencyToken()
                    .HasColumnType("NVARCHAR2(2000)");

                b.Property<string>("Name")
                    .HasMaxLength(256)
                    .HasColumnType("NVARCHAR2(256)");

                b.Property<string>("NormalizedName")
                    .HasMaxLength(256)
                    .HasColumnType("NVARCHAR2(256)");

                b.HasKey("Id");

                b.HasIndex("NormalizedName")
                    .IsUnique()
                    .HasDatabaseName("RoleNameIndex")
                    .HasFilter("\"NormalizedName\" IS NOT NULL");

                b.ToTable("AspNetRoles", (string)null);
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("NUMBER(10)");

                OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                b.Property<string>("ClaimType")
                    .HasColumnType("NVARCHAR2(2000)");

                b.Property<string>("ClaimValue")
                    .HasColumnType("NVARCHAR2(2000)");

                b.Property<string>("RoleId")
                    .IsRequired()
                    .HasColumnType("NVARCHAR2(450)");

                b.HasKey("Id");

                b.HasIndex("RoleId");

                b.ToTable("AspNetRoleClaims", (string)null);
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("NUMBER(10)");

                OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                b.Property<string>("ClaimType")
                    .HasColumnType("NVARCHAR2(2000)");

                b.Property<string>("ClaimValue")
                    .HasColumnType("NVARCHAR2(2000)");

                b.Property<string>("UserId")
                    .IsRequired()
                    .HasColumnType("NVARCHAR2(450)");

                b.HasKey("Id");

                b.HasIndex("UserId");

                b.ToTable("AspNetUserClaims", (string)null);
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
            {
                b.Property<string>("LoginProvider")
                    .HasColumnType("NVARCHAR2(450)");

                b.Property<string>("ProviderKey")
                    .HasColumnType("NVARCHAR2(450)");

                b.Property<string>("ProviderDisplayName")
                    .HasColumnType("NVARCHAR2(2000)");

                b.Property<string>("UserId")
                    .IsRequired()
                    .HasColumnType("NVARCHAR2(450)");

                b.HasKey("LoginProvider", "ProviderKey");

                b.HasIndex("UserId");

                b.ToTable("AspNetUserLogins", (string)null);
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
            {
                b.Property<string>("UserId")
                    .HasColumnType("NVARCHAR2(450)");

                b.Property<string>("RoleId")
                    .HasColumnType("NVARCHAR2(450)");

                b.HasKey("UserId", "RoleId");

                b.HasIndex("RoleId");

                b.ToTable("AspNetUserRoles", (string)null);
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
            {
                b.Property<string>("UserId")
                    .HasColumnType("NVARCHAR2(450)");

                b.Property<string>("LoginProvider")
                    .HasColumnType("NVARCHAR2(450)");

                b.Property<string>("Name")
                    .HasColumnType("NVARCHAR2(450)");

                b.Property<string>("Value")
                    .HasColumnType("NVARCHAR2(2000)");

                b.HasKey("UserId", "LoginProvider", "Name");

                b.ToTable("AspNetUserTokens", (string)null);
            });

            modelBuilder.Entity("FIAP.IRRIGACAO.API.Models.FaucetModel", b =>
            {
                b.HasOne("FIAP.IRRIGACAO.API.Models.LocationModel", "Location")
                    .WithMany()
                    .HasForeignKey("LocationId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Location");
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
            {
                b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                    .WithMany()
                    .HasForeignKey("RoleId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
            {
                b.HasOne("FIAP.IRRIGACAO.API.Models.ApplicationUser", null)
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
            {
                b.HasOne("FIAP.IRRIGACAO.API.Models.ApplicationUser", null)
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
            {
                b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                    .WithMany()
                    .HasForeignKey("RoleId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("FIAP.IRRIGACAO.API.Models.ApplicationUser", null)
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
            {
                b.HasOne("FIAP.IRRIGACAO.API.Models.ApplicationUser", null)
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });
#pragma warning restore 612, 618
        }
    }
}