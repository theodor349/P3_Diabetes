﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIDataAccess.Models.Account
{
    public class UpdateAccountDBModel
    {
        public UpdateAccountDBModel(string id, string firstname, string lastname, string phoneNumber)
        {
            Id = id;
            FirstName = firstname;
            LastName = lastname;
            PhoneNumber = phoneNumber;
        }

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
