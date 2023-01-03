using AccountRegisterApplication.Models.AppSettings;
using PhoneNumbersLayer.Abstarct;
using PhoneNumbersLayer.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountRegisterApplication.RegisterServices.PhoneNumber
{
    internal class PhoneNumberManagerBuilder
    {
        private readonly AplicationSmsManagerSettings _aplicationSmsManagerSettings;

        public PhoneNumberManagerBuilder(AplicationSmsManagerSettings aplicationSmsManagerSettings)
        {
            _aplicationSmsManagerSettings = aplicationSmsManagerSettings;
        }

        public IPhoneNumberManager Get()
        {
            IPhoneNumberManager phoneNumberManager;
            switch (_aplicationSmsManagerSettings.ServiceName)
            {
                case "onlineSim":
                    phoneNumberManager = new OnlineSimPhoneNumberManager(_aplicationSmsManagerSettings.PhoneNumbersSettings);
                    break;
                default:
                    throw new NullReferenceException($"{_aplicationSmsManagerSettings.ServiceName} not found.");
            }

            return phoneNumberManager;
        }
    }
}
