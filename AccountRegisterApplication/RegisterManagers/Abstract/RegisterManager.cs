using AccountRegisterApplication.Models.AppSettings;
using AccountRegisterApplication.RegisterServices.PhoneNumber;
using AccountRegisterApplication.RegisterServices.Proxy;
using PhoneNumbersLayer.Abstarct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZennoLab.CommandCenter;
using ZennoLab.InterfacesLibrary.ProjectModel;

namespace AccountRegisterApplication.RegisterManagers.Abstract
{
    internal abstract class RegisterManager
    {
        private readonly ProxyLoader _proxyLoader;
        public RegisterManager(Instance instance, IZennoPosterProjectModel project, ApplicationSettings settings)
        {
            Instance = instance;
            Project = project;
            Settings = settings;

            _proxyLoader = new ProxyLoader(settings.ProxySettings);

            SetManagerProperties();
            SetProxy();

            PhoneNumberManager = GetPhoneNumberManager(settings);
        }

        protected Instance Instance { get; }
        protected IZennoPosterProjectModel Project { get; }
        protected IPhoneNumberManager PhoneNumberManager { get; }
        protected ApplicationSettings Settings { get; }

        public string Proxy { get; private set; }

        public abstract void StartRegistration();

        private void SetManagerProperties()
        {
            Proxy = _proxyLoader.LoadProxy();
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
