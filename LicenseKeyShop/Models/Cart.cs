using System;
using System.Collections.Generic;
using System.Text;

namespace LicenseKeyShop.Models;

public partial class Cart
{
    public string UserUsername { get; set; } = null!;

    public double Total { get; set; }

    public string GetFormattedTotal()
    {
        string amount = this.Total.ToString();
        StringBuilder formattedAmount = new StringBuilder();
        int endPoint = amount.IndexOf(".");
        if (endPoint < 0)
        {
            endPoint = amount.Length;
        }
        int count = 0;
        for (int i = endPoint - 1; i >= 0; i--)
        {
            formattedAmount.Insert(0, amount[i]);
            count++;

            if (count % 3 == 0 && i > 0)
            {
                formattedAmount.Insert(0, ",");
            }
        }
        if (endPoint == amount.Length)
        {
            return formattedAmount.ToString();
        }
        else
        {
            return formattedAmount.ToString() + amount.Substring(endPoint);
        }
    }
    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual User UserUsernameNavigation { get; set; } = null!;
}
