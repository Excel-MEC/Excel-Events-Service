using API.Models;
using API.Models.FluentApi;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddEventModelConfig();
            modelBuilder.AddRegistrationModelConfig();
            modelBuilder.AddBookmarkModelConfig();
        }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventHead> EventHeads { get; set; }
        public DbSet<Highlight> Highlights { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Bookmark> Bookmarks { get; set; }
    }

}