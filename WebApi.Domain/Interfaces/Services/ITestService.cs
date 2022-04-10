using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebApi.Domain.CustomGenerator;
using WebApi.Domain.Entities;

namespace WebApi.Domain.Interfaces.Services
{
    public interface ITestService
    {
        Task<PagedList<Test>> GetAll(int pageNumber, int pageSize,string include,Expression<Func<Test, bool>> where = null,string orderBy = null);
    }
}