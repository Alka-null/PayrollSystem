using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DTO.Request
{
    public class CreateEmployeeResponseDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int CadreId { get; set; }
        //public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string DepartmentId { get; set; }
        public string Token { get; set; }
    }
}
