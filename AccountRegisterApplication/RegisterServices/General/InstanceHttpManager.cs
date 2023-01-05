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
        private Instance _instance;
        private string _cookieDomain;

        public InstanceHttpManager(Instance instance, string cookieDomain)
        {
            _instance = instance;
            _cookieDomain = cookieDomain;

            HttpSettings httpSettings = GetStartSettings(instance);
            HttpSender = new HttpSender(httpSettings);
        }

        public IHttpSender HttpSender { get; private set; }

        public void UpdateCookies()
        {
            List<Cookie> cookies = GetCookies(_instance);
            System.Net.CookieContainer cookieContainer = new System.Net.CookieContainer(200, 200, 4096);
            foreach (var cookie in cookies)
            {
                cookieContainer.Add(cookie);
            }
            HttpSender.HttpClientHandler.CookieContainer = cookieContainer;
        }

        private HttpSettings GetStartSettings(Instance instance)
        {
            HttpSettings httpSettings = new HttpSettings
            {
                Cookies = GetCookies(instance),
                Proxy = GetProxy(instance),
                HttpVersion = new Version(2, 0)
            };

            return httpSettings;
        }

        private IWebProxy GetProxy(Instance instance)
        {
            string proxy = instance.GetProxy();
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
