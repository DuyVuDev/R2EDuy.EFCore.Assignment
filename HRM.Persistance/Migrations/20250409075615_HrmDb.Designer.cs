﻿// <auto-generated />
using System;
using HRM.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HRM.Persistance.Migrations
{
    [DbContext(typeof(HRMDbContext))]
    [Migration("20250409075615_HrmDb")]
    partial class HrmDb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HRM.Domain.Entities.Department", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("HRM.Domain.Entities.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DepartmentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateOnly>("JoinedDate")
                        .HasColumnType("date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("SalaryId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("SalaryId")
                        .IsUnique();

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("HRM.Domain.Entities.Project", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("HRM.Domain.Entities.ProjectEmployee", b =>
                {
                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Enabled")
                        .HasColumnType("bit");

                    b.HasKey("EmployeeId", "ProjectId");

                    b.HasIndex("ProjectId");

                    b.ToTable("ProjectEmployees");
                });

            modelBuilder.Entity("HRM.Domain.Entities.Salary", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Salaries");
                });

            modelBuilder.Entity("HRM.Domain.Entities.Employee", b =>
                {
                    b.HasOne("HRM.Domain.Entities.Department", "Department")
                        .WithMany("Employees")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HRM.Domain.Entities.Salary", "Salary")
                        .WithOne("Employee")
                        .HasForeignKey("HRM.Domain.Entities.Employee", "SalaryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("Salary");
                });

            modelBuilder.Entity("HRM.Domain.Entities.ProjectEmployee", b =>
                {
                    b.HasOne("HRM.Domain.Entities.Employee", "Employee")
                        .WithMany("ProjectEmployees")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HRM.Domain.Entities.Project", "Project")
                        .WithMany("ProjectEmployees")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("HRM.Domain.Entities.Department", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("HRM.Domain.Entities.Employee", b =>
                {
                    b.Navigation("ProjectEmployees");
                });

            modelBuilder.Entity("HRM.Domain.Entities.Project", b =>
                {
                    b.Navigation("ProjectEmployees");
                });

            modelBuilder.Entity("HRM.Domain.Entities.Salary", b =>
                {
                    b.Navigation("Employee")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
