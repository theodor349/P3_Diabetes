﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.Account
{
    public class AccountModel
    {
        public string ID { get; set; }
        public string Firstname { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string NSLink { get; set; }
        public bool IsEUMeasure { get; set; }
    }
}
