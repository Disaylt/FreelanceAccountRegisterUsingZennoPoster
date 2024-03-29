﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountRegisterApplication.Models.WbBuyer
{
    internal class WbAccountModel
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Cookies { get; set; }
        public string UserAgent { get; set; }
        public string RegisterProxy { get; set; }
        public string NormalizedGender { get; set; }
        public bool IsActive { get; set; }
    }
}
