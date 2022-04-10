using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebApi.Domain.CustomGenerator;

namespace WebApi.Domain.Interfaces
{
    public interface IGenericRepository<T>
    {
        Task<T> GetOne(Expression<Func<T, bool>> where,string include = null);
        Task<PagedList<T>> GetAll( string include = null,Expression<Func<T, bool>> where = null,string orderBy = null,int pageNumber = 1, int pageSize=10);
        Task Add(T entity);
        void Update(T entity);
        Task Delete(Expression<Func<T, bool>> where);
    }
}