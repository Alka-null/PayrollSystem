using DataLayer.DTO.Response;
using DataLayer.DTO.Utilities;
using DataLayer.Repository.Interface;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.PayrollService
{
    public class PayrollService: IPayrollService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PayrollService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public PayrollResponseDto getPayrollForMonthAandYear(int employeedId, int month, int year) {
            var employee = _unitOfWork.Employees.GetByIdAsync(employeedId).Result;
            var monthyeardelegate = (Transaction p) =>
            {
                var dateandtime = p.CreatedDate;
                var _month = dateandtime.Month;
                var _year = dateandtime.Year;

                if (_month == month && _year == year)
                {
                    return true;
                }

                return false;
            };

            var transaction = _unitOfWork.Transactions.GetEntity(x => x.Where(p => p.Employee == employee && monthyeardelegate(p))
                                    .FirstOrDefault());
            return new PayrollResponseDto
            {
                Earnings = new Earnings
                {
                    BasicSalary = transaction.BasicSalary,
                    HousingAllowance = transaction.HousingAllowance,
                },
                Deductions = new Deductions
                {
                    Pension = transaction.Pension,
                    TaxWithheld= transaction.TaxWithheld,
                }
            };
        }
    }
}
