using Libook_API.Models.DTO;
using Libook_API.Models.Response;
using Libook_API.Service.AuthorService;
using Libook_API.Service.CategoryService;
using Libook_API.Service.VoucherService;
using Microsoft.AspNetCore.Mvc;

namespace Libook_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoucherController : Controller
    {
        private readonly IVoucherService voucherService;

        public VoucherController(IVoucherService voucherService)
        {
            this.voucherService = voucherService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var voucherResponses = await voucherService.GetAllVoucherAsync();

            var response = new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Get all voucher successfully!",
                data = voucherResponses
            };
            return Ok(response);
        }

        [HttpGet]
        [Route("user/{userId:Guid}")]
        public async Task<IActionResult> GetVoucherValid([FromRoute] Guid userId)
        {
            var voucherResponses = await voucherService.GetVoucherValidAsync(userId);

            var response = new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Get voucher valid successfully!",
                data = voucherResponses
            };
            return Ok(response);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var voucherResponse = await voucherService.GetVoucherByIdAsync(id);

            if (voucherResponse == null)
            {
                return NotFound();
            }

            var response = new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Get voucher by id successfully!",
                data = voucherResponse
            };
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] VoucherDTO voucherDTO)
        {
            var voucherResponse = await voucherService.AddVoucherAsync(voucherDTO);

            var response = new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Create voucher successfully!",
                data = voucherDTO
            };
            return Ok(response);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] VoucherRemainUpdateDTO voucherRemainUpdateDTO)
        {
            var voucherReponse = await voucherService.UpdateAuthorAsync(id, voucherRemainUpdateDTO);
            if (voucherReponse == null)
            {
                return NotFound();
            }
            var response = new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Update remain of voucher successfully!",
                data = voucherReponse
            };
            return Ok(response);
        }
    }
}
