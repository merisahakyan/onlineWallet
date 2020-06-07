using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Onlinewallet.Core.ServiceInterfaces;

namespace OnlineWalletApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        IWalletService _walletService;
        public CurrencyController(IWalletService walletService)
        {
            _walletService = walletService;
        }

        /// <summary>
        /// Get available currencies for creating new wallet for user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var currencies = _walletService.GetAvailableCurrenciesAsync();
                return Ok(currencies);
            }
            //TODO :  improve application to have global exception handler
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}