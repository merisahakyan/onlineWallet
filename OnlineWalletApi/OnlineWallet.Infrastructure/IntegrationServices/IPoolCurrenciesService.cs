using Onlinewallet.Core.Models.SeedModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineWallet.Infrastructure.IntegrationServices
{
    public interface IPoolCurrenciesService
    {
        Task<List<CurrencySeedModel>> PoolCurrenciesAsync();
    }
}
