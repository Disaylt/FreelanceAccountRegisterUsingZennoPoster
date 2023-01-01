using HttpManager;
using Newtonsoft.Json.Linq;
using PhoneNumbersLayer.Abstarct;
using PhoneNumbersLayer.Models;
using PhoneNumbersLayer.Models.OnlineSim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PhoneNumbersLayer.Implements
{
    internal class OnlineSimPhoneNumberManager : IPhoneNumberManager
    {
        private readonly IHttpSender _httpSender;
        private readonly PhoneNumbersSettings _phoneNumbersSettings;

        private TaskModel _task;

        public OnlineSimPhoneNumberManager(PhoneNumbersSettings phoneNumbersSettings)
        {
            _httpSender = new HttpSender(new HttpSettings());
            _phoneNumbersSettings = phoneNumbersSettings;
        }

        public void Complete()
        {
            throw new NotImplementedException();
        }

        public string GetMessage()
        {
            throw new NotImplementedException();
        }

        public string GetNumber()
        {
            if(_task == null)
            {
                _task = GetTaskModel();
            }

            return _task.PhoneNumber;
        }

        private TaskModel GetTaskModel()
        {
            string url = $"https://onlinesim.ru/api/getNum.php?apikey={_phoneNumbersSettings.Token}&service={_phoneNumbersSettings.Service}&number=true";
            HttpResponseMessage httpResponseMessage = _httpSender.Send(HttpMethod.Get, url);
            string content = httpResponseMessage.Content.ReadAsStringAsync().Result;
            TaskModel task = JToken.Parse(content).ToObject<TaskModel>();

            return task;
        }
    }
}
