using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpManager
{
    public class HttpSender : IHttpSender, IDisposable
    {
        private readonly HttpSettings _httpSettings;

        public HttpSender(HttpSettings httpOption)
        {
            _httpSettings = httpOption;
            HttpClientHandler = new HttpClientHandler();

            SetProxy();
            SetCookies();
        }

        public HttpClientHandler HttpClientHandler { get; }

        public HttpResponseMessage Send(HttpMethod method, string url)
        {
            HttpClient httpClient = new HttpClient(HttpClientHandler);
            using (HttpRequestMessage request = new HttpRequestMessage(method, url))
            {
                SetHeaders(request);
                request.Version = _httpSettings.HttpVersion;
                HttpResponseMessage httpResponse = httpClient.SendAsync(request).Result;

                return httpResponse;
            }
        }

        public HttpResponseMessage Send(HttpMethod method, string url, HttpContent httpContent)
        {
            HttpClient httpClient = new HttpClient(HttpClientHandler);
            using (HttpRequestMessage request = new HttpRequestMessage(method, url))
            {
                SetHeaders(request);
                request.Version = _httpSettings.HttpVersion;
                request.Content = httpContent;

                HttpResponseMessage httpResponse = httpClient.SendAsync(request).Result;

                return httpResponse;
            }
        }

        private void SetHeaders(HttpRequestMessage httpRequestMessage)
        {
            foreach (var headerPair in _httpSettings.Headers)
            {
                httpRequestMessage.Headers.TryAddWithoutValidation(headerPair.Key, headerPair.Value);
            }
        }

        private void SetCookies()
        {
            HttpClientHandler.UseCookies = true;
            HttpClientHandler.CookieContainer = new CookieContainer(200, 200, 4096);
            foreach (var cookie in _httpSettings.Cookies)
            {
                HttpClientHandler.CookieContainer.Add(cookie);
            }
        }

        private void SetProxy()
        {
            if (_httpSettings.Proxy != null)
            {
                HttpClientHandler.UseProxy = true;
                HttpClientHandler.Proxy = _httpSettings.Proxy;
            }
        }

        public void Dispose()
        {
            HttpClientHandler.Dispose();
        }
    }
}
