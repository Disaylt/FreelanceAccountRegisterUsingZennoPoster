using HttpManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AccountRegisterApplication.RegisterServices.WB
{
    //сделать абстрактный класс котоырй из инстанса берет настройки для чттп сендера
    internal class WbBuyerHttpManager
    {
        private readonly IHttpSender _httpSender;

        public WbBuyerHttpManager()
        {
            _httpSender = new HttpSender(new HttpSettings());
        }

        public void SetName(string name)
        {
            string url = "https://www.wildberries.ru/webapi/personalinfo/fio";
            HttpContent content = JsonContent.Create(new { firstName = name });
            _httpSender.Send(new HttpMethod("PATCH"), url, content);
        }

        public void SetGender(string gender)
        {
            string url = "https://www.wildberries.ru/webapi/personalinfo/sex";
            Dictionary<string, string> form = new Dictionary<string, string>
            {
                {"sex", gender }
            };
            HttpContent content = new FormUrlEncodedContent(form);
            _httpSender.Send(new HttpMethod("PATCH"), url, content);
        }
    }
}
