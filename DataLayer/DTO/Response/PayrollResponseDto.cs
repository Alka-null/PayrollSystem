using DataLayer.DTO.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DTO.Response
{
    public class PayrollResponseDto
    {
        public Earnings Earnings { get; set; }
        public Deductions Deductions { get; set; }
        public bool IsDisbursed { get; set; }
    }
}
