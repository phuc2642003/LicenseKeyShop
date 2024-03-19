using System;
using System.Collections.Generic;

namespace LicenseKeyShop.Models;

public partial class OrderHistory
{
    public int OrderId { get; set; }

    public string UserUsername { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual User UserUsernameNavigation { get; set; } = null!;
}
