using Microsoft.EntityFrameworkCore;

namespace API.Models.FluentApi
{
    public static class EventModelConfig
    {
        public static void AddEventModelConfig(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>()
                .Ignore(e => e.Category)
                .Ignore(e => e.EventType)
                .Ignore(e => e.EventStatus);

            modelBuilder.Entity<Event>()
                .HasOne(e => e.EventHead1)
                .WithMany()
                .HasForeignKey(e => e.EventHead1Id)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Event>()
                .HasOne(e => e.EventHead2)
                .WithMany()
                .HasForeignKey(e => e.EventHead2Id)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}