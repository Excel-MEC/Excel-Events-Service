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
        }
    }
}