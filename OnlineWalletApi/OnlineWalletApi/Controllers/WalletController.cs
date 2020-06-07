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
        public async Task<IActionResult> GetAsync(int userId)
        {
            try
            {
                var currencies = await _walletService.GetUserWalletAsync(userId);
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
        public async Task<IActionResult> PostAsync([FromBody] WalletViewModel wallet)
        {
            try
            {
                await _walletService.CreateNewWalletAsync(wallet);
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
        public async Task<IActionResult> AddMoneyAsync(int id, [FromBody] decimal value)
        {
            try
            {
                await _walletService.AddMoneyToWalletAsync(id, value);
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
        public async Task<IActionResult> RemoveMoneyAsync(int id, [FromBody] decimal value)
        {
            try
            {
                await _walletService.RemoveMoneyFromWalletAsync(id, value);
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