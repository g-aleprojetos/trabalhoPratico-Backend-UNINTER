﻿using Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public void Add<T>(T entity) where T : BaseEntity
        {
            _context.Add(entity);
        }

        public Task<T> GetByIdAsync<T>(Guid id) where T : BaseEntity
        {
            return _context.Set<T>().SingleOrDefaultAsync(e => e.Id == id);
        }

        public Task<T> GetByNameAsync<T>(string name) where T : BaseEntity
        {
            return _context.Set<T>().SingleOrDefaultAsync(e => e.Name == name);
        }

        public Task<List<T>> ListAsync<T>() where T : BaseEntity
        {
            return _context.Set<T>().ToListAsync();
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
