using Libook_API.Models.DTO;
using Libook_API.Models.Response;
using Libook_API.Service.AuthorService;
using Libook_API.Service.OrderInfoService;
using Microsoft.AspNetCore.Mvc;

namespace Libook_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderInfoController : Controller
    {
        private readonly IOrderInfoService orderInfoService;

        public OrderInfoController(IOrderInfoService orderInfoService)
        {
            this.orderInfoService = orderInfoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orderInfoResponses = await orderInfoService.GetAllOrderInfoAsync();

            var response = new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Get all order information successfully!",
                data = orderInfoResponses
            };
            return Ok(response);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var orderInfoResponse = await orderInfoService.GetOrderInfoByIdAsync(id);

            if (orderInfoResponse == null)
            {
                return NotFound();
            }

            var response = new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Get order information by id successfully!",
                data = orderInfoResponse
            };
            return Ok(response);
        }

        [HttpGet]
        [Route("user/{userId:Guid}")]
        public async Task<IActionResult> GetByUserId([FromRoute] Guid userId)
        {
            var orderInfoResponse = await orderInfoService.GetOrderInfoByUserIdAsync(userId);

            var response = new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Get order information by user id successfully!",
                data = orderInfoResponse
            };
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderInfoDTO orderInfoDTO)
        {
            var orderInfoResponse = await orderInfoService.AddOrderInfoAsync(orderInfoDTO);

            var response = new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Create order infomation successfully!",
                data = orderInfoResponse
            };
            return Ok(response);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] OrderInfoUpdateDTO orderInfoUpdateDTO)
        {
            var orderInfoResponse = await orderInfoService.UpdateOrderInfoAsync(id, orderInfoUpdateDTO);
            if (orderInfoResponse == null)
            {
                return NotFound();
            }
            var response = new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Update order information successfully!",
                data = orderInfoResponse
            };
            return Ok(response);
        }
    }
}
