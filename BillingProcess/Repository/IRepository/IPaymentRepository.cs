using BillingProcess.Models;

namespace BillingProcess.Repository.IRepository
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        Task<Payment> UpdateAsync(Payment entity);
    }
}
