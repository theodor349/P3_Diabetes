using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIDataAccess.Models.Account
{
    public class UpdateAccountModel
    {
        public string ID { get; set; }
        public string Firstname { get; set; }
        public string LastName { get; set; }
        public string ProneNumber { get; set; }
    }
}
