using PhoneNumbersLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountRegisterApplication.Models.AppSettings
{
    internal class AplicationSmsManagerSettings
    {
        public string ServiceName { get; set; }
        public PhoneNumbersSettings PhoneNumbersSettings { get; set; }
    }
}
