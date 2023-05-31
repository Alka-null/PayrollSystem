using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository.Interface
{
    //public interface IUnitOfWork : IDisposable
    //{
    //    IRepository<TEntity> Repository<TEntity>() where TEntity : class;
    //    void Save();
    //    Task SaveAsync();
    //}

    public interface IUnitOfWork : IDisposable
    {
        IEmployeeRepository Employees { get; }
        public ICadreRepository Cadres { get; }
        public ICategoryRepository Categories { get; }
        public IHousingAllowanceRepository HousingAllowances { get; }
        public IPensionRepository Pensions { get; }
        public IPositionRepository Positions { get; }
        public ITransactionRepository Transactions { get; }
        public ITaxRepository Taxes { get; }
        public IBasicSalaryRepository BasicSalaries { get; }
        public IDepartmentRepository Departments { get; }
        Task<int> CompleteAsync();
    }
}
