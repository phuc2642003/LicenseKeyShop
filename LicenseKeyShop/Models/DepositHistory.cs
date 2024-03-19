using System;
using System.Collections.Generic;

namespace LicenseKeyShop.Models;

public partial class DepositHistory
{
    public int DepositId { get; set; }

    public string UserUsername { get; set; } = null!;

    public double Amount { get; set; }

    public DateTime ActionDate { get; set; }

    public string ActionBy { get; set; } = null!;

    public virtual User UserUsernameNavigation { get; set; } = null!;
}
