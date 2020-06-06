using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Onlinewallet.Core.Database;
using Microsoft.EntityFrameworkCore;
using OnlineWallet.Infrastructure.DatabaseContext;
using OnlineWallet.Infrastructure.IntegrationServices;
using Onlinewallet.Core.RepositoryInterfaces;
using OnlineWallet.Infrastructure.Repositories;
using Onlinewallet.Core.ServiceInterfaces;
using OnlineWallet.Infrastructure.Services;

namespace OnlineWalletApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<WalletDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<IPoolCurrenciesService, PoolCurrenciesService>();

            //Inject repositories
            services.AddTransient<ICurrencyRepository, CurrencyRepository>();
            services.AddTransient<IUsersRepository, UsersRepository>();
            services.AddTransient<IWalletRepository, WalletRepository>();

            //Inject services
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IWalletService, WalletService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
