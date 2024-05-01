using Enquiries.Models;
using Microsoft.EntityFrameworkCore;

namespace Enquiries
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() { }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Enquiry> Enquiries { get; set; }
        public virtual DbSet<Media> Media { get; set; }
        public virtual DbSet<Reporter> Reporters { get; set; }

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

            // Defining set null behaviour for the Reporter entity on Media deletion 
            modelBuilder.Entity<Reporter>()
                .HasOne(e => e.Media)
                .WithMany(m => m.Reporters)
                .OnDelete(DeleteBehavior.SetNull);

            // Defining set null behaviour for the Enquiry entity on Media deletion 
            modelBuilder.Entity<Enquiry>()
                .HasOne(e => e.Media)
                .WithMany(m => m.Enquiries)
                .OnDelete(DeleteBehavior.SetNull);

            // Defining set null behaviour for the Enquiry entity on Reporter deletion 
            modelBuilder.Entity<Enquiry>()
                .HasOne(e => e.Reporter)
                .WithMany(m => m.Enquiries)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
