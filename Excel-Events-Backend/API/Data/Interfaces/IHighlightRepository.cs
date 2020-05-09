using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dtos.Highlight;
using API.Models;

namespace API.Data.Interfaces
{
    public interface IHighlightRepository
    {
        Task<List<Highlight>> GetHighlights();
        Task<bool> AddHighlight(DataForAddingHighlightDto dataForAddingHighlight);
        Task<bool> DeleteHighlight(DataForDeletingHighlightDto dataForDeletingHighlight);
    }
}