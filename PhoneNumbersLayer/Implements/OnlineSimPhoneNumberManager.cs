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
using System.Threading;
using System.Threading.Tasks;

namespace PhoneNumbersLayer.Implements
{
    public class OnlineSimPhoneNumberManager : IPhoneNumberManager
    {
        private readonly IHttpSender _httpSender;
        private readonly PhoneNumbersSettings _phoneNumbersSettings;

        private string _lastMessage;
        private TaskModel _task;

        public OnlineSimPhoneNumberManager(PhoneNumbersSettings phoneNumbersSettings)
        {
            _httpSender = new HttpSender(new HttpSettings());
            _phoneNumbersSettings = phoneNumbersSettings;
        }

        public void Complete()
        {
            if (_task != null)
            {
                PhoneNumberStateModel state = GetState();
                int minTimeForCloseTask = 780;
                if (state.Time > minTimeForCloseTask)
                    Thread.Sleep(1000 * (state.Time - minTimeForCloseTask + 5));

                SetOkStatus();
            }
        }

        public string GetMessage()
        {
            if( _task == null)
            {
                return null;
            }

            string message = WaitMassage();

            return message;
        }

        public string GetNumber()
        {
            if(_task == null)
            {
                _task = GetTaskModel();
            }

            return _task.Number;
        }

        private string WaitMassage()
        {
            int maxAttempt = 5;
            for(int currentAttempt = 0; currentAttempt < maxAttempt; currentAttempt++)
            {
                PhoneNumberStateModel phoneNumberState = GetState();
                if (phoneNumberState.Msg != null && phoneNumberState.Msg != _lastMessage)
                {
                    _lastMessage = phoneNumberState.Msg;
                    return phoneNumberState.Msg;
                }

                Thread.Sleep(5 * 1000);
            }

            throw new NullReferenceException("Sms cod ewas not received.");
        }

        private void SetOkStatus()
        {
            string url = $"https://onlinesim.ru/api/setOperationOk.php?apikey={_phoneNumbersSettings.Token}&tzid={_task.Tzid}";
            _httpSender.Send(HttpMethod.Get, url);
        }

        private TaskModel GetTaskModel()
        {
            string url = $"https://onlinesim.ru/api/getNum.php?apikey={_phoneNumbersSettings.Token}&service={_phoneNumbersSettings.Service}&number=true";
            HttpResponseMessage httpResponseMessage = _httpSender.Send(HttpMethod.Get, url);
            string content = httpResponseMessage.Content.ReadAsStringAsync().Result;
            TaskModel task = JToken.Parse(content).ToObject<TaskModel>();

            return task;
        }

        private PhoneNumberStateModel GetState()
        {
            string url = $"https://onlinesim.ru/api/getState.php?apikey={_phoneNumbersSettings.Token}&tzid={_task.Tzid}";
            string content = _httpSender.Send(HttpMethod.Get, url)
                .Content
                .ReadAsStringAsync()
                .Result;
            List<PhoneNumberStateModel> phoneNumbersStateModel = JToken.Parse(content).ToObject<List<PhoneNumberStateModel>>();
            PhoneNumberStateModel phoneNumberStateModel = phoneNumbersStateModel.First(x=> x.Tzid == _task.Tzid);

            return phoneNumberStateModel;
        }
    }
}
