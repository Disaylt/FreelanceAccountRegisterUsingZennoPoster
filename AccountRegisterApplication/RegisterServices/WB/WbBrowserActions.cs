using AccountRegisterApplication.RegisterServices.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZennoLab.CommandCenter;

namespace AccountRegisterApplication.RegisterServices.WB
{
    internal class WbBrowserActions
    {
        private readonly Instance _instance;
        private readonly BrowserTabService _browserTabService;

        public WbBrowserActions(Instance instance)
        {
            _instance = instance;
            _browserTabService = new BrowserTabService(instance);
        }

        public void LoadRegisterPage()
        {
            _instance.ActiveTab.Navigate("https://www.wildberries.ru/security/login");
        }

        public void WritePhoneNumber(string phoneNumberWithoutCode)
        {
            string elementXPath = "//input[@inputmode='tel' and @class='input-item']";
            bool isExists = _browserTabService.WaitElement(elementXPath, 20);

            if (!isExists)
                throw new NullReferenceException($"{elementXPath} not found");

            _browserTabService.InputText(elementXPath, phoneNumberWithoutCode);
        }
    }
}
