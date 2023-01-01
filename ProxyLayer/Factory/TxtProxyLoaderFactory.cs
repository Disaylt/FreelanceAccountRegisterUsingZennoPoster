using ProxyLayer.Abstract;
using ProxyLayer.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyLayer.Factory
{
    public class TxtProxyLoaderFactory : IProxyLoaderFactory
    {
        private readonly string _fileName;
        
        public TxtProxyLoaderFactory(string fileName)
        {
            _fileName = fileName;
        }

        public IProxyLoader Create()
        {
            IProxyLoader proxyLoader = new TxtProxyLoader(_fileName);

            return proxyLoader;
        }
    }
}
