﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using iTechArt.SurveysSite.Repositories.DbContexts;

namespace iTechArt.SurveysSite.Repositories.Migrations
{
    [DbContext(typeof(SurveysSiteDbContext))]
    [Migration("20210903084807_Add_survey_question")]
    partial class Add_survey_question
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("iTechArt.SurveysSite.DomainModel.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Role");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = 2,
                            Name = "User",
                            NormalizedName = "USER"
                        });
                });

            modelBuilder.Entity("iTechArt.SurveysSite.DomainModel.Survey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("ChangeDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Survey");
                });

            modelBuilder.Entity("iTechArt.SurveysSite.DomainModel.SurveyQuestion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("QuestionType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SurveyId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("SurveyId");

                    b.ToTable("SurveyQuestion");
                });

            modelBuilder.Entity("iTechArt.SurveysSite.DomainModel.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(35)
                        .HasColumnType("nvarchar(35)");

                    b.Property<string>("NormalizedUserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("iTechArt.SurveysSite.DomainModel.UserRole", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("iTechArt.SurveysSite.DomainModel.Survey", b =>
                {
                    b.HasOne("iTechArt.SurveysSite.DomainModel.User", "Owner")
                        .WithMany("Surveys")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("iTechArt.SurveysSite.DomainModel.SurveyQuestion", b =>
                {
                    b.HasOne("iTechArt.SurveysSite.DomainModel.Survey", "Survey")
                        .WithMany("Questions")
                        .HasForeignKey("SurveyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Survey");
                });

            modelBuilder.Entity("iTechArt.SurveysSite.DomainModel.UserRole", b =>
                {
                    b.HasOne("iTechArt.SurveysSite.DomainModel.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("iTechArt.SurveysSite.DomainModel.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("iTechArt.SurveysSite.DomainModel.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("iTechArt.SurveysSite.DomainModel.Survey", b =>
                {
                    b.Navigation("Questions");
                });

            modelBuilder.Entity("iTechArt.SurveysSite.DomainModel.User", b =>
                {
                    b.Navigation("Surveys");

                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}