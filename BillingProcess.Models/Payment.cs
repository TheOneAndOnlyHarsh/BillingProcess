using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingProcess.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }

        public int BillId { get; set; }

        [ForeignKey("BillId")]

        public virtual Bill? Bill { get; set; }
        [Required(ErrorMessage = "Payment date is required")]

        public DateTime PaymentDate { get; set; }
        [Required(ErrorMessage = "Payment amount is required")]

        public decimal PaymentAmount { get; set; }

        [Column(TypeName = "int")]

        [EnumDataType(typeof(PaymentType), ErrorMessage = "Invalid payment type")]
        public PaymentType PaymentType { get; set; }

        public string? SessionId { get; set; }

        public string? PaymentIntentId { get; set; }

    }
        public enum PaymentType
        {
            CreditCard =1,
            DebitCard=2,
            Cash=3,
            Check=4,
            
        }
}

