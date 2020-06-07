using Onlinewallet.Core.Database.Entities;
using Onlinewallet.Core.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Onlinewallet.Core.ServiceInterfaces
{
    public interface IWalletService
    {
        Task<List<Currency>> GetAvailableCurrenciesAsync();
        Task<List<UserWalletViewModel>> GetUserWalletAsync(int userId);
        Task CreateNewWalletAsync(WalletViewModel wallet);
        Task AddMoneyToWalletAsync(int walletId, decimal value);
        Task RemoveMoneyFromWalletAsync(int walletId, decimal value);
        Task DoExchangeAsync(int fromWalletId, int toWalletId, decimal value);
    }
}
