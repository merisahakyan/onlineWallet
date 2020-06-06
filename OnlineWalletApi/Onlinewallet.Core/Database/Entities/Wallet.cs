using System;
using System.Collections.Generic;
using System.Text;

namespace Onlinewallet.Core.Database.Entities
{
    public class Wallet : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }
        public decimal Value { get; set; }
    }
}
