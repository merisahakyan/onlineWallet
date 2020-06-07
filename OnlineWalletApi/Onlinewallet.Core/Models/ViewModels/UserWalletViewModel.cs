using System;
using System.Collections.Generic;
using System.Text;

namespace Onlinewallet.Core.Models.ViewModels
{
    public class UserWalletViewModel
    {
        public int WalletId { get; set; }
        public string Currency { get; set; }
        public decimal Value { get; set; }
    }
}
