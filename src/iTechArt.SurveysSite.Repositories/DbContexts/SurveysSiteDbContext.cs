﻿using iTechArt.SurveysSite.DomainModel;
using Microsoft.EntityFrameworkCore;

namespace iTechArt.SurveysSite.Repositories.DbContexts
{
    public class SurveysSiteDbContext : DbContext
    {
        public SurveysSiteDbContext(DbContextOptions<SurveysSiteDbContext> options)
            : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(options =>
            {
                options.Property(user => user.UserName)
                    .IsRequired();

                options.Property(user => user.PasswordHash)
                    .IsRequired();

                options.Property(user => user.NormalizedUserName)
                    .IsRequired();

                options.Property(user => user.Email)
                    .IsRequired();
            });

            modelBuilder.Entity<Role>(options =>
            {
                options.Property(role => role.Name)
                    .IsRequired();

                options.Property(role => role.NormalizedName)
                    .IsRequired();

                options.HasData(new Role
                    {
                        Id = 1,
                        Name = RoleNames.AdminRole,
                        NormalizedName = RoleNames.AdminRole.ToUpper()
                    },
                    new Role
                    {
                        Id = 2,
                        Name = RoleNames.UserRole,
                        NormalizedName = RoleNames.UserRole.ToUpper()
                    });
            });

            modelBuilder.Entity<UserRole>(options =>
            {
                options.HasKey(ur => new {ur.UserId, ur.RoleId});

                options.HasOne(ur => ur.User)
                    .WithMany(user => user.UserRoles)
                    .HasForeignKey(ur => ur.UserId);

                options.HasOne(ur => ur.Role)
                    .WithMany(role => role.UserRoles)
                    .HasForeignKey(ur => ur.RoleId);
            });

            modelBuilder.Entity<Survey>(options =>
            {
                options.Property(survey => survey.Title)
                    .IsRequired();

                options.HasOne(survey => survey.Owner)
                    .WithMany(user => user.Surveys)
                    .IsRequired();
            });
        }
    }
}