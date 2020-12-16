using API.Dtos.Registration;
using Microsoft.EntityFrameworkCore;

namespace API.Models.FluentApi
{
    public static class RegisterModelConfig
    {
        public static void AddRegistrationModelConfig(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Registration>()
                .Property(r => r.EventId)
                .HasDefaultValue(null);

            modelBuilder.Entity<Registration>()                
                .HasOne(r => r.Event)
                .WithMany(e => e.Registrations)
                .HasForeignKey(r => r.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Registration>()
                .HasOne(r => r.Team)
                .WithMany(t => t.Registrations)
                .HasForeignKey(t => t.TeamId);

            modelBuilder.Entity<Registration>()
                .HasIndex(registration => new {registration.EventId, registration.ExcelId}).IsUnique();
        }
    }
}