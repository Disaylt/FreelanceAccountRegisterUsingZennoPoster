using AccountRegisterApplication.Models.AppSettings;
using ProxyLayer.Abstract;
using ProxyLayer.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZennoLab.CommandCenter;

namespace AccountRegisterApplication.RegisterServices.Proxy
{
    internal class ProxyLoader
    {
        private readonly ApplicationProxySettings _proxySettings;

        public ProxyLoader(ApplicationProxySettings proxySettings)
        {
            _proxySettings = proxySettings;
        }

        public string LoadProxy()
        {
            if(_proxySettings == null || string.IsNullOrEmpty(_proxySettings.LoaderType))
            {
                throw new NullReferenceException("Proxy settings is null");
            }

            IProxyLoader proxyLoader = GetProxyLoader();
            string proxy = proxyLoader.Get();

            return proxy;
        }

        private IProxyLoader GetProxyLoader()
        {
            IProxyLoaderFactory proxyLoaderFactory;

            switch (_proxySettings.LoaderType)
            {
                case "txt":
                    proxyLoaderFactory = new TxtProxyLoaderFactory(_proxySettings.FileName);
                    break;
                default:
                    throw new NullReferenceException($"Proxy loader type {_proxySettings.LoaderType} not found.");
            }

            IProxyLoader proxyLoader = proxyLoaderFactory.Create();

            return proxyLoader;
        }
    }
}
