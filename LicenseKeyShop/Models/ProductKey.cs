using System;
using System.Collections.Generic;

namespace LicenseKeyShop.Models;

public partial class ProductKey
{
    public int KeyId { get; set; }

    public int ProductProductId { get; set; }

    public string ProductKey1 { get; set; } = null!;

    public string? ExpirationDate { get; set; }

    public bool IsExpired { get; set; }

    public virtual Product ProductProduct { get; set; } = null!;
}
