using BillingProcess.DataAccess;
using BillingProcess.Models;
using BillingProcess.Repository.IRepository;

namespace BillingProcess.Repository
{
    public class BillRepository : Repository<Bill>, IBillRepository
    {
        private readonly ApplicationDbContext _db;
        public BillRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Bill> UpdateAsync(Bill entity)
        {
            _db.Update(entity);
            await SaveAsync();
            return entity;
        }

    }
}
