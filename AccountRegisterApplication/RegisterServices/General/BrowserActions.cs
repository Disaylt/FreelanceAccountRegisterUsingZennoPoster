using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZennoLab.CommandCenter;

namespace AccountRegisterApplication.RegisterServices.General
{
    internal class BrowserActions
    {
        
        public BrowserActions(Instance instance)
        {
            Instance = instance;
            BrowserTabService = new BrowserTabService(instance);
        }

        public void RefreshPage()
        {
            string url = Instance.ActiveTab.URL;
            Instance.ActiveTab.Navigate(url);
        }

        protected Instance Instance { get; }
        protected BrowserTabService BrowserTabService { get; }
    }
}
