using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dtos.Bookmark;

namespace API.Data.Interfaces
{
    public interface IBookmarkRepository
    {
        Task<bool> Add(int excelId, int eventId);   
        Task<bool> Remove(int excelId, int eventId);   
        Task<bool> RemoveAll(int excelId);
        Task<List<EventForBookmarkListViewDto>> EventList(int excelId);
    }
}