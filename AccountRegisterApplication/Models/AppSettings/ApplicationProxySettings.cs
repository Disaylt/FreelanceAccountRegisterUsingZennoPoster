using ProxyLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountRegisterApplication.Models.AppSettings
{
    internal class ApplicationProxySettings
    {
        public string LoaderType { get; set; }
        public string FileName { get; set; }
        public SoaxProxySettingsModel SoaxProxySettings { get; set; }
    }
}
