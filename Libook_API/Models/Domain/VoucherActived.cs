using System;
using System.Collections.Generic;

namespace Libook_API.Models.Domain;

public partial class VoucherActived
{
    public Guid VoucherId { get; set; }

    public Guid UserId { get; set; }

    public virtual Voucher Voucher { get; set; } = null!;
}
