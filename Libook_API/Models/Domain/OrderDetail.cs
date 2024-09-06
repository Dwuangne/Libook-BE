using System;
using System.Collections.Generic;

namespace Libook_API.Models.Domain;

public partial class OrderDetail
{
    public OrderDetail()
    {
        Id = Guid.NewGuid();
    }
    public Guid Id { get; set; }

    public double UnitPrice { get; set; }

    public int Quantity { get; set; }

    public Guid OrderId { get; set; }

    public Guid BookId { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}
