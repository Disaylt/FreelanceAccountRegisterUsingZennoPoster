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

        public void ClickToContunueAfterWriteCode()
        {
            string elementXPath = "//button[@class='login__btn btn-main-lg']";
            WaitElement(elementXPath);
            _browserTabService.Click(elementXPath);
        }

        public void InputCaptchaCode(string code)
        {
            string elementXPath = "//input[@id='smsCaptchaCode']";
            WaitElement(elementXPath);

            _browserTabService.InputText(elementXPath, code);
        }

        public string GetRegisterCaptchaAsBase64()
        {
            string elementXPath = "//img[@class='form-block__captcha-img']";
            WaitElement(elementXPath);

            HtmlElement htmlElement = _browserTabService.GetHtmlElement(elementXPath);
            string srcValue = htmlElement.GetAttribute("src");
            string base64 = srcValue.Replace("data:image/jpeg;base64,", string.Empty);

            return base64;
        }

        public void ClickForGettingCode()
        {
            string elementXPath = "//button[@id='requestCode']";
            WaitElement(elementXPath);
            _browserTabService.Click(elementXPath);

        }

        public void WritePhoneNumber(string phoneNumberWithoutCode)
        {
            string elementXPath = "//input[@inputmode='tel' and @class='input-item']";
            WaitElement(elementXPath);

            _browserTabService.InputText(elementXPath, phoneNumberWithoutCode);
        }

        private void WaitElement(string xPath)
        {
            bool isExists = _browserTabService.WaitElement(xPath, 20);

            if (!isExists)
                throw new NullReferenceException($"{xPath} not found");
        }
    }
}
