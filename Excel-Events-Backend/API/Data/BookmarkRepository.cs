using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data.Interfaces;
using API.Dtos.Bookmark;
using API.Extensions.CustomExceptions;
using API.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class BookmarkRepository : IBookmarkRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IRegistrationRepository _repo;
        public BookmarkRepository(DataContext context, IMapper mapper, IRegistrationRepository repo)
        {
            _repo = repo;
            _mapper = mapper;
            _context = context;
        }

        public async Task<BookmarkForViewDto> Add(int excelId, int eventId)
        {
            if( await _context.Bookmarks.FirstOrDefaultAsync(x => x.ExcelId == excelId && x.EventId == eventId) != null)
                throw new InvalidOperationException(" Event is already in bookmarks. ");
            var favorite = new Bookmark
            {
                ExcelId = excelId, EventId = eventId
            };
            await _context.Bookmarks.AddAsync(favorite);
            if (await _context.SaveChangesAsync() > 0) return _mapper.Map<BookmarkForViewDto>(favorite);
            throw new Exception("Problem in bookmarking the event");
        }

        public async Task<List<EventForBookmarkListViewDto>> EventList(int excelId)
        {
            var registrations = await _context.Bookmarks.Where(r => r.ExcelId == excelId)
                .Include(x => x.Event)
                .ToListAsync();
            return registrations.Select(x => _mapper.Map<EventForBookmarkListViewDto>(x.Event)).ToList();
        }

        public async Task<BookmarkForViewDto> Remove(int excelId, int eventId)
        {
            var fav = await _context.Bookmarks.FirstOrDefaultAsync(x => x.ExcelId == excelId && x.EventId == eventId);
            _context.Remove(fav);
            if(await _context.SaveChangesAsync() > 0) return _mapper.Map<BookmarkForViewDto>(fav);
            throw new Exception("Problem removing the bookmarked event");
        }

        public async Task<List<BookmarkForViewDto>> RemoveAll(int excelId)
        {
            var bookmarks = await _context.Bookmarks.Where(r => r.ExcelId == excelId).ToListAsync();
            if(bookmarks.Count == 0) throw new DataInvalidException("Invalid User ID. Please re-check the user ID");
            _context.RemoveRange(bookmarks);
            if (await _context.SaveChangesAsync() > 0)
                return bookmarks.Select(x => _mapper.Map<BookmarkForViewDto>(x)).ToList();
            throw new Exception("Problem clearing bookmarks. Check out the userid");
        }
    }
}