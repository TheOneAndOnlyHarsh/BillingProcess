using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingProcess.Models.DTO
{
    public class LoginResponseDTO
    {
        public LocalUser User { get; set; }

        public string Token { get; set; }
    }
}
