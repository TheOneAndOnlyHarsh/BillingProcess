using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingProcess.Models
{
    public class Student
    {
        public int StudentId { get; set; }

        [Required(ErrorMessage = "Name is required")]

        public string Name { get; set; }
        [StringLength(100, ErrorMessage = "Contact details cannot exceed 100 characters")]

        public string ContactDetails { get; set; }

        public bool EnrollmentStatus { get; set; }

        public List<Bill>? Bills { get; set; } = new List<Bill>(); 

    }
}
