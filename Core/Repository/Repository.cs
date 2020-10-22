﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Core.Repository
{
    public class Repository : IRepository
    {
        private readonly ShopDbContext _context;
        public Repository(ShopDbContext context)
        {
            _context = context;
        }

        public async Task<T> Get<T>(Expression<Func<T,bool>> expression) where T : class
        {
            return await _context.Set<T>().Where(expression).SingleOrDefaultAsync();
        }

        public IQueryable<T> GetQuery<T>(Expression<Func<T, bool>> expression) where T : class
        {
            return  _context.Set<T>().Where(expression);
        }

        public async Task<bool> Any<T>(Expression<Func<T, bool>> expression) where T : class
        {
            return await _context.Set<T>().AnyAsync(expression);
        }
        public IEnumerable<T> GetAll<T>() where T: class
        {
            return _context.Set<T>().ToList();
        }
        public T GetById<T>(object id) where T : class
        {
            return _context.Set<T>().Find(id);
        }
        public async Task Insert<T>(T obj) where T : class
        {
           await _context.Set<T>().AddAsync(obj);
        }
        public void Update<T>(T obj) where T : class
        {
            _context.Set<T>().Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }

        public void Delete<T>(object id) where T : class
        {
            T existing = _context.Set<T>().Find(id);
            _context.Remove(existing);
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
