using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data.Interfaces;
using API.Dtos.Highlight;
using API.Extensions.CustomExceptions;
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

        public async Task<Highlight> AddHighlight(DataForAddingHighlightDto dataForAddingHighlight)
        {
            var newHighlight = new Highlight {Name = dataForAddingHighlight.Name};            
            await _context.Highlights.AddAsync(newHighlight);
            await _context.SaveChangesAsync();
            var imageUrl = await _service.UploadHighlightImage(newHighlight.Id.ToString(), dataForAddingHighlight.Image);
            newHighlight.Image = imageUrl;
            if(await _context.SaveChangesAsync() > 0) return newHighlight;
            throw new Exception("Failed to Add Highlight");
        }

        public async Task<List<Highlight>> GetHighlights()
        {
            var highlights = await _context.Highlights.ToListAsync();
            return highlights;
        }

        public async Task<Highlight> DeleteHighlight(DataForDeletingHighlightDto dataForDeletingHighlight)
        {
            var highlightToRemove = await _context.Highlights.FindAsync(dataForDeletingHighlight.Id);
            if (highlightToRemove.Name != dataForDeletingHighlight.Name)
                throw new DataInvalidException("Name and Id does not match");
            var imageUrl = highlightToRemove.Image;
            if(imageUrl != null)
                await _service.DeleteHighlightImage(highlightToRemove.Id, imageUrl);
            _context.Highlights.Remove(highlightToRemove);
            if(await _context.SaveChangesAsync() > 0) return highlightToRemove;
            throw new Exception("Error deleting the Highlight");
        }
    }
}