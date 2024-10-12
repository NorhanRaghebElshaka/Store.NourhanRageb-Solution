using Microsoft.EntityFrameworkCore;
using Store.NourhanRageb.Domain.Entities;
using Store.NourhanRageb.Domain.Repositories.Contract;
using Store.NourhanRageb.Domain.Specifications;
using Store.NourhanRageb.Repository.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.NourhanRageb.Repository.Repositories
{
    public class GenericRepositroy<TEntity, TKey> : IGenericRepositroy<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly StoreDbContext _context;

        public GenericRepositroy(StoreDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            if (typeof(TEntity) == typeof(Product))
            {
                return (IEnumerable <TEntity> )await _context.Products.Include(p=>p.Brand).Include(p=>p.type).ToListAsync();
            }
         return  await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetAsync(TKey id)
        {
            if (typeof(TEntity) == typeof(Product))
            {
                return await _context.Products.Include(p => p.Brand).Include(p => p.type).FirstOrDefaultAsync(P=>P.Id == id as int?) as TEntity;
            }
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task AddAsync(TEntity entity)
        {
           await _context.AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            _context.Update(entity);
        }

        public void Delete(TEntity entity)
        {
             _context.Remove(entity);
        }
        public async Task<IEnumerable<TEntity>> GetAllWithSpecsAsync(ISpecifications<TEntity, TKey> spec)
        {
            return await ApplySpecifications(spec).ToListAsync();
        }

        public async Task<TEntity> GetWithSpecsAsync(ISpecifications<TEntity, TKey> spec)
        {
            return await ApplySpecifications(spec).FirstOrDefaultAsync();
        }
        private IQueryable<TEntity> ApplySpecifications(ISpecifications<TEntity, TKey> spec)
        {
            return SpecificationsEvaluator<TEntity, TKey>.GetQuery(_context.Set<TEntity>(), spec);
        }

    }
}
