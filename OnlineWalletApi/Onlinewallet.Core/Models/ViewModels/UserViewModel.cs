using System;
using System.Collections.Generic;
using System.Text;

namespace Onlinewallet.Core.Models.ViewModels
{
    public class UserViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Passport { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
    }
}
