using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Permission
    {
        public int Id { get; set; }
        public string TargetId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsOwner { get; set; }
        public bool BloodGlucose { get; set; }
        public bool Battery { get; set; }
        public bool Insulin { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
