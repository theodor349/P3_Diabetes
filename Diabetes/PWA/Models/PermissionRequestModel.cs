using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWA.Models
{
    public class PermissionRequestModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsCreater { get; set; }

        public string GetName()
        {
            return FirstName + " " + LastName;
        }
    }
}
