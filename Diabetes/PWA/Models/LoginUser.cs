using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWA.Models
{
    public class LoginUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string NSLink { get; set; }
        public string UserID { get; set; }
        public bool UnitOfMeasure { get; set; }
        public string Token { get; set; }

        public string GetName()
        {
            return FirstName + " " + LastName;
        }
    }
}
