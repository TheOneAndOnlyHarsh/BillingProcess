using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingProcess.Models
{
    public class SuccessfulPayment
    {
        public string? SessionId { get; set; }

        public string? PaymentIntentId { get; set; }

    }
}
