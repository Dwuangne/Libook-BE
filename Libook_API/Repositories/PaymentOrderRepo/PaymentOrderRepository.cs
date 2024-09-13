using Libook_API.Data;
using Libook_API.Models.Domain;
using Libook_API.Repositories.ParticipantRepo;

namespace Libook_API.Repositories.PaymentOrderRepo
{
    public class PaymentOrderRepository : GenericRepository<PaymentOrder>, IPaymentOrderRepository
    {
        public PaymentOrderRepository(LibookDbContext context) : base(context)
        {
        }
    }
}
