using AutoMapper;
using Libook_API.Models.Domain;
using Libook_API.Models.DTO;
using Libook_API.Repositories.AuthorRepo;
using Libook_API.Repositories.OrderDetailRepo;
using Libook_API.Repositories.OrderInfoRepo;
using Libook_API.Repositories.OrderRepo;
using Libook_API.Repositories.OrderStatusRepo;
using Libook_API.Repositories.VoucherActivedRepo;
using Libook_API.Repositories.VoucherRepo;
using Libook_API.Service.BookService;
using Libook_API.Service.OrderInfoService;
using Libook_API.Service.VoucherActivedService;
using Libook_API.Service.VoucherService;

namespace Libook_API.Service.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;
        private readonly IOrderInfoService orderInfoService;
        private readonly IBookService bookService;
        private readonly IVoucherService voucherService;
        private readonly IVoucherActivedService voucherActivedService;
        private readonly IMapper mapper;

        public OrderService(IOrderRepository orderRepository, IOrderInfoService orderInfoService,IBookService bookService, IVoucherService voucherService, IVoucherActivedService voucherActivedService, IMapper mapper)
        {
            this.orderRepository = orderRepository;
            this.orderInfoService = orderInfoService;
            this.bookService = bookService;
            this.voucherService = voucherService;
            this.voucherActivedService = voucherActivedService;
            this.mapper = mapper;
        }
        public async Task<OrderResponseDTO> AddOrderAsync(OrderDTO orderDTO)
        {
            // Map or Convert DTO to Domain Model
            var orderDomain = mapper.Map<Order>(orderDTO);

            //Update properties 
            orderDomain.Address = await orderInfoService.GetAddressAsync(orderDomain.OrderInfoId);
            orderDomain.DateCreate = DateTime.Now;
            
            foreach (var orderDetail in orderDomain.OrderDetails)
            {
                orderDetail.OrderId = orderDomain.OrderId;
            }
            foreach (var orderStatus in orderDomain.OrderStatuses)
            {
                orderStatus.DateCreate = DateTime.Now;
                orderStatus.OrderId = orderDomain.OrderId;
            }

            // Use Domain Model to create
            orderDomain = await orderRepository.InsertAsync(orderDomain);

            //Update remain for book, voucher
            foreach (var orderDetail in orderDomain.OrderDetails)
            {
                var exisitingBook = await bookService.GetBookByIdAsync(orderDetail.BookId);

                //Update remain
                var newRemain = (exisitingBook.Remain - orderDetail.Quantity) < 0 ? 0 : (exisitingBook.Remain - orderDetail.Quantity);
                
                await bookService.UpdateBookRemainAsync(exisitingBook.Id, newRemain);
            }
            if(orderDTO.VoucherId != null)
            {
                var exisitingVoucher = await voucherService.GetVoucherByIdAsync((Guid)orderDTO.VoucherId);
                await voucherService.UpdateVoucherRemainAsync(exisitingVoucher.VoucherId, exisitingVoucher.Remain - 1);
                await voucherActivedService.AddVoucherActivedAsync(new VoucherActivedDTO 
                                        { UserId= orderDTO.UserId, VoucherId = (Guid)orderDTO.VoucherId });
            }

            return mapper.Map<OrderResponseDTO>(orderDomain);
        }

        public async Task<IEnumerable<OrderResponseDTO?>> GetAllOrderAsync()
        {
            var orderDomains = await orderRepository.GetAllAsync();

            // Giả sử authorDomains là danh sách các đối tượng AuthorDomain
            var orderResponse = mapper.Map<List<OrderResponseDTO>>(orderDomains);

            return orderResponse;
        }

        public async Task<OrderResponseDTO?> GetOrderByIdAsync(Guid orderId)
        {
            var orderDomain = await orderRepository.GetByIdAsync(orderId);
            return mapper.Map<OrderResponseDTO>(orderDomain);
        }

        public async Task<IEnumerable<OrderResponseDTO?>> GetOrderByUserIdAsync(Guid userId)
        {
            var orderDomains = await orderRepository.GetByUserIdAsync(userId);

            // Giả sử authorDomains là danh sách các đối tượng AuthorDomain
            var orderResponse = mapper.Map<List<OrderResponseDTO>>(orderDomains);

            return orderResponse;
        }
    }
}
