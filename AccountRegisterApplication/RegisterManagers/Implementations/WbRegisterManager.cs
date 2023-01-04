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
        public WbRegisterManager(Instance instance, IZennoPosterProjectModel project, ApplicationSettings applicationSettings) : base(instance, project, applicationSettings)
        {
            _wbBrowserActions = new WbBrowserActions(instance);
        }

        public override void StartRegistration()
        {
            _wbBrowserActions.LoadRegisterPage();
            WritePhoneNumber();
            _wbBrowserActions.ClickForGettingCode();
            WriteCaptchaCode();
            _wbBrowserActions.ClickToContunueAfterWriteCode();

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
            //string phoneNumber = PhoneNumberManager.GetNumber()
            //    .Substring(2);
            string phoneNumber = "9041289071";
            _wbBrowserActions.WritePhoneNumber(phoneNumber);
        }
    }
}
