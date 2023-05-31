using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Cadre
    {
        public int CadreId { get; set; }
        public string CadreName { get; set; }
        //public int PositionId { get; set; }
        //public int TaxId { get; set; }
        //public int PensionId { get; set; }
        //public int HousingAllowanceId { get; set; }
        //public int BasicSalaryId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        //Navigation properties
        public List<Employee> Employees { get; set;}
        public Position Position { get; set; }
        public Tax Tax { get; set; }
        public Pension Pension { get; set; }
        public HousingAllowance HousingAllowance { get; set; }
        public BasicSalary BasicSalary { get; set; }
        //public Category Category { get; set; }

    }
}
