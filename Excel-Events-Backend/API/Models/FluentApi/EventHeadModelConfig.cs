using Microsoft.EntityFrameworkCore;

namespace API.Models.FluentApi
{
    public static class EventHeadModelConfig 
    {
        public static void AddEventHeadModelConfig(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventHead>()
            .HasIndex(e => e.Email)
            .IsUnique();
        }
    }
}