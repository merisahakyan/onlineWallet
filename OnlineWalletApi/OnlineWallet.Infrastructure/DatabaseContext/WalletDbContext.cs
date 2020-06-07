using Microsoft.EntityFrameworkCore;
using Onlinewallet.Core.Database.Configurations;
using Onlinewallet.Core.Database.Entities;
using OnlineWallet.Infrastructure.IntegrationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineWallet.Infrastructure.DatabaseContext
{
    public class WalletDbContext : DbContext
    {
        readonly IPoolCurrenciesService _poolerService;

        public WalletDbContext()
        {

        }
        public WalletDbContext(IPoolCurrenciesService poolerService, DbContextOptions<WalletDbContext> options)
           : base(options)
        {
            _poolerService = poolerService;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Currency> Currenies { get; set; }
        public DbSet<Wallet> Wallets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new CurrencyConfiguration());

            var dataFromPooler = _poolerService.PoolCurrenciesAsync().GetAwaiter().GetResult();
            var i = 1;
            var currencies = dataFromPooler.Select(c => new Currency
            {
                Id = i++,
                Name = c.Currency
            });
            modelBuilder.Entity<Currency>().HasData(currencies);
            base.OnModelCreating(modelBuilder);
        }
    }
}
