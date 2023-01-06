using HttpManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ZennoLab.CommandCenter;

namespace AccountRegisterApplication.RegisterServices.General
{
    internal abstract class InstanceHttpManager
    {
        private string _cookieDomain;
        private IHttpSender _httpSender;
        private readonly HttpSettings _httpSettings;

        public InstanceHttpManager(Instance instance, string cookieDomain)
        {
            Instance = instance;
            _cookieDomain = cookieDomain;

            _httpSettings = GetStartSettings(instance);
            _httpSender = new HttpSender(_httpSettings);
        }

        protected Instance Instance { get;  }

        public HttpResponseMessage Send(HttpMethod method, string url, HttpContent httpContent)
        {
            UpdateCookies();
            var response = _httpSender.Send(method, url, httpContent);

            return response;
        }

        public HttpResponseMessage Send(HttpMethod method, string url)
        {
            UpdateCookies();
            var response = _httpSender.Send(method, url);

            return response;
        }

        public void SetHeaders(Dictionary<string, string> headers)
        {
            _httpSender.HttpSettings.Headers = headers;
        }

        private void UpdateCookies()
        {
            _httpSettings.Cookies = GetCookies(Instance);
            _httpSender = new HttpSender(_httpSettings);
        }

        private HttpSettings GetStartSettings(Instance instance)
        {
            HttpSettings httpSettings = new HttpSettings
            {
                Cookies = GetCookies(instance),
                Proxy = GetProxy(instance),
                HttpVersion = new Version(1, 1)
            };

            return httpSettings;
        }

        private IWebProxy GetProxy(Instance instance)
        {
            string proxy = instance.GetProxy().Replace("http://", string.Empty);
            string[] divideProxy = proxy.Split('@');
            string[] hostAndPort = divideProxy[1].Split(':');
            string host = hostAndPort[0];
            int port = int.Parse(hostAndPort[1]);

            string[] logAndPass = divideProxy[0].Split(':');
            string log = logAndPass[0];
            string pass = logAndPass[1];

            IWebProxy webProxy = new WebProxy(host, port);
            webProxy.Credentials = new NetworkCredential(log, pass);

            return webProxy;
        }

        private List<Cookie> GetCookies(Instance instance)
        {
            var cookiesStr = instance.GetCookie(isCookieFormat: true);

            List<Cookie> cookies = new List<Cookie>();

            if (string.IsNullOrEmpty(cookiesStr))
                return cookies;

            foreach (string cookieStr in cookiesStr.Split(';'))
            {
                string[] keyAndValue = cookieStr.Split('=');
                string key = keyAndValue[0].Trim();
                string value = keyAndValue[1].Trim();
                Cookie cookie = new Cookie(key, value, "/", _cookieDomain);
                cookies.Add(cookie);
            }

            return cookies;
        }
    }
}
