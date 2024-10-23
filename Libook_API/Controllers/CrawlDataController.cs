using Libook_API.Models.DTO;
using Libook_API.Models.Response;
using Libook_API.Service.CrawlDataService;
using Microsoft.AspNetCore.Mvc;

namespace Libook_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrawlDataController : Controller
    {

        public readonly ICrawlDataService crawlDataService;

        public CrawlDataController(ICrawlDataService crawlDataService)
        {
            this.crawlDataService = crawlDataService;
        }

        [HttpGet]
        [Route("/vnexpress")]
        public async Task<IActionResult> GetArticleFromVnExpress()
        {
            var url = "https://vnexpress.net/giai-tri/sach/diem-sach";

            var articles = await crawlDataService.GetArticleFromVnExpressAsync(url);
            var response = new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Get article from vnexpress successfully!",
                data = articles
            };

            return Ok(response);
        }
    }
}
