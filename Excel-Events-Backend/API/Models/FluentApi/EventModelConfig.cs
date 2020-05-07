using Microsoft.EntityFrameworkCore;

namespace API.Models.FluentApi
{
    public static class EventModelConfig
    {
        public static void AddEventModelConfig(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>()
                .HasIndex(e => e.Name)
                .IsUnique();

            modelBuilder.Entity<Event>()
                .Property(e => e.CategoryId)
                .HasDefaultValue(0);

            modelBuilder.Entity<Event>()
                .Property(e => e.EventTypeId)
                .HasDefaultValue(0);

            modelBuilder.Entity<Event>()
                .Property(e => e.EntryFee)
                .HasDefaultValue(null);

            modelBuilder.Entity<Event>()
                .Property(e => e.PrizeMoney)
                .HasDefaultValue(null);

            modelBuilder.Entity<Event>()
                .Property(e => e.EventHead1Id)
                .HasDefaultValue(null);

            modelBuilder.Entity<Event>()
                .Property(e => e.EventHead2Id)
                .HasDefaultValue(null);

            modelBuilder.Entity<Event>()
                .Property(e => e.TeamSize)
                .HasDefaultValue(null);

            modelBuilder.Entity<Event>()
                .Property(e => e.EventStatus)
                .HasDefaultValue(0);

            modelBuilder.Entity<Event>()
                .Property(e => e.NumberOfRounds)
                .HasDefaultValue(null);

            modelBuilder.Entity<Event>()
                .Property(e => e.CurrentRound)
                .HasDefaultValue(null);

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