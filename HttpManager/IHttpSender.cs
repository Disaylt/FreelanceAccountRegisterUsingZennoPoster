using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpManager
{
    public interface IHttpSender
    {
        HttpSettings HttpSettings { get; }
        HttpClientHandler HttpClientHandler { get; }
        HttpResponseMessage Send(HttpMethod method, string url);
        HttpResponseMessage Send(HttpMethod method, string url, HttpContent httpContent);
    }
}
