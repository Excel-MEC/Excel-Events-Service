using Microsoft.EntityFrameworkCore;

namespace API.Models.FluentApi
{
    public static class ResultModelConfig
    {
        public static void AddResultModelConfig(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Result>()
                .Property(r => r.EventId)
                .HasDefaultValue(null);

            modelBuilder.Entity<Result>()                
                .HasOne(r => r.Event)
                .WithMany(e => e.Results)
                .HasForeignKey(r => r.EventId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}