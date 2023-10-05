﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UOAmarking.Data;

#nullable disable

namespace UOAmarking.Migrations
{
    [DbContext(typeof(WebAPIDBContext))]
    [Migration("20230928083159_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CourseCourseSupervisor", b =>
                {
                    b.Property<int>("CourseSupervisorId")
                        .HasColumnType("int");

                    b.Property<int>("coursesId")
                        .HasColumnType("int");

                    b.HasKey("CourseSupervisorId", "coursesId");

                    b.HasIndex("coursesId");

                    b.ToTable("CourseCourseSupervisor");
                });

            modelBuilder.Entity("CourseUser", b =>
                {
                    b.Property<int>("coursesId")
                        .HasColumnType("int");

                    b.Property<int>("markersId")
                        .HasColumnType("int");

                    b.HasKey("coursesId", "markersId");

                    b.HasIndex("markersId");

                    b.ToTable("CourseUser");
                });

            modelBuilder.Entity("UOAmarking.Models.Admin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("UOAmarking.Models.Application", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CourseId")
                        .HasColumnType("int");

                    b.Property<string>("currentStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("haveDoneBefore")
                        .HasColumnType("bit");

                    b.Property<string>("haveDoneReleventCourse")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("haveMarkedBefore")
                        .HasColumnType("bit");

                    b.Property<bool>("isRecommanded")
                        .HasColumnType("bit");

                    b.Property<double>("previousGrade")
                        .HasColumnType("float");

                    b.Property<int?>("userId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("userId");

                    b.ToTable("Applications");
                });

            modelBuilder.Entity("UOAmarking.Models.Assignment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CourseId")
                        .HasColumnType("int");

                    b.Property<string>("assignmentType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("description")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("Assignments");
                });

            modelBuilder.Entity("UOAmarking.Models.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CourseName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CourseNumber")
                        .HasColumnType("int");

                    b.Property<int>("EnrolledStudents")
                        .HasColumnType("int");

                    b.Property<int>("EstimatedStudents")
                        .HasColumnType("int");

                    b.Property<bool>("NeedsMarker")
                        .HasColumnType("bit");

                    b.Property<string>("Overview")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SemesterId")
                        .HasColumnType("int");

                    b.Property<double>("TotalMarkingHour")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("SemesterId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("UOAmarking.Models.CourseSupervisor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isDirector")
                        .HasColumnType("bit");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("courseSupervisors");
                });

            modelBuilder.Entity("UOAmarking.Models.MarkerCoordinator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("markerCoordinators");
                });

            modelBuilder.Entity("UOAmarking.Models.MarkingHours", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("courseId")
                        .HasColumnType("int");

                    b.Property<double>("remainHour")
                        .HasColumnType("float");

                    b.Property<int?>("userId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("courseId");

                    b.HasIndex("userId");

                    b.ToTable("MarkingHours");
                });

            modelBuilder.Entity("UOAmarking.Models.Semester", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("SemesterType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Semesters");
                });

            modelBuilder.Entity("UOAmarking.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AUID")
                        .HasColumnType("int");

                    b.Property<byte[]>("AcademicRecord")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("CV")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UPI")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UnderOrPost")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("descriptionOfContracts")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("enrolmentDetail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("haveOtherContracts")
                        .HasColumnType("bit");

                    b.Property<bool>("isCitizenOrPermanent")
                        .HasColumnType("bit");

                    b.Property<bool>("isOverseas")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CourseCourseSupervisor", b =>
                {
                    b.HasOne("UOAmarking.Models.CourseSupervisor", null)
                        .WithMany()
                        .HasForeignKey("CourseSupervisorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UOAmarking.Models.Course", null)
                        .WithMany()
                        .HasForeignKey("coursesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CourseUser", b =>
                {
                    b.HasOne("UOAmarking.Models.Course", null)
                        .WithMany()
                        .HasForeignKey("coursesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UOAmarking.Models.User", null)
                        .WithMany()
                        .HasForeignKey("markersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("UOAmarking.Models.Application", b =>
                {
                    b.HasOne("UOAmarking.Models.Course", "Course")
                        .WithMany("applications")
                        .HasForeignKey("CourseId");

                    b.HasOne("UOAmarking.Models.User", "user")
                        .WithMany("applications")
                        .HasForeignKey("userId");

                    b.Navigation("Course");

                    b.Navigation("user");
                });

            modelBuilder.Entity("UOAmarking.Models.Assignment", b =>
                {
                    b.HasOne("UOAmarking.Models.Course", "Course")
                        .WithMany("assignments")
                        .HasForeignKey("CourseId");

                    b.Navigation("Course");
                });

            modelBuilder.Entity("UOAmarking.Models.Course", b =>
                {
                    b.HasOne("UOAmarking.Models.Semester", "Semester")
                        .WithMany("Courses")
                        .HasForeignKey("SemesterId");

                    b.Navigation("Semester");
                });

            modelBuilder.Entity("UOAmarking.Models.MarkingHours", b =>
                {
                    b.HasOne("UOAmarking.Models.Course", "course")
                        .WithMany("remainHours")
                        .HasForeignKey("courseId");

                    b.HasOne("UOAmarking.Models.User", "user")
                        .WithMany("remainHours")
                        .HasForeignKey("userId");

                    b.Navigation("course");

                    b.Navigation("user");
                });

            modelBuilder.Entity("UOAmarking.Models.Course", b =>
                {
                    b.Navigation("applications");

                    b.Navigation("assignments");

                    b.Navigation("remainHours");
                });

            modelBuilder.Entity("UOAmarking.Models.Semester", b =>
                {
                    b.Navigation("Courses");
                });

            modelBuilder.Entity("UOAmarking.Models.User", b =>
                {
                    b.Navigation("applications");

                    b.Navigation("remainHours");
                });
#pragma warning restore 612, 618
        }
    }
}
