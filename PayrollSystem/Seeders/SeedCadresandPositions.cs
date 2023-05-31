using DataLayer;
using DataLayer.Repository.Implementation;
using DataLayer.Repository.Interface;
using Entities.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;

namespace PayrollSystem.ExtensionMethods.Seeders
{
    public static class SeedCadresandPositions
    {
        public static void SeedCadresAndPositions(IApplicationBuilder app)
        {
            //using (var serviceScope = app.ApplicationServices.CreateScope())
            //{
            //    SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
            //}

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<IUnitOfWork>());
            }
        }

        private static void SeedData(IUnitOfWork unitOfWork)
        {
            try
            {
                Category earning = new Category
                {
                    Type = Entities.Enums.TypeEnum.Earnings
                };
                Category deduction = new Category
                {
                    Type = Entities.Enums.TypeEnum.Deductions
                };
                unitOfWork.Categories.AddRangeAsync(new List<Category> { earning, deduction});

                Dictionary<string, List<decimal>> cadresinfo = new Dictionary<string, List<decimal>>() {
                        { "JuniorAssociate", new List<decimal> { 0.01m, 0.02m, 30000, 2000} },
                        { "Trainee", new List<decimal> { 0.01m, 0.02m, 30000, 2000} },
                        { "Intern", new List<decimal> { 0.01m, 0.02m, 30000, 2000} },


                        { "Associate", new List<decimal> { 0.01m, 0.02m, 30000, 2000} },
                        { "SeniorAssociate", new List<decimal> { 0.01m, 0.02m, 30000, 2000} },
                        { "Specialist", new List<decimal> { 0.01m, 0.02m, 30000, 2000} },
                        { "Analyst", new List<decimal> { 0.01m, 0.02m, 30000, 2000} },

                    };

                var cadres = new List<Cadre>();
                var taxes = new List<Tax>();
                var pensions = new List<Pension>();
                var basicsalaries = new List<BasicSalary>();
                var housingallowances = new List<HousingAllowance>();

                if (!unitOfWork.Cadres.GetAll().Any())
                {
                    var i = 0;
                    foreach (var item in cadresinfo)
                    {
                        i++;
                        var tax = new Tax
                        {
                            TaxId= i,
                            CreatedDate = DateTime.Now,
                            TaxRate = item.Value[0],
                            Category = deduction
                        };
                        taxes.Add(tax);

                        var pension = new Pension
                        {
                            PensionId= i,
                            CreatedDate = DateTime.Now,
                            Rate = item.Value[1],
                            Category = deduction
                        };
                        pensions.Add(pension);

                        var basicsalary = new BasicSalary
                        {
                            BasicSalaryId= i,
                            CreatedDate = DateTime.Now,
                            Amount = item.Value[2],
                            Category = earning
                        };
                        basicsalaries.Add(basicsalary);

                        var housingallowance = new HousingAllowance
                        {
                            HousingAllowanceId= i,
                            CreatedDate = DateTime.Now,
                            Amount = item.Value[3],
                            Category = earning
                        };
                        housingallowances.Add(housingallowance);

                        cadres.Add(new Cadre
                        {
                            CadreId= i,
                            CadreName = item.Key,
                            CreatedDate = DateTime.Now,
                            Tax = tax,
                            Pension=pension,
                            BasicSalary=basicsalary,
                            HousingAllowance=housingallowance
                        });
                    }


                    var cadrenames = new List<string>
                    {
                        "JuniorAssociate",
                        "Trainee",
                        "Intern",


                        "Associate",
                        "SeniorAssociate",
                        "Specialist",
                        "Analyst",

                        "Manager",
                        "AssistantManager",
                        "SeniorManager",
                        "DepartmentHead",

                        "Director",
                        "VicePresident",
                        "SeniorVicePresident",

                        "ChiefExecutiveOfficer",
                        "ChiefFinancialOfficer",
                        "President",
                        "Chairman"
                    };

                }

                List<Position> positions = new List<Position>();

                if (!unitOfWork.Positions.GetAll().Any())
                {
                    var positionsname = new List<string> {
                        "EntryLevelPositions",
                        "MidLevelPositions",
                        "ManagerialPositions",
                        "SeniorLeadershipPositions",
                        "ExecutivePositions"
                        };

                    var j = 0;
                    foreach (var item in positionsname)
                    {
                        j++;
                        positions.Add(new Position
                        {
                            PositionId= j,
                            PositionName = item,
                            CreatedDate = DateTime.Now,
                        });
                    }



                    Console.WriteLine("Seeding database completed...");
                }
                else
                {
                    Console.WriteLine("Settlement accounts already available...");

                }

                cadres[0].Position = positions[0];
                cadres[1].Position = positions[0];
                cadres[2].Position = positions[0];
                cadres[3].Position = positions[1];
                cadres[4].Position = positions[1];
                cadres[3].Position = positions[1];


                unitOfWork.Taxes.AddRangeAsync(taxes);
                unitOfWork.Pensions.AddRangeAsync(pensions);
                unitOfWork.BasicSalaries.AddRangeAsync(basicsalaries);
                unitOfWork.HousingAllowances.AddRangeAsync(housingallowances);
                unitOfWork.Cadres.AddRangeAsync(cadres);
                unitOfWork.Positions.AddRangeAsync(positions);

                unitOfWork.CompleteAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong : {ex.Message}");
            }
        }


    }


}
