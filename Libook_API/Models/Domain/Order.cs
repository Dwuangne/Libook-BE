using System;
using System.Collections.Generic;

namespace Libook_API.Models.Domain;

public partial class Order
{
    public Guid OrderId { get; set; }

    public DateTime DateCreate { get; set; }

    public double Amount { get; set; }

    public string Address {  get; set; }

    public Guid UserId { get; set; }

    public Guid? VoucherId { get; set; }

    public Guid OrderInfoId { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<OrderStatus> OrderStatuses { get; set; } = new List<OrderStatus>();

    public virtual Voucher? Voucher { get; set; }

    public virtual OrderInfo OrderInfo { get; set; }
}
