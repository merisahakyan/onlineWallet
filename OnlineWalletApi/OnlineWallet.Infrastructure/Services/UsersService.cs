using Onlinewallet.Core.Database.Entities;
using Onlinewallet.Core.Models.ViewModels;
using Onlinewallet.Core.RepositoryInterfaces;
using Onlinewallet.Core.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineWallet.Infrastructure.Services
{
    public class UsersService : IUsersService
    {
        readonly IUsersRepository _userRepository;
        public UsersService(IUsersRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<int> CreateUserAsync(UserViewModel user)
        {
            var dbUser = new User
            {
                Address = user.Address,
                City = user.City,
                Country = user.Country,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Mobile = user.Mobile,
                Passport = user.Passport,
                PostalCode = user.PostalCode
            };
            _userRepository.Add(dbUser);
            await _userRepository.SaveChangesAsync();

            return dbUser.Id;
        }
    }
}