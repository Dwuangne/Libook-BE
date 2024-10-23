//using HtmlAgilityPack;
using HtmlAgilityPack;
using Libook_API.Models.DTO;

namespace Libook_API.Service.CrawlDataService
{
    public class CrawlDataService : ICrawlDataService
    {

        public CrawlDataService()
        {
        }

        public async Task<IEnumerable<ArticleDTO?>> GetArticleFromVnExpressAsync(string url)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);  // Tải HTML từ URL

            // Tìm tất cả các nút article có class 'item-news full-thumb'
            HtmlNodeCollection htmlNodes = doc.DocumentNode.SelectNodes("//article[@class='item-news']");

            var articles = new List<ArticleDTO>();

            foreach (HtmlNode node in htmlNodes)
            {
                // Lấy tiêu đề bài viết
                var titleNode = node.SelectSingleNode(".//h3[@class='title-news']/a");
                string title = titleNode?.InnerText.Trim() ?? "No Title";

                // Lấy tóm tắt bài viết (nếu có)
                var summaryNode = node.SelectSingleNode(".//p [@class='description']/a");
                string? summary = summaryNode?.InnerText.Trim();

                // Lấy URL hình ảnh
                var imgNode = node.SelectSingleNode(".//img");
                string urlPicture = imgNode?.GetAttributeValue("src", "") ?? "No Image";

                // Lấy URL bài viết
                string link = titleNode?.GetAttributeValue("href", "") ?? "No Link";

                // Thêm bài viết vào danh sách
                articles.Add(new ArticleDTO
                {
                    Title = title,
                    Summary = summary,
                    UrlPicture = urlPicture,
                    Link = link
                });
            }

            return articles;
        }

    }
}
