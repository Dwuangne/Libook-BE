using System;
using System.Collections.Generic;

namespace Libook_API.Models.Domain;

public partial class OrderStatus
{
    public Guid Id { get; set; }

    public string Status { get; set; } = null!;

    public DateTime DateCreate { get; set; }

    public Guid OrderId { get; set; }

    public virtual Order Order { get; set; } = null!;
}
