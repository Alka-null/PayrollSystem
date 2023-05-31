using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace DataLayer.DTO.Utilities
{
    public class Deductions
    {
        public decimal TaxWithheld { get; set; }
        public decimal Pension { get; set; }
    }
}
