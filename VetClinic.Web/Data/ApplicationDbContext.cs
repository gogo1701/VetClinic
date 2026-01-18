using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VetClinic.Web.Models;

namespace VetClinic.Web.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Pet> Pets => Set<Pet>();
    public DbSet<Owner> Owners => Set<Owner>();
    public DbSet<Appointment> Appointments => Set<Appointment>();
    public DbSet<MedicalRecord> MedicalRecords => Set<MedicalRecord>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Pet>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Species).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Breed).HasMaxLength(100);
            entity.Property(e => e.Weight).HasPrecision(10, 2);

            entity.HasOne(e => e.Owner)
                  .WithMany(o => o.Pets)
                  .HasForeignKey(e => e.OwnerId)
                  .OnDelete(DeleteBehavior.SetNull);
        });

        builder.Entity<Owner>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
            entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Phone).IsRequired().HasMaxLength(20);
        });

        builder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Reason).IsRequired().HasMaxLength(500);

            entity.HasOne(e => e.Pet)
                  .WithMany(p => p.Appointments)
                  .HasForeignKey(e => e.PetId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<MedicalRecord>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Diagnosis).IsRequired().HasMaxLength(1000);
            entity.Property(e => e.Cost).HasPrecision(10, 2);

            entity.HasOne(e => e.Pet)
                  .WithMany(p => p.MedicalRecords)
                  .HasForeignKey(e => e.PetId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}