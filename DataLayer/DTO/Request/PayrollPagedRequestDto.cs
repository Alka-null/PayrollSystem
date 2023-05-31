using DataLayer.DTO.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DTO.Request
{
    public class PayrollPagedRequestDto: BasePagedRequest
    {
        [Range(1, 12, ErrorMessage = "Value must be between 1 and 12")]
        public int monthindex { get; set; }
        public int year { get; set; }
    }
}
