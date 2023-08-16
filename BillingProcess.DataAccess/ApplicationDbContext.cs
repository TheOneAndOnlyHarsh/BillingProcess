using BillingProcess.Models;
using Microsoft.EntityFrameworkCore;

namespace BillingProcess.DataAccess
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }

        public DbSet<Bill> Bills { get; set; }
        public DbSet<Payment> Payments { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<LocalUser> LocalUsers { get; set; }



    }
}