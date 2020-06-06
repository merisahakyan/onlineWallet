using Onlinewallet.Core.Database.Entities;
using Onlinewallet.Core.RepositoryInterfaces;
using OnlineWallet.Infrastructure.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineWallet.Infrastructure.Repositories
{
    public class CurrencyRepository : RepositoryBase<Currency>, ICurrencyRepository
    {
        public CurrencyRepository(WalletDbContext context) : base(context)
        {

        }
    }
}
