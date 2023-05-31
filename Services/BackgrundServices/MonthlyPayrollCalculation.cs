using DataLayer.DTO.Utilities;
using DataLayer.Repository.Interface;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Services.BackgrundServices
{
    public class MonthlyPayrollCalculation: IMonthlyPayrollCalculation
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IServiceScopeFactory _scopeFactory;

        public MonthlyPayrollCalculation(IServiceScopeFactory scopeFactory)
        {
            using (var scope = scopeFactory.CreateScope())
            {
                _unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            }
        }
        public void CreateMonthlyPayroll() {
            //var cadrepayrollinfos = _unitOfWork.Cadres.GetEntities((x) =>
            //{
            //    var cadres = x.Include(s => s.BasicSalary).Include(p => p.Tax);
            //    //var taxdeductions = new List<object>();
            //    var cadreinfos = cadres.Select(d => new CadrePayrollInfo
            //    {
            //        CadreId = d.CadreId,
            //        TaxWithheld = d.BasicSalary.Amount * d.Tax.TaxRate,
            //        BasicSalary = d.BasicSalary.Amount,
            //        HousingAllowance = d.HousingAllowance.Amount,
            //        Pension = d.BasicSalary.Amount * d.Pension.Rate
            //    }).ToList();
            //    return cadreinfos;
            //});

            var cadres = _unitOfWork.Cadres.GetAll().Include(s => s.BasicSalary).Include(p => p.Tax);
            var cadrepayrollinfos = cadres.Select(d => new CadrePayrollInfo
            {
                CadreId = d.CadreId,
                TaxWithheld = d.BasicSalary.Amount * d.Tax.TaxRate,
                BasicSalary = d.BasicSalary.Amount,
                HousingAllowance = d.HousingAllowance.Amount,
                Pension = d.BasicSalary.Amount * d.Pension.Rate
            }).ToList();
            //return cadreinfos;
     
            var employeesId = _unitOfWork.Employees.GetAll().ForEachAsync(employee =>
            {
                var cadre = cadrepayrollinfos.Where(c => c.CadreId == employee.Cadre.CadreId).FirstOrDefault();
                if(cadre != null)
                {
                    _unitOfWork.Transactions.AddAsync(new Transaction
                    {
                        Employee = employee,
                        IsDisbursed = false,
                        BasicSalary = cadre.BasicSalary,
                        CreatedDate = DateTime.Now,
                        HousingAllowance = cadre.HousingAllowance,
                        TaxWithheld = cadre.TaxWithheld,
                        Pension = cadre.Pension
                    });
                }
            });

            _unitOfWork.CompleteAsync();
        }
    }
}