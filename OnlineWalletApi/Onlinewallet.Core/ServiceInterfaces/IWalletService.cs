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
        List<Currency> GetAvailableCurrencies();
        void CreateNewWallet(WalletViewModel wallet);
        void AddMoneyToWallet(int walletId, decimal value);
        void RemoveMoneyFromWallet(int walletId, decimal value);
        Task DoExchangeAsync(int fromWalletId, int toWalletId, decimal value);
    }
}
