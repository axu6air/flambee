using Flambee.Core.Domain.Authentication;
using Flambee.Core.Domain.Image;
using Flambee.Core.Domain.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Flambee.Core.Data
{
    public partial class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            #region Identity Tables

            builder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable(name: "User");
                entity.Property(u => u.Id).HasDefaultValueSql("newsequentialid()");
            });

            builder.Entity<ApplicationRole>(entity =>
            {
                entity.ToTable(name: "Role");
                entity.Property(u => u.Id).HasDefaultValueSql("newsequentialid()");

            });
            builder.Entity<IdentityUserRole<Guid>>(entity =>
            {
                entity.ToTable("UserRoleMapping");
            });

            builder.Entity<IdentityUserClaim<Guid>>(entity =>
            {
                entity.ToTable("UserClaim");
            });

            builder.Entity<IdentityUserLogin<Guid>>(entity =>
            {
                entity.ToTable("UserLogin");
            });

            builder.Entity<IdentityRoleClaim<Guid>>(entity =>
            {
                entity.ToTable("RoleClaim");

            });

            builder.Entity<IdentityUserToken<Guid>>(entity =>
            {
                entity.ToTable("UserToken");
            });


            #endregion

            builder.Entity<ApplicationUser>()
                .HasOne(e => e.UserInfo)
                .WithOne(e => e.ApplicationUser)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasForeignKey<UserInfo>(e => e.ApplicationUserId);
        }


        #region Application Tables

        public DbSet<UserInfo> UserInfo { get; set; }

        public DbSet<Avatar> Avatar { get; set; }

        #endregion

    }
}
