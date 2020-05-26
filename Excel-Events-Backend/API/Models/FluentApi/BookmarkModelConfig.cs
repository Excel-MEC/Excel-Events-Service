using Microsoft.EntityFrameworkCore;

namespace API.Models.FluentApi
{
    public static class BookmarkModelConfig
    {
        public static void AddBookmarkModelConfig(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bookmark>()
                .Property(r => r.EventId)
                .HasDefaultValue(null);

            modelBuilder.Entity<Bookmark>()                
                .HasOne(r => r.Event)
                .WithMany(e => e.Bookmarks)
                .HasForeignKey(r => r.EventId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}