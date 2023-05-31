using DataLayer.DTO.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository.Interface
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetPagedRecords(int pageNumber = 1, int pageSize = 10);
        Task<TEntity> GetByIdAsync(int id);
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(List<TEntity> entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        TEntity GetEntity(Func<IQueryable<TEntity>, TEntity> func);

        //List<CadrePayrollInfo> GetEntities(Func<IQueryable<TEntity>, List<CadrePayrollInfo>> func);
    }
}
