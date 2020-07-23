using Microsoft.EntityFrameworkCore;

namespace API.Models.FluentApi
{
    public static class ScheduleModelConfig
    {
        public static void AddScheduleModelConfig(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Schedule>()
                .Property(s => s.EventId)
                .HasDefaultValue(null);

            modelBuilder.Entity<Schedule>()                
                .HasOne(s => s.Event)
                .WithMany(e => e.Rounds)
                .HasForeignKey(r => r.EventId)
                .OnDelete(DeleteBehavior.Cascade);
        } 
    }
}