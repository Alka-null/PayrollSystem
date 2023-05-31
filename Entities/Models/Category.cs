using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Enums;

namespace Entities.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public Enums.TypeEnum Type { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public List<Tax> Taxes { get; set; }
        public List<Pension> Pensions { get; set; }
        public List<BasicSalary> BasicSalaries { get; set; }
        public List<HousingAllowance> HousingAllowances { get; set; }
    }
}
