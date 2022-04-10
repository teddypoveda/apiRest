using System;
using System.Threading.Tasks;
using WebApi.Domain.Entities;

namespace WebApi.Domain.Interfaces
{
    public interface IRepositoryManager : IDisposable
    {
        public IGenericRepository<Test> Test { get;}
        Task CompleteAsync();
    }
}