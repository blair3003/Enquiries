﻿using Microsoft.EntityFrameworkCore;

namespace Enquiries
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Enquiry> Enquiries { get; set; }
        public DbSet<Media> Media { get; set; }
        public DbSet<Reporter> Reporters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Defining a unique index on the Name property of the Media entity
            modelBuilder.Entity<Media>()
                .HasIndex(m => m.Name)
                .IsUnique();

            // Defining a unique index on the Name property of the Reporter entity
            modelBuilder.Entity<Reporter>()
                .HasIndex(r => r.Name)
                .IsUnique();
        }
    }
}
