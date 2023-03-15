using AccountRegisterApplication.Models.AppSettings;
using AccountRegisterApplication.Models.WbBuyer;
using AccountRegisterApplication.RegisterManagers.Abstract;
using AccountRegisterApplication.RegisterServices.MpSnake;
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
    internal class WbRegisterManager : RegisterManager<WbAccountModel>
    {
        private readonly WbBrowserActions _wbBrowserActions;
        private readonly WbBuyerHttpManager _wbBuyerHttpManager;
        private readonly MpSnakeApiManager _mpSnakeApiManager;

        public WbRegisterManager(Instance instance, IZennoPosterProjectModel project, ApplicationSettings applicationSettings) : base(instance, project, applicationSettings)
        {
            _wbBrowserActions = new WbBrowserActions(instance);
            _wbBuyerHttpManager = new WbBuyerHttpManager(instance, project);
            _mpSnakeApiManager = new MpSnakeApiManager(applicationSettings.ApplicationMpSnakeSettings);
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
                SetPersonalInfo();
                CheckPersonalInfo();
                ConfrimAccountAndDeleteAllSessions();

                AddAdditionalCookies();
                
                BuildAccount();
                SendAccount();
                SaveProfileAsFile();
            }
            finally
            {
                PhoneNumberManager.Complete();
            }
        }

        private void ConfrimAccountAndDeleteAllSessions()
        {
            _wbBuyerHttpManager.SendCodeForCloseSessions();
            string code = PhoneNumberManager.GetMessage();
            _wbBuyerHttpManager.ConfirmAccountRights(code);
            int totalSessions = _wbBuyerHttpManager.GetSessions().Count;

            if(totalSessions > 1)
            {
                _wbBuyerHttpManager.CloseOtherSessions();
            }
        }

        private void AddAdditionalCookies()
        {
            _wbBrowserActions.RefreshPage();
            Thread.Sleep(10 * 1000);
        }

        private void SaveProfileAsFile()
        {
            if(Account.Id != 0)
            {
                string savePath = $@"{Settings.ProfilesSaveFolder}\{Account.PhoneNumber}_{Account.Id}";
                Project.Profile.Save(savePath, saveLocalStorage: true);
            }
            else
            {
                throw new Exception("Bad account id");
            }
        }

        private void SendAccount()
        {
            WbAccountModel wbAccount = _mpSnakeApiManager.SendAccountToDb(Account);
            Account.Id = wbAccount.Id;
        }

        private void BuildAccount()
        {
            Account.Cookies = Instance.GetCookie(isCookieFormat: true);
            Account.PhoneNumber = PhoneNumberManager.GetNumber();
            Account.Gender = Settings.ApplicationPersonalInfoSettings.Gender;
            Account.NormalizedGender = Settings.ApplicationPersonalInfoSettings.Gender.ToUpper();
            Account.UserAgent = Project.Profile.UserAgent;
            Account.IsActive = true;
            Account.Name = UserFirstName;
            Account.RegisterProxy = Proxy;
        }

        private void CheckPersonalInfo()
        {
            Thread.Sleep(1000);
            PersonalInfoModel personalInfoModel = _wbBuyerHttpManager.GetPersonalInfo();
            if (personalInfoModel.Value.FirstName != UserFirstName || personalInfoModel.Value.GenderE != Settings.ApplicationPersonalInfoSettings.Gender)
                throw new Exception("Bad personal info.");
        }

        private void SetPersonalInfo()
        {
            _wbBuyerHttpManager.SetGender(Settings.ApplicationPersonalInfoSettings.Gender);
            _wbBuyerHttpManager.SetName(UserFirstName);
        }

        private string GetCode()
        {
            string message = PhoneNumberManager.GetMessage();
            string fourNumberCode;
            if(message.Length > 6)
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
            Thread.Sleep(3 * 1000);
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
