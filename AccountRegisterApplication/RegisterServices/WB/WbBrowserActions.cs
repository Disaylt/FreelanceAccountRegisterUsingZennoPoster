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

        public WbBrowserActions(Instance instance)
        {
            _instance = instance;
        }

        public void LoadRegisterPage()
        {
            _instance.ActiveTab.Navigate("https://www.wildberries.ru/security/login");
        }
    }
}
