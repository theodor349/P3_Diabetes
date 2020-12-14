using System;
namespace APIDataAccess.Models.Account
{
    public class UpdateAccountDBModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsEU { get; set; }
    }
}
