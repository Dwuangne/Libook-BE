using AutoMapper.Internal.Mappers;

namespace Libook_API.Models.Domain
{
    public class OrderInfo
    {
        public OrderInfo()
        {
            Id = Guid.NewGuid();    
        }
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Phone {  get; set; }

        public string ProvinceId { get; set; }

        public string DistrictId { get; set; }

        public string WardId { get; set; }

        public string Address { get; set; }

        public Guid UserId { get; set; }

        //Navigation properties

        public Province Province { get; set; }

        public District District { get; set; }

        public Ward Ward { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    }
}
