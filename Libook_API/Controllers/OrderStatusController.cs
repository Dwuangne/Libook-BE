using Libook_API.Models.DTO;
using Libook_API.Models.Response;
using Libook_API.Service.OrderInfoService;
using Libook_API.Service.OrderStatusService;
using Microsoft.AspNetCore.Mvc;

namespace Libook_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderStatusController : Controller
    {
        private readonly IOrderStatusService orderStatusService;

        public OrderStatusController(IOrderStatusService orderStatusService)
        {
            this.orderStatusService = orderStatusService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orderStatusResponses = await orderStatusService.GetAllOrderStatusAsync();

            var response = new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Get all order status successfully!",
                data = orderStatusResponses
            };
            return Ok(response);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var orderStatusResponse = await orderStatusService.GetOrderStatusByIdAsync(id);

            if (orderStatusResponse == null)
            {
                return NotFound();
            }

            var response = new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Get order status by id successfully!",
                data = orderStatusResponse
            };
            return Ok(response);
        }

        [HttpGet]
        [Route("order/{orderId:Guid}")]
        public async Task<IActionResult> GetByUserId([FromRoute] Guid orderId)
        {
            var orderStatusResponses = await orderStatusService.GetOrderStatusByOrderIdAsync(orderId);

            var response = new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Get order status by order id successfully!",
                data = orderStatusResponses

            };
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderStatusDTO orderStatusDTO)
        {
            var orderStatusResponse = await orderStatusService.AddOrderStatusAsync(orderStatusDTO);

            var response = new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Create order status successfully!",
                data = orderStatusResponse
            };
            return Ok(response);
        }
    }
}
