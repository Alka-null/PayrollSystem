using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DTO.Request
{
    public class CreateEmployeeRequestDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        
        public string Password { get; set; }
        public int CadreId { get; set; }
        //public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string DepartmentId { get; set; }
    }
}
