using Libook_API.Models.DTO;
using Libook_API.Models.Response;
using Libook_API.Service.BookService;
using Libook_API.Service.CheckOutService;
using Libook_API.Service.OrderService;
using Libook_API.Service.OrderStatusService;
using Libook_API.Service.PaymentOrderService;
using Microsoft.AspNetCore.Authorization;
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
        private readonly ICheckOutService checkOutService;

        public CheckOutController(ICheckOutService checkOutService)
        {
            this.checkOutService = checkOutService;
        }

        [HttpPost]
        [Route("create")]
        [Authorize(Roles = "customer")]
        public async Task<IActionResult> CreatePaymentLink(CheckOutDTO checkOutDTO)
        {
            try
            {
                CreatePaymentResult createPayment = await checkOutService.CreatePaymentLink(checkOutDTO);
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
        [Authorize(Roles = "customer")]
        public async Task<IActionResult> ReturnPayment(string code, string id, bool cancel, string status, long orderCode)
        {
            try
            {
                if (status == "PAID" && !cancel)
                {
                    var orderStatusResponse = await checkOutService.PaymentSuccess(orderCode);
                    if (orderStatusResponse == null)
                    {
                        throw new Exception("Can't update status order!");
                    }
                    return Redirect("/Template/PaymentTemplate/PaymentSuccess.html");
                }
                else
                {
                    return Redirect("/Template/PaymentTemplate/PaymentFail.html");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Redirect("/Template/PaymentTemplate/PaymentError.html");
            }
        }
    }
}
