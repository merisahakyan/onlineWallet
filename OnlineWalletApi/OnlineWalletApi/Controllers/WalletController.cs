using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Onlinewallet.Core.Models.ViewModels;
using Onlinewallet.Core.ServiceInterfaces;

namespace OnlineWalletApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        IWalletService _walletService;
        public WalletController(IWalletService walletService)
        {
            _walletService = walletService;
        }

        /// <summary>
        /// Get user's wallet
        /// </summary>
        /// <returns></returns>
        [HttpGet("{userId}")]
        public IActionResult Get(int userId)
        {
            try
            {
                var currencies = _walletService.GetUserWallet(userId);
                return Ok(currencies);
            }
            //TODO :  improve application to have global exception handler
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// create new wallet for user
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody] WalletViewModel wallet)
        {
            try
            {
                _walletService.CreateNewWallet(wallet);
                return Ok();
            }
            //TODO :  improve application to have global exception handler
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// add money to wallet
        /// </summary>
        /// <returns></returns>
        [HttpPut("add/{id}")]
        public IActionResult AddMoney(int id, [FromBody] decimal value)
        {
            try
            {
                _walletService.AddMoneyToWallet(id, value);
                return Ok();
            }
            //TODO :  improve application to have global exception handler
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// remove money to wallet
        /// </summary>
        /// <returns></returns>
        [HttpPut("remove/{id}")]
        public IActionResult RemoveMoney(int id, [FromBody] decimal value)
        {
            try
            {
                _walletService.RemoveMoneyFromWallet(id, value);
                return Ok();
            }
            //TODO :  improve application to have global exception handler
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// exchange money
        /// </summary>
        /// <returns></returns>
        [HttpPut("exchange/{fromWalletId}/{toWalletId}")]
        public async Task<IActionResult> DoExchangeAsync(int fromWalletId, int toWalletId, [FromBody] decimal value)
        {
            try
            {
                await _walletService.DoExchangeAsync(fromWalletId, toWalletId, value);
                return Ok();
            }
            //TODO :  improve application to have global exception handler
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}