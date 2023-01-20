using ProxyLayer.Abstract;
using ProxyLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyLayer.Implementations
{
    internal class SoaxProxyLoader : IProxyLoader
    {
        private readonly SoaxProxySettingsModel _settings;
        private readonly static Random _random = new Random();

        public SoaxProxyLoader(SoaxProxySettingsModel soaxProxySettingsModel)
        {
            _settings = soaxProxySettingsModel;
        }

        public string Get()
        {
            int port = _random.Next(_settings.MinPort, _settings.MaxPort);
            string proxy = $"http://{_settings.Login}:{_settings.Password}@{_settings.Host}:{port}";

            return proxy;
        }
    }
}
