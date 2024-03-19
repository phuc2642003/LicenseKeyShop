using System;
using System.Collections.Generic;

namespace LicenseKeyShop.Models;

public partial class User
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int RoleRoleId { get; set; }

    public bool IsVerified { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<BalanceHistory> BalanceHistories { get; set; } = new List<BalanceHistory>();

    public virtual Cart? Cart { get; set; }

    public virtual ICollection<DepositHistory> DepositHistories { get; set; } = new List<DepositHistory>();

    public virtual ICollection<OrderHistory> OrderHistories { get; set; } = new List<OrderHistory>();

    public virtual Role RoleRole { get; set; } = null!;

    public virtual UserBalance? UserBalance { get; set; }
}
