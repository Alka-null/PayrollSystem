using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Employee: IdentityUser
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        //public int CadreId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string DepartmentId { get; set; }

        //Navigation Properties
        public Cadre Cadre { get; set; }
    }
}
