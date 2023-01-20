using ProxyLayer.Abstract;
using ProxyLayer.Implementations;
using ProxyLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyLayer.Factory
{
    public class SoaxProxyLoaderFactory : IProxyLoaderFactory
    {
        private readonly SoaxProxySettingsModel _settings;

        public SoaxProxyLoaderFactory(SoaxProxySettingsModel settings)
        {
            _settings = settings;
        }

        public IProxyLoader Create()
        {
            IProxyLoader proxyLoader = new SoaxProxyLoader(_settings);

            return proxyLoader;
        }
    }
}
