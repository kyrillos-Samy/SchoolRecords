﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistence;

#nullable disable

namespace Persistence.Migrations
{
    [DbContext(typeof(SchoolRecordsContext))]
    [Migration("20230325182927_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Entities.Class", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("Classes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CourseId = 1,
                            Name = "Class A"
                        });
                });

            modelBuilder.Entity("Entities.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Courses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Math 1"
                        });
                });

            modelBuilder.Entity("Entities.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.HasKey("Id");

                    b.ToTable("Students");

                    b.HasData(
                        new
                        {
                            Id = 1
                        });
                });

            modelBuilder.Entity("Entities.StudentClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ClassId")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClassId");

                    b.HasIndex("StudentId");

                    b.ToTable("StudentClass");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ClassId = 1,
                            StudentId = 1
                        });
                });

            modelBuilder.Entity("Entities.StudentData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("StudentDataConfigurationId")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("StudentDataConfigurationId");

                    b.HasIndex("StudentId");

                    b.ToTable("StudentData");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            StudentDataConfigurationId = 1,
                            StudentId = 1,
                            Value = "john"
                        },
                        new
                        {
                            Id = 2,
                            StudentDataConfigurationId = 2,
                            StudentId = 1,
                            Value = "Doe"
                        },
                        new
                        {
                            Id = 3,
                            StudentDataConfigurationId = 3,
                            StudentId = 1,
                            Value = "John.Doe@gmail.com"
                        });
                });

            modelBuilder.Entity("Entities.StudentDataConfiguration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("FieldName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsVisable")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("StudentDataConfiguration");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FieldName = "First Name",
                            IsVisable = true
                        },
                        new
                        {
                            Id = 2,
                            FieldName = "Last Name",
                            IsVisable = true
                        },
                        new
                        {
                            Id = 3,
                            FieldName = "Email",
                            IsVisable = true
                        });
                });

            modelBuilder.Entity("Entities.Class", b =>
                {
                    b.HasOne("Entities.Course", "Course")
                        .WithMany("Classes")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("Entities.StudentClass", b =>
                {
                    b.HasOne("Entities.Class", "Class")
                        .WithMany()
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Class");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Entities.StudentData", b =>
                {
                    b.HasOne("Entities.StudentDataConfiguration", "StudentDataConfiguration")
                        .WithMany()
                        .HasForeignKey("StudentDataConfigurationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Student", "Student")
                        .WithMany("StudentData")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");

                    b.Navigation("StudentDataConfiguration");
                });

            modelBuilder.Entity("Entities.Course", b =>
                {
                    b.Navigation("Classes");
                });

            modelBuilder.Entity("Entities.Student", b =>
                {
                    b.Navigation("StudentData");
                });
#pragma warning restore 612, 618
        }
    }
}
