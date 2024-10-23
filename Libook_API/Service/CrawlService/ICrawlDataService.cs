using Libook_API.Models.DTO;

namespace Libook_API.Service.CrawlDataService
{
    public interface ICrawlDataService
    {
        Task<IEnumerable<ArticleDTO>> GetArticleFromVnExpressAsync(string url);
    }
}
