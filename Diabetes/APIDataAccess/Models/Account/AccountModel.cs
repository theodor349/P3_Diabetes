using System;
using System.Collections.Generic;
using System.Text;

namespace APIDataAccess.Models.Account
{
    public class AccountModel
    {
        public string ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string NSLink { get; set; }
        public bool IsEUMeasure { get; set; }
    }
}
