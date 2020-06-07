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
    public class UsersController : ControllerBase
    {
        IUsersService _usersService;
        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        /// <summary>
        /// create new user
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] UserViewModel user)
        {
            try
            {
                var id = await _usersService.CreateUserAsync(user);
                return Ok(id);
            }
            //TODO :  improve application to have global exception handler
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}