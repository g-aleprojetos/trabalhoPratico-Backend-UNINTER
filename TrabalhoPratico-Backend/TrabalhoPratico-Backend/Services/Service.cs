using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Context;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrabalhoPratico_Backend;
using TrabalhoPratico_Backend.Services.Interfaces;

namespace Services
{
    public class Service : IRepository
    {
        private readonly ApiContext _context;

        public Service(ApiContext context)
        {
            _context = context;
        }

        private IQueryable<T> ApplySpecification<T>(ISpecification<T> spec) where T : BaseEntity
        {
            return SpecificationEvaluator.Default.GetQuery(_context.Set<T>().AsQueryable(), specification: spec);
        }

        public void Add<T>(T entity) where T : BaseEntity
        {
            _context.Add(entity);
        }

        public Task<T> GetByIdAsync<T>(Guid id) where T : BaseEntity
        {
            return _context.Set<T>().SingleOrDefaultAsync(e => e.Id == id);
        }

        public Task<T> GetByLoginAsync<T>(string login) where T : User
        {
            return _context.Set<T>().SingleOrDefaultAsync(e => e.Login == login && e.Deletada == false);
        }

        public Task<List<T>> ListAsync<T>() where T : BaseEntity
        {
            return _context.Set<T>().ToListAsync();
        }
        public async Task<List<T>> ListAsync<T>(ISpecification<T> spec) where T : BaseEntity
        {
            var specificationResult = ApplySpecification(spec);
            return await specificationResult.ToListAsync();
        }

        public async Task<T> AddAsync<T>(T entity) where T : BaseEntity
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync<T>(T entity) where T : BaseEntity
        {
            _context.Entry(entity).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync<T>(T entity) where T : BaseEntity
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLogicAsync<T>(T entity) where T : BaseEntity
        {
            entity.Deletada = true;
            await UpdateAsync(entity);
        }
    }
}
