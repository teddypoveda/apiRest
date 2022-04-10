using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebApi.Domain.CustomGenerator;
using WebApi.Domain.Entities;
using WebApi.Domain.Interfaces;
using WebApi.Domain.Interfaces.Services;

namespace WebApi.Services
{
    public class TestService : ITestService
    {
        private readonly IRepositoryManager _repositoryManager;

        public TestService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<PagedList<Test>> GetAll(int pageNumber, int pageSize, string include, Expression<Func<Test, bool>> where = null, string orderBy = null)
        {
            /* Aqui se puede hacer mas logica ,como enviar correos idk */
            return await _repositoryManager.Test.GetAll(null, null, orderBy, pageNumber, pageSize);
        }
    }
}