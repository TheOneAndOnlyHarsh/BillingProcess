using BillingProcess.Models;

namespace BillingProcess.Repository.IRepository
{
    public interface IStudentRepository : IRepository<Student>
    {
        Task<Student> UpdateAsync(Student entity);
    }
}
