using Libook_API.Models.DTO;
using Libook_API.Models.Response;
using Libook_API.Service.BookService;
using Libook_API.Service.OrderService;
using Libook_API.Service.OrderStatusService;
using Libook_API.Service.PaymentOrderService;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Net.payOS;
using Net.payOS.Types;

namespace Libook_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckOutController : Controller
    {
        private readonly PayOS payOS;
        private readonly IConfiguration configuration;
        private readonly IOrderService orderService;
        private readonly IBookService bookService;
        private readonly IPaymentOrderService paymentOrderService;
        private readonly IOrderStatusService orderStatusService;

        public CheckOutController(PayOS payOS, IConfiguration configuration, IOrderService orderService, IBookService bookService, IPaymentOrderService paymentOrderService, IOrderStatusService orderStatusService)
        {
            this.payOS = payOS;
            this.configuration = configuration;
            this.orderService = orderService;
            this.bookService = bookService;
            this.paymentOrderService = paymentOrderService;
            this.orderStatusService = orderStatusService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreatePaymentLink(CheckOutDTO checkOutDTO)
        {
            try
            {
                // Lấy thông tin đơn hàng từ service
                var orderResponse = await orderService.GetOrderByIdAsync(checkOutDTO.OrderId);

                // Thêm dữ liệu thanh toán vào PaymentOrder
                var paymentOrderResponse = await paymentOrderService.AddPaymentOrderAsync(new PaymentOrderDTO( (int)orderResponse.Amount, orderResponse.OrderId));

                // Tạo mã đơn hàng duy nhất
                long orderCode = paymentOrderResponse.PaymentID;

                // Đọc URL hủy và URL hoàn tất từ cấu hình
                var cancelUrl = configuration["CheckOutPayOs:Environment:CANCEL_URL"];
                var returnUrl = configuration["CheckOutPayOs:Environment:RETURN_URL"];

                if (orderResponse == null || orderResponse.PaymentMethod == "COD" || orderResponse.OrderStatuses.Any(s => s.Status == "PAID"))
                {
                    return BadRequest(new ResponseObject
                    {
                        status = System.Net.HttpStatusCode.BadRequest,
                        message = "Order is invalid!"
                    });
                }

                // Tạo danh sách item từ chi tiết đơn hàng
                List<ItemData> items = orderResponse.OrderDetails.Select(orderDetail =>
                {
                    var bookName = bookService.GetBookByIdAsync(orderDetail.BookId).Result?.Name;
                    return new ItemData(bookName, orderDetail.Quantity, (int)orderDetail.UnitPrice);
                }).ToList();

                // Tạo dữ liệu thanh toán
                PaymentData paymentData = new PaymentData(orderCode, (int)orderResponse.Amount, checkOutDTO.Description, items, cancelUrl, returnUrl);

                // Tạo đường dẫn thanh toán
                CreatePaymentResult createPayment = await payOS.createPaymentLink(paymentData);

                return Ok(new ResponseObject
                {
                    status = System.Net.HttpStatusCode.OK,
                    message = "Create payment link successfully!",
                    data = createPayment
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Ok(new ResponseObject
                {
                    status = System.Net.HttpStatusCode.InternalServerError,
                    message = "Create payment link failed!",
                });
            }
        }

        [HttpGet]
        [Route("return_url")]
        public async Task<IActionResult> ReturnPayment(string code, string id, bool cancel, string status, long orderCode)
        {
            if (status == "PAID" && !cancel)
            {
                var paymentOrderDomain = await paymentOrderService.GetPaymentOrderByIdAsync(orderCode);

                await orderStatusService.AddOrderStatusAsync(new OrderStatusDTO("PAID", paymentOrderDomain.OrderId));

                return Redirect("/payment/success");
            }
            else if (status == "CANCELLED" && cancel)
            {
                return Redirect("/payment/cancelled");
            }
            return Redirect("/payment/error");
        }
    }
}
