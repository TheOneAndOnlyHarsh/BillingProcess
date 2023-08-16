using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BillingProcess.Models
{
    public class Bill
    {
        public int BillID { get; set; }

/*        public string? BillName { get; set; }
*/        public int StudentID { get; set; }
        [ForeignKey("StudentID")]
        public virtual Student? Student { get; set; }
        [Required(ErrorMessage = "Amount due is required")]

        public decimal? AmountDue { get; set; }
        [DataType(DataType.Date)]

        public DateTime? DueDate { get; set; }
        [Required(ErrorMessage = "Payment status is required")]

        public bool? IsPaid { get; set; }
    }
}
