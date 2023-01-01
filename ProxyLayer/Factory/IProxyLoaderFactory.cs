using ProxyLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyLayer.Factory
{
    public interface IProxyLoaderFactory
    {
        IProxyLoader Create();
    }
}
