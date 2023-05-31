using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class BasicSalary
    {
        public int BasicSalaryId { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        //Navigation Properties
        public Category Category { get; set; }
    }
}
