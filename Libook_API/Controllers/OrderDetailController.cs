using Libook_API.Models.DTO;
using Libook_API.Models.Response;
using Libook_API.Service.OrderDetailService;
using Libook_API.Service.OrderInfoService;
using Microsoft.AspNetCore.Mvc;

namespace Libook_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : Controller
    {
        private readonly IOrderDetailService orderDetailService;

        public OrderDetailController(IOrderDetailService orderDetailService)
        {
            this.orderDetailService = orderDetailService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orderDetailResponses = await orderDetailService.GetAllOrderDetailAsync();

            var response = new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Get all order details successfully!",
                data = orderDetailResponses
            };
            return Ok(response);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var orderDetailResponse = await orderDetailService.GetOrderDetailByIdAsync(id);

            if (orderDetailResponse == null)
            {
                return NotFound();
            }

            var response = new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Get order detail by id successfully!",
                data = orderDetailResponse
            };
            return Ok(response);
        }

        [HttpGet]
        [Route("order/{orderId:Guid}")]
        public async Task<IActionResult> GetByUserId([FromRoute] Guid orderId)
        {
            var orderDetailResponses = await orderDetailService.GetOrderDetailByOrderIdAsync(orderId);

            var response = new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Get order details by order id successfully!",
                data = orderDetailResponses
            };
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderDetailDTO orderDetailDTO)
        {
            var orderDetailResponses = await orderDetailService.AddOrderDetailAsync(orderDetailDTO);

            var response = new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Create order detail successfully!",
                data = orderDetailResponses
            };
            return Ok(response);
        }
    }
}
