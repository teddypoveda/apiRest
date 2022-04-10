using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApi.Domain.Base;
using WebApi.Domain.CustomGenerator;
using WebApi.Domain.Interfaces;
using WebApi.Infrastructure.Context;

namespace WebApi.Infrastructure
{
    public class GenericRepository<T> : IGenericRepository<T> where T : Base
    {
        private readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<T> GetOne(Expression<Func<T, bool>> where,string include = null)
        {
            return await _context.Set<T>().Where(where).FirstOrDefaultAsync();
            
        }

        public async Task<PagedList<T>> GetAll(string include = null, Expression<Func<T, bool>> where = null,string orderBy = null, int pageNumber = 1, int pageSize = 10)
        {
            IQueryable<T> query = _context.Set<T>();
            if (where != null)
            {
                query = query.Where(where);
            }
            if (orderBy != null)
            {
                query = query.OrderBy(orderBy);
            }else
            {
                query = query.OrderBy(x => x.Id);
            }

            if (include !=null)
            {
                foreach (var includeProperty in include.Split(new char[]{','},StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            
            var registros = await query.ToListAsync();
            return PagedList<T>.Create(registros,pageNumber,pageSize);
        }

        public async Task Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public void Update(T entity)
        {
            _context.Update(entity);
        }

        public async Task Delete(Expression<Func<T, bool>> where)
        {
            var entity = await _context.Set<T>().FirstOrDefaultAsync(where);
            _context.Set<T>().Remove(entity);
        }
    }
}