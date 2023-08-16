using BillingProcess.Models;

namespace BillingProcess.Repository.IRepository
{
    public interface IBillRepository : IRepository<Bill>
    {
        Task<Bill> UpdateAsync(Bill entity);
    }
}
