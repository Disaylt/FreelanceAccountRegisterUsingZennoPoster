using AccountRegisterApplication.Models.AppSettings;
using AccountRegisterApplication.Models.WbBuyer;
using Global.ZennoLab.Json.Linq;
using HttpManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace AccountRegisterApplication.RegisterServices.MpSnake
{
    internal class MpSnakeApiManager
    {
        private readonly IHttpSender _httpSender;

        public MpSnakeApiManager(ApplicationMpSnakeSettings applicationMpSnakeSettings)
        {
            HttpSettings httpSettings = CreateApiSettings(applicationMpSnakeSettings);
            _httpSender = new HttpSender(httpSettings);
        }

        public WbAccountModel SendAccountToDb(WbAccountModel wbAccount)
        {
            string url = "https://mp-snake.ru/api/v1/wbbuyeraccounts/add";
            HttpContent httpContent = JsonContent.Create(wbAccount);
            HttpResponseMessage httpResponse = _httpSender.Send(HttpMethod.Post, url, httpContent);
            string content = httpResponse.Content.ReadAsStringAsync().Result;
            WbAccountModel resultAccount = JToken.Parse(content).ToObject<WbAccountModel>();

            return resultAccount;
        }

        private HttpSettings CreateApiSettings(ApplicationMpSnakeSettings applicationMpSnakeSettings)
        {
            HttpSettings httpSettings = new HttpSettings
            {
                Headers = new Dictionary<string, string>
                {
                    { "Authorization", $"Bearer {applicationMpSnakeSettings.Token}" }
                }
            };

            return httpSettings;
        }
    }
}
