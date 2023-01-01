using ProxyLayer.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyLayer.Implementations
{
    internal class TxtProxyLoader : IProxyLoader
    {
        private string _fileName;

        private static readonly object _lock = new object();

        public TxtProxyLoader(string fileName)
        {
            _fileName = fileName;
        }

        public string Get()
        {
            lock (_lock)
            {
                List<string> proxies = GetProxies();
                string proxy = TakeFirstElementAndUpdateFile(proxies);

                return proxy;
            }
        }

        private List<string> GetProxies()
        {
            List<string> proxies = File.ReadAllLines(_fileName)
                .ToList();

            if (proxies.Count == 0)
                throw new NullReferenceException("Proxies list is empty.");

            return proxies;
        }

        private string TakeFirstElementAndUpdateFile(List<string> proxies)
        {
            string proxy = proxies[0];

            proxies.RemoveAt(0);
            File.WriteAllLines(_fileName, proxies);

            return proxy;
        }
    }
}
