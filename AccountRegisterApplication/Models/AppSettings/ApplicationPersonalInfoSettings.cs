using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountRegisterApplication.Models.AppSettings
{
    internal class ApplicationPersonalInfoSettings
    {
        public string Gender { get; set; }
        public List<string> MaleNames { get; set; }
        public List<string> FemaleNames { get; set; }
    }
}
