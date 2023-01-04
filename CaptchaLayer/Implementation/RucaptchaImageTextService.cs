using CaptchaLayer.Abstract;
using CaptchaLayer.Models;
using HttpManager;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CaptchaLayer.Implementation
{
    public class RucaptchaImageTextService : ICaptchaService<string, string, RuCaptchaResult>
    {
        private readonly IHttpSender _httpSender;
        private readonly string _token;
        private readonly int _maxAttempts;
        private int _currentAttempt;

        public RucaptchaImageTextService(string token, int maxAttempts = 5)
        {
            _token = token;
            _maxAttempts = maxAttempts;
            _currentAttempt = 0;
            _httpSender = new HttpSender(new HttpSettings());
        }

        public string Get(RuCaptchaResult serviceResult)
        {
            string url = $"http://rucaptcha.com/res.php?key={_token}&action=get&id={serviceResult.Request}&json=1";
            int maxAttempts = 5;
            int timeoutSec = 7;

            for(int attempt = 1; attempt < maxAttempts; attempt++)
            {
                HttpResponseMessage response = _httpSender.Send(HttpMethod.Get, url);
                string responseContent = response.Content.ReadAsStringAsync().Result;
                RuCaptchaResult ruCaptchaResult = JObject.Parse(responseContent).ToObject<RuCaptchaResult>();

                if (ruCaptchaResult.Status == 1)
                    return ruCaptchaResult.Request;

                Thread.Sleep(1000 * timeoutSec);
            }

            throw new NullReferenceException("Failed to get captcha code.");
        }

        public RuCaptchaResult Send(string base64)
        {
            _currentAttempt += 1;
            if (_currentAttempt >= _maxAttempts)
                throw new Exception($"Too many captcha request. Current attempt - {_currentAttempt}, max attempts - {_maxAttempts}");

            string url = "http://rucaptcha.com/in.php";
            HttpContent content = GetCaptchaRequestBody(base64);
            HttpResponseMessage response = _httpSender.Send(HttpMethod.Post, url, content);
            string responseContent = response.Content.ReadAsStringAsync().Result;
            RuCaptchaResult ruCaptchaResult = JObject.Parse(responseContent).ToObject<RuCaptchaResult>();

            return ruCaptchaResult;
        }

        private HttpContent GetCaptchaRequestBody(string base64)
        {
            Dictionary<string, string> body = new Dictionary<string, string>
            {
                {"body", base64 },
                {"json", "1" },
                {"key", _token },
                {"method", "base64" }
            };
            HttpContent content = new FormUrlEncodedContent(body);

            return content;
        }
    }
}
