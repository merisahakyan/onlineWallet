using Onlinewallet.Core.Database.Entities;
using Onlinewallet.Core.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Onlinewallet.Core.ServiceInterfaces
{
    public interface IUsersService
    {
        Task CreateUserAsync(UserViewModel user);
    }
}
