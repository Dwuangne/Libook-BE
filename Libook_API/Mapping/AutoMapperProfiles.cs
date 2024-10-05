using AutoMapper;
using Libook_API.Models.Domain;
using Libook_API.Models.DTO;

namespace Libook_API.Mapping
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AuthorDTO, Author>().ReverseMap();
            CreateMap<Author, AuthorResponseDTO>().ReverseMap();

            CreateMap<CategoryDTO, Category>().ReverseMap();
            CreateMap<Category, CategoryResponseDTO>().ReverseMap();

            CreateMap<SupplierDTO, Supplier>().ReverseMap();
            CreateMap<Supplier, SupplierResponseDTO>().ReverseMap();

            CreateMap<BookDTO, Book>().ReverseMap();
            CreateMap<BookUpdateDTO, Book>().ReverseMap();
            CreateMap<Book, BookResponseDTO>().ReverseMap();

            CreateMap<BookImageDTO, BookImage>().ReverseMap();
            CreateMap<BookImageWithDTO, BookImage>().ReverseMap();
            CreateMap<BookImage, BookImageResponseDTO>().ReverseMap();
            CreateMap<BookImage, BookImageResponseWithDTO>().ReverseMap();

            CreateMap<CommentDTO, Comment>().ReverseMap();
            CreateMap<Comment, CommentResponsesDTO>().ReverseMap();

            CreateMap<CommentImageDTO, CommentImage>().ReverseMap();
            //CreateMap<CommentImageUpdateDTO, CommentImage>().ReverseMap();
            CreateMap<CommentImage, CommentImageResponseDTO>().ReverseMap();
            CreateMap<CommentImage, CommentImageResponseWithDTO>().ReverseMap();

            CreateMap<OrderStatusDTO, OrderStatus>().ReverseMap();
            CreateMap<OrderStatusWithDTO, OrderStatus>().ReverseMap();
            CreateMap<OrderStatus, OrderStatusResponseDTO>().ReverseMap();
            CreateMap<OrderStatus, OrderStatusResponseWithDTO>().ReverseMap();

            CreateMap<OrderInfoDTO, OrderInfo>().ReverseMap();
            //CreateMap<OrderInfoUpdateDTO, OrderInfo>().ReverseMap();
            CreateMap<OrderInfo, OrderInfoResponseDTO>().ReverseMap();

            CreateMap<OrderDetailDTO, OrderDetail>().ReverseMap();
            CreateMap<OrderDetailWithDTO, OrderDetail>().ReverseMap();
            CreateMap<OrderDetail, OrderDetailResponseDTO>().ReverseMap();
            CreateMap<OrderDetail, OrderDetailResponseWithDTO>().ReverseMap();

            CreateMap<OrderDTO, Order>().ReverseMap();
            CreateMap<Order, OrderResponseDTO>().ReverseMap();

            CreateMap<PaymentOrderDTO, PaymentOrder>().ReverseMap();
            CreateMap<PaymentOrder, PaymentOrderResponseDTO>().ReverseMap();

            CreateMap<VoucherDTO, Voucher>().ReverseMap();
            CreateMap<Voucher, VoucherResponseDTO>().ReverseMap();

            CreateMap<VoucherActivedDTO, VoucherActived>().ReverseMap();
            CreateMap<VoucherActived, VoucherActivedResponseDTO>().ReverseMap();

            CreateMap<ProvinceDTO, Province>().ReverseMap();
            CreateMap<Province, ProvinceResponseDTO>().ReverseMap();

            CreateMap<DistrictDTO, District>().ReverseMap();
            CreateMap<District, DistrictResponseDTO>().ReverseMap();

            CreateMap<WardDTO, Ward>().ReverseMap();
            CreateMap<Ward, WardResponseDTO>().ReverseMap();

            CreateMap<ConversationDTO, Conversation>().ReverseMap();
            CreateMap<Conversation, ConversationResponseDTO>().ReverseMap();

            CreateMap<ParticipantDTO, Participant>().ReverseMap();
            CreateMap<ParticipantWithDTO, Participant>().ReverseMap();
            CreateMap<Participant, ParticipantResponseDTO>().ReverseMap();
            CreateMap<Participant, ParticipantResponseWithDTO>().ReverseMap();

            CreateMap<MessageDTO, Message>().ReverseMap();
            CreateMap<MessageWithDTO, Message>().ReverseMap();
            CreateMap<Message, MessageResponseDTO>().ReverseMap();
            CreateMap<Message, MessageResponseWithDTO>().ReverseMap();
        }
    }
}
