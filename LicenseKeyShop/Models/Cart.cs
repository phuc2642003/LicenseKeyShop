using System;
using System.Collections.Generic;

namespace LicenseKeyShop.Models;

public partial class Cart
{
    public string UserUsername { get; set; } = null!;

    public double Total { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual User UserUsernameNavigation { get; set; } = null!;
}
