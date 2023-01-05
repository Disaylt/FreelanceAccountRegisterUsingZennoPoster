using AccountRegisterApplication.Models.AppSettings;
using AccountRegisterApplication.RegisterManagers.Abstract;
using AccountRegisterApplication.RegisterServices.WB;
using CaptchaLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZennoLab.CommandCenter;
using ZennoLab.InterfacesLibrary.ProjectModel;

namespace AccountRegisterApplication.RegisterManagers.Implementations
{
    internal class WbRegisterManager : RegisterManager
    {
        private readonly WbBrowserActions _wbBrowserActions;
        private readonly WbBuyerHttpManager _wbBuyerHttpManager;

        public WbRegisterManager(Instance instance, IZennoPosterProjectModel project, ApplicationSettings applicationSettings) : base(instance, project, applicationSettings)
        {
            _wbBrowserActions = new WbBrowserActions(instance);
            _wbBuyerHttpManager = new WbBuyerHttpManager(instance, project);
        }

        public override void StartRegistration()
        {
            try
            {
                _wbBrowserActions.LoadRegisterPage();
                WritePhoneNumber();
                _wbBrowserActions.ClickForGettingCode();
                WriteCaptchaCode();
                _wbBrowserActions.ClickToContunueAfterWriteCaptchaCode();
                string code = GetCode();
                _wbBrowserActions.WriteSmsCode(code);
                Thread.Sleep(3 * 1000);
                _wbBrowserActions.GoToProfilePage();
            }
            finally
            {
                PhoneNumberManager.Complete();
            }
        }



        private string GetCode()
        {
            string message = PhoneNumberManager.GetMessage();
            string fourNumberCode;
            if(message.Length > 4)
            {
                fourNumberCode = message.Substring(message.Length - 4);
            }
            else
            {
                fourNumberCode = message;
            }

            return fourNumberCode;
        }

        private void WriteCaptchaCode()
        {
            string base64Captcha = _wbBrowserActions.GetRegisterCaptchaAsBase64();
            RuCaptchaResult ruCaptchaResult = CaptchaService.Send(base64Captcha);
            Thread.Sleep(10 * 1000);
            string code = CaptchaService.Get(ruCaptchaResult);
            _wbBrowserActions.InputCaptchaCode(code);
        }

        private void WritePhoneNumber()
        {
            string phoneNumber = PhoneNumberManager.GetNumber()
                .Substring(2);
            _wbBrowserActions.WritePhoneNumber(phoneNumber);
        }
    }
}
