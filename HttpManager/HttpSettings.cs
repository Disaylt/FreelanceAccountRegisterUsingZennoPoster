using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpManager
{
    public class HttpSettings
    {
        public List<Cookie> Cookies { get; set; } = new List<Cookie>();
        public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
        public IWebProxy Proxy { get; set; }
        public Version HttpVersion { get; set; } = new Version(1, 0);
    }
}
