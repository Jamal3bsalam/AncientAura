using AncientAura.Core.Entities;
using AncientAura.Core.Repositories.Contracts;
using AncientAura.Repository.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Repository.Repositories
{
    public class GenericRepository<TEntity, Tkey> : IGenericRepository<TEntity, Tkey> where TEntity : BaseEntitiy<Tkey>
    {
        private readonly AncientAuraDbContext _context;

        public GenericRepository(AncientAuraDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            if(typeof(TEntity) == typeof(AncientSites))
            {
               return (IEnumerable<TEntity>) await _context.Set<AncientSites>().Include(A => A.Reviews).Include(A => A.ImageURLs).ToListAsync();
            }
          return await _context.Set<TEntity>().ToListAsync();
        }

        public Task<TEntity> GetByIdAsync(Tkey Id)
        {
            throw new NotImplementedException();
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
    }
}
