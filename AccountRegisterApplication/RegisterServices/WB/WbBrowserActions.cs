using AccountRegisterApplication.RegisterServices.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZennoLab.CommandCenter;

namespace AccountRegisterApplication.RegisterServices.WB
{
    internal class WbBrowserActions : BrowserActions
    {
        public WbBrowserActions(Instance instance) : base(instance)
        {
        }

        public void LoadRegisterPage()
        {
            Instance.ActiveTab.Navigate("https://www.wildberries.ru/security/login");
        }

        public void GoToProfilePage()
        {
            Instance.ActiveTab.Navigate("https://www.wildberries.ru/lk/details");
            string elementXPath = "//div[@class='personal-data-page']";
            WaitElement(elementXPath);
        }

        public void WriteSmsCode(string code)
        {
            string elementXPath = "//input[@inputmode='tel' and @class='j-input-confirm-code val-msg']";
            WaitElement(elementXPath);

            BrowserTabService.InputText(elementXPath, code);
        }

        public void ClickToContunueAfterWriteCaptchaCode()
        {
            string elementXPath = "//button[@class='login__btn btn-main-lg']";
            WaitElement(elementXPath);
            BrowserTabService.Click(elementXPath);
        }

        public void InputCaptchaCode(string code)
        {
            string elementXPath = "//input[@id='smsCaptchaCode']";
            WaitElement(elementXPath);

            BrowserTabService.InputText(elementXPath, code);
        }

        public string GetRegisterCaptchaAsBase64()
        {
            string elementXPath = "//img[@class='form-block__captcha-img']";
            WaitElement(elementXPath);

            HtmlElement htmlElement = BrowserTabService.GetHtmlElement(elementXPath);
            string srcValue = htmlElement.GetAttribute("src");
            string base64 = srcValue.Replace("data:image/jpeg;base64,", string.Empty);

            return base64;
        }

        public void ClickForGettingCode()
        {
            string elementXPath = "//button[@id='requestCode']";
            WaitElement(elementXPath);
            BrowserTabService.Click(elementXPath);

        }

        public void WritePhoneNumber(string phoneNumberWithoutCode)
        {
            string elementXPath = "//input[@inputmode='tel' and @class='input-item']";
            WaitElement(elementXPath);

            BrowserTabService.InputText(elementXPath, phoneNumberWithoutCode);
        }

        private void WaitElement(string xPath)
        {
            bool isExists = BrowserTabService.WaitElement(xPath, 20);

            if (!isExists)
                throw new NullReferenceException($"{xPath} not found");
        }
    }
}
