using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dtos.Bookmark;
using API.Models;

namespace API.Data.Interfaces
{
    public interface IBookmarkRepository
    {
        Task<BookmarkForViewDto> Add(int excelId, int eventId);   
        Task<BookmarkForViewDto> Remove(int excelId, int eventId);   
        Task<List<BookmarkForViewDto>> RemoveAll(int excelId);
        Task<List<EventForBookmarkListViewDto>> EventList(int excelId);
    }
}