using Libook_API.Models.DTO;
using Libook_API.Models.Response;
using Libook_API.Service.OrderDetailService;
using Libook_API.Service.OrderService;
using Microsoft.AspNetCore.Mvc;

namespace Libook_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orderResponses = await orderService.GetAllOrderAsync();

            var response = new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Get all order successfully!",
                data = orderResponses
            };
            return Ok(response);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var orderResponse = await orderService.GetOrderByIdAsync(id);

            if (orderResponse == null)
            {
                return NotFound();
            }

            var response = new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Get order by id successfully!",
                data = orderResponse
            };
            return Ok(response);
        }

        [HttpGet]
        [Route("user/{userId:Guid}")]
        public async Task<IActionResult> GetByUserId([FromRoute] Guid userId)
        {
            var orderResponses = await orderService.GetOrderByUserIdAsync(userId);

            var response = new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Get order by user id successfully!",
                data = orderResponses
            };
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderDTO orderDTO)
        {
            var orderResponses = await orderService.AddOrderAsync(orderDTO);

            var response = new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Create order successfully!",
                data = orderResponses
            };
            return Ok(response);
        }
    }
}
