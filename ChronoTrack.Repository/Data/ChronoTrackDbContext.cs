using ChronoTrack.Repository.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoTrack.Repository.Data
{
    public class ChronoTrackDbContext: IdentityDbContext<User>
    {
        public ChronoTrackDbContext(DbContextOptions<ChronoTrackDbContext> options) : base(options)
        {
        }

        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<TimeEntry> TimeEntries { get; set; }
        public DbSet<OrganizationUser> OrganizationUsers { get; set; }
        public DbSet<ProjectUser> ProjectUsers { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Organization configuration
            builder.Entity<Organization>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.HasIndex(e => e.Name).IsUnique();
            });

            // Project configuration
            builder.Entity<Project>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.HourlyRate).HasColumnType("decimal(18,2)");
                entity.HasOne(e => e.Organization)
                      .WithMany(e => e.Projects)
                      .HasForeignKey(e => e.OrganizationId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // TimeEntry configuration
            builder.Entity<TimeEntry>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Description).HasMaxLength(1000);
                entity.Property(e => e.HoursWorked).HasColumnType("decimal(18,2)");
                entity.HasOne(e => e.User)
                      .WithMany(e => e.TimeEntries)
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.Project)
                      .WithMany(e => e.TimeEntries)
                      .HasForeignKey(e => e.ProjectId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // OrganizationUser configuration
            builder.Entity<OrganizationUser>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.OrganizationId });
                entity.HasOne(e => e.User)
                      .WithMany(e => e.OrganizationUsers)
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.Organization)
                      .WithMany(e => e.OrganizationUsers)
                      .HasForeignKey(e => e.OrganizationId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // ProjectUser configuration
            builder.Entity<ProjectUser>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.ProjectId });
                entity.HasOne(e => e.User)
                      .WithMany(e => e.ProjectUsers)
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.Project)
                      .WithMany(e => e.ProjectUsers)
                      .HasForeignKey(e => e.ProjectId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // RefreshToken configuration
            builder.Entity<RefreshToken>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Token).IsRequired().HasMaxLength(500);
                entity.HasOne(e => e.User)
                      .WithMany(e => e.RefreshTokens)
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
