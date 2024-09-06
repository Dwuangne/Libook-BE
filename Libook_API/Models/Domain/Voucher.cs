using System;
using System.Collections.Generic;

namespace Libook_API.Models.Domain;

public partial class Voucher
{
    public Voucher()
    {
        VoucherId = Guid.NewGuid();
    }
    public Guid VoucherId { get; set; }

    public string Title { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public double Discount { get; set; }

    public int Remain { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<VoucherActived> VoucherActiveds { get; set; } = new List<VoucherActived>();
}
