﻿// <auto-generated />
using System;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240323020121_update_product_entity_to_be_more_flexible")]
    partial class update_product_entity_to_be_more_flexible
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Customers.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("MobileNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RedirectURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Domain.Entities.LoanApplications.LoanApplicaton", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Interest")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("InterestRate")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("Repayment")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("RepaymentFrequency")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalRepayments")
                        .HasColumnType("decimal(18,4)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("LoanApplicatons");
                });

            modelBuilder.Entity("Domain.Entities.Loans.Loan", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AmountRequired")
                        .HasColumnType("int");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Term")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId")
                        .IsUnique();

                    b.ToTable("Loans");
                });

            modelBuilder.Entity("Domain.Entities.Product.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("MinimumDuration")
                        .HasColumnType("int");

                    b.Property<int>("MonthsOfFreeInterest")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("PerAnnumInterestRate")
                        .HasColumnType("decimal(18,4)");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = new Guid("9e6287bf-c582-4665-a847-37748d01fb6b"),
                            MinimumDuration = 1,
                            MonthsOfFreeInterest = 0,
                            Name = "Product A",
                            PerAnnumInterestRate = 0m
                        },
                        new
                        {
                            Id = new Guid("2b065d0a-9c86-462e-aac1-6c8d9cab89de"),
                            MinimumDuration = 1,
                            MonthsOfFreeInterest = 2,
                            Name = "Product B",
                            PerAnnumInterestRate = 9.20m
                        },
                        new
                        {
                            Id = new Guid("27b12ed1-92f9-4a03-8242-282581cc55cd"),
                            MinimumDuration = 1,
                            MonthsOfFreeInterest = 0,
                            Name = "Product C",
                            PerAnnumInterestRate = 10.58m
                        });
                });

            modelBuilder.Entity("Domain.Entities.LoanApplications.LoanApplicaton", b =>
                {
                    b.HasOne("Domain.Entities.Customers.Customer", null)
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.Loans.Loan", b =>
                {
                    b.HasOne("Domain.Entities.Customers.Customer", "Customer")
                        .WithOne("Loan")
                        .HasForeignKey("Domain.Entities.Loans.Loan", "CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Domain.Entities.Customers.Customer", b =>
                {
                    b.Navigation("Loan")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}