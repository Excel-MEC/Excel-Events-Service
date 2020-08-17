using Microsoft.EntityFrameworkCore;

namespace API.Models.FluentApi
{
    public static class HighlightModelConfig
    {
        public static void AddHighlightModelConfig(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Highlight>()
                .Property(h => h.Name)
                .IsRequired();
            modelBuilder.Entity<Highlight>()
                .Property(h => h.Image)
                .IsRequired();
        }
    }
}