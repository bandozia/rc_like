using Microsoft.EntityFrameworkCore;
using RCLike.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RCLike.Data.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasPostgresExtension("uuid-ossp");

            modelBuilder.Entity<AppUser>(user =>
            {
                user.HasKey(u => u.Id);
                user.HasIndex(u => u.Id).IsUnique();
                user.HasIndex(u => u.Email).IsUnique();

                user.HasData(new AppUser { Id = Guid.NewGuid(), Email = "test@user.com" });
            });

            modelBuilder.Entity<UrlSource>(url =>
            {
                url.HasKey(u => u.Id);
                url.HasIndex(u => u.Id).IsUnique();
                url.HasIndex(u => u.Url).IsUnique();

                url.HasMany(u => u.UsersWhoLiked).WithMany(u => u.LikedUrls).UsingEntity(j => j.ToTable("Likes"));
            });

        }
    }
}
