using System;

namespace API.Dtos.Bookmark
{
    public class EventForBookmarkListViewDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string EventType { get; set; }
        public string Category { get; set; }
        public DateTime Datetime { get; set; }
        public bool IsRegistered { get; set; }
    }
}