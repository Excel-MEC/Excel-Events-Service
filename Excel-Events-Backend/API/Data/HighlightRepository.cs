using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data.Interfaces;
using API.Dtos.Highlight;
using API.Models;
using API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class HighlightRepository : IHighlightRepository
    {
        private readonly DataContext _context;
        private readonly IHighlightService _service;

        public HighlightRepository(DataContext context, IHighlightService service)
        {
            _context = context;
            _service = service;
        }

        public async Task<bool> AddHighlight(DataForAddingHighlightDto dataForAddingHighlight)
        {
            Highlight newHighlight = new Highlight();
            newHighlight.Name = dataForAddingHighlight.Name;
            _context.Highlights.Add(newHighlight);
            await _context.SaveChangesAsync();
            string imageUrl = await _service.UploadHighlightImage(newHighlight.Id.ToString(), dataForAddingHighlight.Image);
            newHighlight.Image = imageUrl;
            bool success = await _context.SaveChangesAsync() > 0;
            return success;
        }

        public async Task<List<Highlight>> GetHighlights()
        {
            List<Highlight> highlights = await _context.Highlights.ToListAsync();
            return highlights;
        }

        public async Task<bool> DeleteHighlight(DataForDeletingHighlightDto dataForDeletingHighlight)
        {
            Highlight highlightToRemove = await _context.Highlights.FindAsync(dataForDeletingHighlight.Id);
            if (highlightToRemove.Name == dataForDeletingHighlight.Name)
            {
                string imageUrl = highlightToRemove.Image;
                await _service.DeleteHighlightImage(highlightToRemove.Id, imageUrl);
                _context.Highlights.Remove(highlightToRemove);
                bool success = await _context.SaveChangesAsync() > 0;
                return success;
            }
            throw new System.Exception("Name and Id doesnot match");
        }
    }
}