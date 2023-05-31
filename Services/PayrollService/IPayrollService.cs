using DataLayer.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.PayrollService
{
    public interface IPayrollService
    {
        PayrollResponseDto getPayrollForMonthAandYear(int employeedId, int month, int year);
    }
}