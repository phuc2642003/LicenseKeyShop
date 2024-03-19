using System;
using System.Collections.Generic;

namespace LicenseKeyShop.Models;

public partial class UserBalance
{
    public string UserUsername { get; set; } = null!;

    public double Amount { get; set; }

    public virtual User UserUsernameNavigation { get; set; } = null!;
}
