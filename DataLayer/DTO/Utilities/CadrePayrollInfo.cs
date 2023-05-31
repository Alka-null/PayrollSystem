using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DTO.Utilities
{
    public class CadrePayrollInfo
    {
        
        public int CadreId { get; set; }
        public decimal TaxWithheld { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal HousingAllowance { get; set; }
        public decimal Pension { get; set; }
    }
}
