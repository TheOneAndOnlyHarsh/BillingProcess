using BillingProcess.DataAccess;
using BillingProcess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BillingProcess.Repository
{
    
        public class Repository<T> : IRepository<T> where T : class
        {
            private readonly ApplicationDbContext _context;
            private readonly DbSet<T> _dbSet;

            public Repository(ApplicationDbContext context)
            {
                _context = context;
                _dbSet = context.Set<T>();
            }

            public async Task<List<T>> GetAllAsync()
            {
                return await _dbSet.ToListAsync();
            }

            public async Task<T> GetAsync(int id)
            {
                return await _dbSet.FindAsync(id);
            }

            public async Task CreateAsync(T entity)
            {


                await _dbSet.AddAsync(entity);
            }

            public async Task RemoveAsync(T entity)
            {

                _dbSet.Remove(entity);
            }

            public async Task SaveAsync()
            {
                await _context.SaveChangesAsync();
            }

        }
    
}
