﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingProcess.Models.DTO
{
    public class RegistrationRequestDTO
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
    }
}
