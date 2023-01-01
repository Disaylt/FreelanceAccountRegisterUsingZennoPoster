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
    internal class ProxyInstanceInstaller
    {
        private readonly ApplicationProxySettings _proxySettings;

        public ProxyInstanceInstaller(ApplicationProxySettings proxySettings)
        {
            _proxySettings = proxySettings;
        }

        public void SetProxy(Instance instance)
        {
            IProxyLoader proxyLoader = GetProxyLoader();
            string proxy = proxyLoader.Get();
            instance.SetProxy(proxy, emulateGeolocation: true, emulateTimezone: true, emulateWebrtc: true);
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
