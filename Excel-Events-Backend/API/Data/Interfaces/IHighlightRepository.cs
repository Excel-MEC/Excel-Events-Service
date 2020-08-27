using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dtos.Highlight;
using API.Models;

namespace API.Data.Interfaces
{
    public interface IHighlightRepository
    {
        Task<List<Highlight>> GetHighlights();
        Task<Highlight> AddHighlight(DataForAddingHighlightDto dataForAddingHighlight);
        Task<Highlight> DeleteHighlight(DataForDeletingHighlightDto dataForDeletingHighlight);
    }
}