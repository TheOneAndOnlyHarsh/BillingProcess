using BillingProcess.DataAccess;
using BillingProcess.Models;
using BillingProcess.Repository.IRepository;

namespace BillingProcess.Repository
{
    public class PaymentRespository : Repository<Payment>, IPaymentRepository
    {
        private readonly ApplicationDbContext _db;
        public PaymentRespository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Payment> UpdateAsync(Payment entity)
        {
            _db.Update(entity);
            await SaveAsync();
            return entity;
        }
    }
}
