using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Onlinewallet.Core.RepositoryInterfaces;
using Onlinewallet.Core.ServiceInterfaces;
using OnlineWallet.Infrastructure.DatabaseContext;
using OnlineWallet.Infrastructure.IntegrationServices;
using OnlineWallet.Infrastructure.Repositories;
using OnlineWallet.Infrastructure.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineWallet.Tests
{
    public class Tests
    {
        private IWalletService _walletService;
        private IUsersService _userService;
        private ICurrencyRepository _currencyRepository;
        private int _userId;

        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            //Inject repositories
            services.AddTransient<ICurrencyRepository, CurrencyRepository>();
            services.AddTransient<IUsersRepository, UsersRepository>();
            services.AddTransient<IWalletRepository, WalletRepository>();

            //Inject services
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IWalletService, WalletService>();

            services.AddDbContext<WalletDbContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDbForTesting");
            });
            services.AddTransient<IPoolCurrenciesService, PoolCurrenciesService>();


            var serviceProvider = services.BuildServiceProvider();

            _walletService = serviceProvider.GetService<IWalletService>();
            _currencyRepository = serviceProvider.GetService<ICurrencyRepository>();
            _userService = serviceProvider.GetService<IUsersService>();

            SetupDatabase();
        }

        [Test]
        public async Task DoExchangeAsync_TestAsync()
        {
            //Arrange
            var userWallets = await _walletService.GetUserWalletAsync(_userId);
            var from = userWallets.FirstOrDefault();
            var to = userWallets.LastOrDefault();

            //Act
            await _walletService.DoExchangeAsync(from.WalletId, to.WalletId, from.Value);

            userWallets = await _walletService.GetUserWalletAsync(_userId);
            from = userWallets.FirstOrDefault(w => w.WalletId == from.WalletId);
            //Assert
            Assert.IsTrue(from.Value == 0);
        }

        [Test]
        public async Task DoExchangeAsync_NotEnoughMoney_TestAsync()
        {
            //Arrange
            var userWallets = await _walletService.GetUserWalletAsync(_userId);
            var from = userWallets.FirstOrDefault();
            var to = userWallets.LastOrDefault();

            //Act
            //Assert
            Assert.Throws<Exception>(() =>
                _walletService.DoExchangeAsync(from.WalletId, to.WalletId, from.Value + 100).GetAwaiter().GetResult());
        }

        [Test]
        public async Task DoExchangeAsync_NotFoundWallet_TestAsync()
        {
            //Arrange
            var userWallets = await _walletService.GetUserWalletAsync(_userId);
            var from = userWallets.FirstOrDefault();
            var to = userWallets.LastOrDefault();

            //Act
            //Assert
            Assert.Throws<Exception>(() =>
                _walletService.DoExchangeAsync(0, to.WalletId, from.Value).GetAwaiter().GetResult());
        }

        private void SetupDatabase()
        {
            _currencyRepository.Add(new Onlinewallet.Core.Database.Entities.Currency
            {
                Name = "USD"
            });

            _currencyRepository.Add(new Onlinewallet.Core.Database.Entities.Currency
            {
                Name = "RUB"
            });

            _currencyRepository.SaveChanges();

            var currencies = _currencyRepository.GetAll();
            _userId = _userService.CreateUserAsync(new Onlinewallet.Core.Models.ViewModels.UserViewModel
            {
                Address = "test",
                City = "test",
                Country = "test",
                Email = "test",
                FirstName = "test",
                LastName = "test",
                Mobile = "test",
                Passport = "test",
                PostalCode = "test"
            }).GetAwaiter().GetResult();

            foreach (var currency in currencies)
            {
                _walletService.CreateNewWalletAsync(new Onlinewallet.Core.Models.ViewModels.WalletViewModel
                {
                    CurrencyId = currency.Id,
                    UserId = _userId
                }).GetAwaiter().GetResult();
            }

            var userWallets = _walletService.GetUserWalletAsync(_userId).GetAwaiter().GetResult();

            _walletService.AddMoneyToWalletAsync(userWallets.FirstOrDefault().WalletId, 500).GetAwaiter().GetResult();

        }
    }
}