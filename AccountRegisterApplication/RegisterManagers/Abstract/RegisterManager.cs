using AccountRegisterApplication.Models.AppSettings;
using AccountRegisterApplication.RegisterServices.PhoneNumber;
using AccountRegisterApplication.RegisterServices.Proxy;
using CaptchaLayer.Abstract;
using CaptchaLayer.Implementation;
using CaptchaLayer.Models;
using PhoneNumbersLayer.Abstarct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using ZennoLab.CommandCenter;
using ZennoLab.InterfacesLibrary.ProjectModel;

namespace AccountRegisterApplication.RegisterManagers.Abstract
{
    internal abstract class RegisterManager<AccountT> where AccountT : class, new()
    {
        private static readonly Random _random = new Random();
        private readonly ProxyLoader _proxyLoader;
        public RegisterManager(Instance instance, IZennoPosterProjectModel project, ApplicationSettings settings)
        {
            Instance = instance;
            Project = project;
            Settings = settings;

            _proxyLoader = new ProxyLoader(settings.ProxySettings);

            SetManagerProperties();
            SetProxy();
            SetFirstName(settings.ApplicationPersonalInfoSettings);

            Account = new AccountT();
            PhoneNumberManager = GetPhoneNumberManager(settings);
            CaptchaService = new RucaptchaImageTextService(settings.ApplicationRuCaptchaSettings.Token);
        }
        protected AccountT Account { get; }
        protected Instance Instance { get; }
        protected IZennoPosterProjectModel Project { get; }
        protected IPhoneNumberManager PhoneNumberManager { get; }
        protected ICaptchaService<string, string, RuCaptchaResult> CaptchaService { get; }
        protected ApplicationSettings Settings { get; }

        protected string UserFirstName { get; private set; }
        public string Proxy { get; private set; }

        public abstract void StartRegistration();

        private void SetManagerProperties()
        {
            Proxy = _proxyLoader.LoadProxy();
        }

        private void SetFirstName(ApplicationPersonalInfoSettings applicationPersonalInfoSettings)
        {
            if (applicationPersonalInfoSettings.Gender.ToLower() == "male")
            {
                UserFirstName = ChooseName(applicationPersonalInfoSettings.MaleNames);
            }
            else if(applicationPersonalInfoSettings.Gender.ToLower() == "female")
            {
                UserFirstName = ChooseName(applicationPersonalInfoSettings.FemaleNames);
            }
            else
            {
                throw new Exception("First name not found for current gender.");
            }
        }

        private string ChooseName(List<string> names)
        {
            int index = _random.Next(names.Count);
            string name = names[index];

            return name; ;
        }

        private IPhoneNumberManager GetPhoneNumberManager(ApplicationSettings settings)
        {
            PhoneNumberManagerBuilder phoneNumberManagerBuilder = new PhoneNumberManagerBuilder(settings.AplicationSmsManagerSettings);

            return phoneNumberManagerBuilder.Get();
        }

        private void SetProxy()
        {
            Instance.SetProxy(Proxy, emulateGeolocation: true, emulateTimezone: true, emulateWebrtc: true);
        }
    }
}
