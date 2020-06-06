using Onlinewallet.Core.Database.Entities;
using Onlinewallet.Core.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Onlinewallet.Core.ServiceInterfaces
{
    public interface IUsersService
    {
        void CreateUser(UserViewModel user);
    }
}
