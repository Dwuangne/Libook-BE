using Libook_API.Models.Response;
using Libook_API.Service.DistrictService;
using Libook_API.Service.WardService;
using Microsoft.AspNetCore.Mvc;

namespace Libook_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WardController : Controller
    {
        private readonly IWardService wardService;

        public WardController(IWardService wardService)
        {
            this.wardService = wardService;
        }

        [HttpGet]
        [Route("district/{districtId:decimal}")]
        public async Task<IActionResult> GetAllWardByDistrictId([FromRoute] string districtId)
        {
            var wardResponses = await wardService.GetWardByDistrictIdAsync(districtId);

            var response = new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Get wards by district id successfully!",
                data = wardResponses
            };
            return Ok(response);
        }

        [HttpGet]
        [Route("{id:decimal}")]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            var wardResponse = await wardService.GetWardByIdAsync(id);

            if (wardResponse == null)
            {
                return NotFound();
            }

            var response = new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Get ward by id successfully!",
                data = wardResponse
            };
            return Ok(response);
        }
    }
}
