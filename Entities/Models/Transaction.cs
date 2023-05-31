using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public decimal TaxWithheld { get; set; }
        public decimal Pension { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal HousingAllowance { get; set; }
        public bool IsDisbursed { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime DateDisbursed { get; set; } = DateTime.Now;

        //Navigation properties
        public Employee Employee { get; set; }
    }
}
