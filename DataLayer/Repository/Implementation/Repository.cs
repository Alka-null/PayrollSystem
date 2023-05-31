using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using DataLayer.Repository.Interface;
using DataLayer.DTO.Utilities;
using Entities.Models;

namespace DataLayer.Repository.Implementation
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
            
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbSet.AsQueryable();
        }
        public IQueryable<TEntity> GetPagedRecords(int pageNumber = 1, int pageSize = 10)
        {
            return _dbSet.Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize)
                        .AsQueryable();
        }
        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(List<TEntity> entity)
        {
            await _dbSet.AddRangeAsync(entity);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        //public List<CadrePayrollInfo> GetEntities(Func<IQueryable<TEntity>, List<CadrePayrollInfo>> func)
        //{

        //    return func(_dbSet.AsQueryable());
        //}

        public TEntity GetEntity(Func<IQueryable<TEntity>, TEntity> func)
        {

            return func(_dbSet.AsQueryable());
        }
        public Task<IQueryable<TEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }

}
