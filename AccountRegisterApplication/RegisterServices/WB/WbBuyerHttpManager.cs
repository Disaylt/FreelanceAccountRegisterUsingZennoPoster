using AccountRegisterApplication.Models.WbBuyer;
using AccountRegisterApplication.RegisterServices.General;
using Global.ZennoLab.Json.Linq;
using HttpManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ZennoLab.CommandCenter;
using ZennoLab.InterfacesLibrary.ProjectModel;

namespace AccountRegisterApplication.RegisterServices.WB
{
    internal class WbBuyerHttpManager : InstanceHttpManager
    {
        private static readonly string _domain = "wildberries.ru";

        public WbBuyerHttpManager(Instance instance, IZennoPosterProjectModel project) : base(instance, _domain)
        {
            SetWbHeaders(project);
        }

        public void SetName(string name)
        {
            string url = "https://www.wildberries.ru/webapi/personalinfo/fio";
            HttpContent content = JsonContent.Create(new { firstName = name });
            Send(new HttpMethod("PATCH"), url, content);
        }

        public void SetGender(string gender)
        {
            string url = "https://www.wildberries.ru/webapi/personalinfo/sex";
            Dictionary<string, string> form = new Dictionary<string, string>
            {
                {"sex", gender }
            };
            HttpContent content = new FormUrlEncodedContent(form);
            Send(new HttpMethod("PATCH"), url, content);
        }

        public PersonalInfoModel GetPersonalInfo()
        {
            string url = "https://www.wildberries.ru/webapi/personalinfo";
            HttpResponseMessage responseMessage = Send(HttpMethod.Post, url);
            string content = responseMessage.Content.ReadAsStringAsync().Result;
            PersonalInfoModel personalInfo = JToken.Parse(content).ToObject<PersonalInfoModel>();

            return personalInfo;
        }

        private void SetWbHeaders(IZennoPosterProjectModel project)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>
            {
                {"User-Agent", project.Profile.UserAgent },
                { "Accept", "*/*" },
                { "Accept-Languag", "ru-RU,ru;q=0.8,en-US;q=0.5,en;q=0.3" },
                { "Accept-Encoding", "UTF-8" },
                { "x-requested-with", "XMLHttpRequest" },
                { "x-spa-version", "9.3.73" },
                { "Origin", "https://www.wildberries.ru" },
                { "Connection", "keep-alive" },
                { "Sec-Fetch-Dest", "empty" },
                { "Sec-Fetch-Mode", "cors" },
                { "Sec-Fetch-Site", "same-origin" },
                { "Pragma", "no-cache" },
                { "Cache-Control", "no-cache" },
                { "TE", "trailers"}
            };
            SetHeaders(headers);
        }
    }
}
