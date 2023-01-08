using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountRegisterApplication.Models.WbBuyer
{
    internal class SessionModel
    {
        public string SessionId { get; set; }
        public string DeviceName { get; set; }
        public string LastVisit { get; set; }
        public string IpAddress { get; set; }
        public string AppName { get; set; }
        public bool IsCurrentSession { get; set; }
        public bool CanFinish { get; set; }
    }
}
