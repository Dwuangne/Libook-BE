using Libook_API.Models.Domain;
using Libook_API.Models.DTO;
using Libook_API.Models.Response;
using Libook_API.Service.BookService;
using Libook_API.Service.OrderDetailService;
using Libook_API.Service.OrderService;
using Libook_API.Service.VoucherActivedService;
using Libook_API.Service.VoucherService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Libook_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;
        private readonly IBookService bookService;
        private readonly IVoucherService voucherService;
        private readonly IVoucherActivedService voucherActivedService;

        public OrderController(IOrderService orderService, IBookService bookService, IVoucherService voucherService, IVoucherActivedService voucherActivedService)
        {
            this.orderService = orderService;
            this.bookService = bookService;
            this.voucherService = voucherService;
            this.voucherActivedService = voucherActivedService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Customer")]
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
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Create([FromBody] OrderDTO orderDTO)
        {
            foreach (var orderDetail in orderDTO.OrderDetails)
            {
                var existingBook = await bookService.GetBookByIdAsync(orderDetail.BookId);
                if (existingBook == null || existingBook.Remain <= orderDetail.Quantity || !existingBook.IsActive)
                {
                    return BadRequest("Book does not exist or not enough remain.");
                }
            }
            if(orderDTO.VoucherId != null)
            {
                var existingVoucher = await voucherService.GetVoucherByIdAsync((Guid)orderDTO.VoucherId);
                if(existingVoucher.Remain == 0 || await voucherActivedService.VoucherActivedAsync(orderDTO.UserId, (Guid)orderDTO.VoucherId))
                {
                    return BadRequest("Voucher has expired.");
                }
            }

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
