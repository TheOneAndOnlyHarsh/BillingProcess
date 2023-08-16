using BillingProcess.DataAccess;
using BillingProcess.Models;
using BillingProcess.Repository.IRepository;

namespace BillingProcess.Repository
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {

        private readonly ApplicationDbContext _db;
        public StudentRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Student> UpdateAsync(Student entity)
        {
            _db.Update(entity);
            await SaveAsync();
            return entity;
        }
    }
}
