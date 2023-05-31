using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using DataLayer.Repository.Interface;
using DataAccess.Repositories;

namespace DataLayer.Repository.Implementation
{

    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IEmployeeRepository _employeeRepository;
        private ICadreRepository _cadreRepository;
        private ICategoryRepository _categoryRepository;
        private IHousingAllowanceRepository _housingAllowanceRepository;
        private IPensionRepository _pensionRepository;
        private IPositionRepository _positionRepository;
        private ITaxRepository _taxRepository;
        private ITransactionRepository _transactionRepository;
        private IDepartmentRepository _departmentRepository;
        private IBasicSalaryRepository _basicSalaryRepository;
        public UnitOfWork(AppDbContext context, IEmployeeRepository employeeRepository,
            ICadreRepository cadreRepository, ICategoryRepository categoryRepository,
            IHousingAllowanceRepository housingAllowanceRepository, IPensionRepository pensionRepository,
            IPositionRepository positionRepository, ITaxRepository taxRepository,
            IDepartmentRepository departmentRepository, IBasicSalaryRepository basicSalaryRepository,
            ITransactionRepository transactionRepository)
        {
            this._employeeRepository = employeeRepository;
            this._context = context;
            this._cadreRepository = cadreRepository;
            this._categoryRepository = categoryRepository;
            this._housingAllowanceRepository = housingAllowanceRepository;
            this._pensionRepository = pensionRepository;
            this._positionRepository = positionRepository;
            this._taxRepository = taxRepository;
            this._transactionRepository = transactionRepository;
            this._basicSalaryRepository = basicSalaryRepository;
            this._departmentRepository = departmentRepository;
        }

        public IEmployeeRepository Employees => _employeeRepository ??= new EmployeeRepository(_context);
        public ICadreRepository Cadres => _cadreRepository ??= new CadreRepository(_context);
        public ICategoryRepository Categories => _categoryRepository ??= new CategoryRepository(_context);
        public IHousingAllowanceRepository HousingAllowances => _housingAllowanceRepository ??= new HousingAllowanceRepository(_context);
        public IPensionRepository Pensions => _pensionRepository ??= new PensionRepository(_context);
        public IPositionRepository Positions => _positionRepository ??= new PositionRepository(_context);
        public ITransactionRepository Transactions => _transactionRepository ??= new TransactionRepository(_context);
        public ITaxRepository Taxes => _taxRepository ??= new TaxRepository(_context);
        public IBasicSalaryRepository BasicSalaries => _basicSalaryRepository ??= new BasicSalaryRepository(_context);
        public IDepartmentRepository Departments => _departmentRepository ??= new DepartmentRepository(_context);

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

}