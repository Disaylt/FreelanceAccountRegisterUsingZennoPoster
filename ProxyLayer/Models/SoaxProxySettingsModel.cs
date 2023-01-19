using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyLayer.Models
{
    public class SoaxProxySettingsModel
    {
        public string Host { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int MaxPort { get; set; }
        public int MinPort { get; set; }
    }
}
