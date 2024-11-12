using Libook_API.Models.DTO;

namespace Libook_API.Service.OpenAIService
{
    public interface IAzureOpenAIService
    {
        Task<IEnumerable<BookResponseDTO?>> GetBookRecommendationAsync(BookResponseDTO bookOrigin, List<BookResponseDTO> potentialBooks);
    }
}
