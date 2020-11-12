using Microsoft.EntityFrameworkCore;

namespace API.Models.FluentApi
{
    public static class TeamModelConfig
    {
        public static void AddTeamModelConfig(this ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Team>()
                .Property(t => t.Id)
                .HasIdentityOptions(startValue: 2246, incrementBy: 37);
            
            modelBuilder.Entity<Team>()
                .HasOne(t => t.Event)
                .WithMany(e => e.Teams)
                .HasForeignKey(t => t.EventId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}