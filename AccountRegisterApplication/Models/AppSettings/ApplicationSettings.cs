using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountRegisterApplication.Models.AppSettings
{
    internal class ApplicationSettings
    {
        public string Site { get; set; }
        public ApplicationProxySettings ProxySettings { get; set; }
    }
}
