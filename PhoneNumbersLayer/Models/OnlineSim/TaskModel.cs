using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneNumbersLayer.Models.OnlineSim
{
    internal class TaskModel
    {
        public int Tzid { get; set; }
        public string Number { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
