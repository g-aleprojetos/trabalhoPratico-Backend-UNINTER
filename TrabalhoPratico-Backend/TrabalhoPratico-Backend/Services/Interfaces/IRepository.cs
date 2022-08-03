using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TrabalhoPratico_Backend.Services.Interfaces
{
    public interface IRepository
    {
        void Add<T>(T entity) where T : BaseEntity;
        Task<T> GetByIdAsync<T>(Guid id) where T : BaseEntity;
        Task<T> GetByNameAsync<T>(string name) where T : BaseEntity;
        Task<List<T>> ListAsync<T>() where T : BaseEntity;
        Task<List<T>> ListAsync<T>(ISpecification<T> spec) where T : BaseEntity;
        Task<T> AddAsync<T>(T entity) where T : BaseEntity;
        Task UpdateAsync<T>(T entity) where T : BaseEntity;
        Task DeleteAsync<T>(T entity) where T : BaseEntity;
        Task DeleteLogicAsync<T>(T entity) where T : BaseEntity;


        
    }
}
