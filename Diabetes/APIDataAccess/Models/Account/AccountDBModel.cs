using System;
using System.Collections.Generic;
using System.Text;

namespace APIDataAccess.Models.Account
{
    public class AccountDBModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NSLink { get; set; }
        public bool UnitOfMeasure { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
