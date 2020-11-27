using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIDataAccess.Models.Account
{
    public class CreateAccountModel
    {
        public CreateAccountModel()
        {

        }
        public CreateAccountModel(string firstName, string lastName, string email, string phoneNumber, string password, string nSLink, bool isEUMeasure)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            Password = password;
            NSLink = nSLink;
            IsEUMeasure = isEUMeasure;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string NSLink { get; set; }
        public bool IsEUMeasure { get; set; }
    }
}
