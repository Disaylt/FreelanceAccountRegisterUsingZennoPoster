using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneNumbersLayer.Models.OnlineSim
{
    internal class PhoneNumberStateModel
    {
        public string Number { get; set; }
        public string Msg { get; set; }
        public int Time { get; set; }
        public int Tzid { get; set; }
    }
}
