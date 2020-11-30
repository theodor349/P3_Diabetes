using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace PWA.Models
{
    public class Permission : IComparable<Permission>
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

        public string GetExpireDate()
        {
            return ExpireDate.ToString("g", CultureInfo.CreateSpecificCulture("es-ES"));
        }

        public string GetAccessString()
        {
            string s = "";
            if (BloodGlucose)
                s += "Bloodsugar";

            if (s.Length > 0 && Battery)
                s += ", Battery";
            else if (Battery)
                s += "Battery";

            if (s.Length > 0 && Insulin)
                s += ", Insulin";
            else if (Insulin)
                s += "Insuling";
            return s;
        }

        public string GetName()
        {
            return FirstName + " " + LastName;
        }

        public int CompareTo(Permission other)
        {
            return TargetId.CompareTo(other.TargetId);
        }
    }
}
