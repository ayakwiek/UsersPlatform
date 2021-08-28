using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UsersPlatform.Areas.Identity.Data;
using UsersPlatform.Models;

namespace UsersPlatform.Data
{
    public class UsersPlatformContext : IdentityDbContext<ApplicationUser>
    {
        public UsersPlatformContext()
            : base()
        {
        }
        public UsersPlatformContext(DbContextOptions<UsersPlatformContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ProjectUser>().HasKey(sc => new { sc.UserId, sc.ProjectId });

            builder.Entity<ProjectUser>()
                .HasOne<Project>(sc => sc.Project)
                .WithMany(s => s.ProjectUser)
                .HasForeignKey(sc => sc.ProjectId);


            builder.Entity<ProjectUser>()
                .HasOne<ApplicationUser>(sc => sc.User)
                .WithMany(s => s.ProjectUser)
                .HasForeignKey(sc => sc.UserId);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<UsersPlatform.Models.ProjectRole> ProjectRole { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<ProjectUser> ProjectUser { get; set; }

    }
}
