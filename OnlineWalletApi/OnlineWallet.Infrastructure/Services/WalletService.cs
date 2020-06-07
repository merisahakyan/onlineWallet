using Microsoft.EntityFrameworkCore;
using Onlinewallet.Core.Database.Entities;
using Onlinewallet.Core.Models.ViewModels;
using Onlinewallet.Core.RepositoryInterfaces;
using Onlinewallet.Core.ServiceInterfaces;
using OnlineWallet.Infrastructure.IntegrationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineWallet.Infrastructure.Services
{
    public class WalletService : IWalletService
    {
        readonly ICurrencyRepository _currencyRepository;
        readonly IWalletRepository _walletRepository;
        readonly IPoolCurrenciesService _poolerService;

        public WalletService(ICurrencyRepository currencyRepository,
            IWalletRepository walletRepository,
            IPoolCurrenciesService poolerService)
        {
            _currencyRepository = currencyRepository;
            _walletRepository = walletRepository;
            _poolerService = poolerService;
        }

        /// <summary>
        /// Add money to existing wallet by id
        /// </summary>
        /// <param name="walletId"></param>
        /// <param name="value"></param>
        public async Task AddMoneyToWalletAsync(int walletId, decimal value)
        {
            var wallet = _walletRepository.GetSingle(walletId);
            if (wallet == null)
                //TODO : create custom exceptions for global exception handling
                throw new Exception($"Not found wallet with id : {walletId}");

            wallet.Value += value;
            _walletRepository.Update(wallet);
            await _walletRepository.SaveChangesAsync();
        }

        /// <summary>
        /// Create new wallet for user
        /// </summary>
        /// <param name="wallet">Model contains user id and currency id for wallet</param>
        public async Task CreateNewWalletAsync(WalletViewModel wallet)
        {
            _walletRepository.Add(new Wallet
            {
                CurrencyId = wallet.CurrencyId,
                UserId = wallet.UserId,
                Value = 0
            });
            await _walletRepository.SaveChangesAsync();
        }

        /// <summary>
        /// Exchange money between wallets
        /// </summary>
        /// <param name="fromWalletId"></param>
        /// <param name="toWalletId"></param>
        /// <param name="value"></param>
        public async Task DoExchangeAsync(int fromWalletId, int toWalletId, decimal value)
        {
            var fromWallet = _walletRepository.GetSingle(fromWalletId);
            if (fromWallet == null)
                //TODO : create custom exceptions for global exception handling
                throw new Exception($"Not found wallet with id : {fromWalletId}");

            var toWallet = _walletRepository.GetSingle(toWalletId);
            if (toWallet == null)
                //TODO : create custom exceptions for global exception handling
                throw new Exception($"Not found wallet with id : {toWalletId}");

            if (fromWallet.Value < value)
                //TODO : create custom exceptions for global exception handling
                throw new Exception($"Not enough money on wallet with id : {toWalletId}. Current value : {fromWallet.Value}");

            var currencyCurrentRates = await _poolerService.PoolCurrenciesAsync();

            var fromCurrency = _currencyRepository.GetSingle(fromWallet.CurrencyId);
            var toCurrency = _currencyRepository.GetSingle(toWallet.CurrencyId);

            var fromRate = currencyCurrentRates.FirstOrDefault(c => c.Currency == fromCurrency.Name).Rate;
            var toRate = currencyCurrentRates.FirstOrDefault(c => c.Currency == toCurrency.Name).Rate;

            var newValue = value / fromRate * toRate;

            fromWallet.Value -= value;
            toWallet.Value += newValue;

            _walletRepository.Update(fromWallet);
            _walletRepository.Update(toWallet);
            await _walletRepository.SaveChangesAsync();
        }

        /// <summary>
        /// Get available currencies for creating wallet
        /// </summary>
        /// <returns></returns>
        public async Task<List<Currency>> GetAvailableCurrenciesAsync()
        {
            return await _currencyRepository.GetAll().ToListAsync();
        }

        /// <summary>
        /// Returns user's wallet with currencies and amount of money
        /// </summary>
        /// <param name="userId">the user id</param>
        /// <returns></returns>
        public async Task<List<UserWalletViewModel>> GetUserWalletAsync(int userId)
        {
            var userWallets = _walletRepository.GetAll().Where(w => w.UserId == userId);
            return await userWallets.Select(u => new UserWalletViewModel
            {
                Currency = u.Currency.Name,
                Value = u.Value
            }).ToListAsync();
        }

        /// <summary>
        /// remove money from wallet
        /// </summary>
        /// <param name="walletId">id of the wallet</param>
        /// <param name="value">amout of money</param>
        public async Task RemoveMoneyFromWalletAsync(int walletId, decimal value)
        {
            var wallet = _walletRepository.GetSingle(walletId);
            if (wallet == null)
                //TODO : create custom exceptions for global exception handling
                throw new Exception($"Not found wallet with id : {walletId}");

            if (wallet.Value < value)
                //TODO : create custom exceptions for global exception handling
                throw new Exception($"Not enough money on wallet with id : {walletId}. Current value : {wallet.Value}");

            wallet.Value -= value;
            _walletRepository.Update(wallet);
            await _walletRepository.SaveChangesAsync();
        }
    }
}
