using Libook_API.Models.Response;
using Libook_API.Service.DistrictService;
using Libook_API.Service.ProvinceService;
using Microsoft.AspNetCore.Mvc;

namespace Libook_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistrictController : Controller
    {
        private readonly IDistrictService districtService;

        public DistrictController(IDistrictService districtService)
        {
            this.districtService = districtService;
        }

        [HttpGet]
        [Route("province/{provinceId:decimal}")]
        public async Task<IActionResult> GetAllDistrictByProvinceId([FromRoute] string provinceId)
        {
            var districtResponses = await districtService.GetDistrictByProvinceIdAsync(provinceId);

            var response = new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Get districts by province id successfully!",
                data = districtResponses
            };
            return Ok(response);
        }

        [HttpGet]
        [Route("{id:decimal}")]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            var districtResponses = await districtService.GetDistrictByIdAsync(id);

            if (districtResponses == null)
            {
                return NotFound();
            }

            var response = new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Get district by id successfully!",
                data = districtResponses
            };
            return Ok(response);
        }
    }
}
