using Libook_API.Models.Response;
using Libook_API.Service.AuthorService;
using Libook_API.Service.ProvinceService;
using Microsoft.AspNetCore.Mvc;

namespace Libook_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvinceController : Controller
    {
        private readonly IProvinceService provinceService;

        public ProvinceController(IProvinceService provinceService)
        {
            this.provinceService = provinceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var provinceResponses = await provinceService.GetAllProvinceAsync();

            var response = new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Get all province successfully!",
                data = provinceResponses
            };
            return Ok(response);
        }

        [HttpGet]
        [Route("{id:decimal}")]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            var provinceResponse = await provinceService.GetProvinceByIdAsync(id);

            if (provinceResponse == null)
            {
                return NotFound();
            }

            var response = new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Get province by id successfully!",
                data = provinceResponse
            };
            return Ok(response);
        }
    }
}
