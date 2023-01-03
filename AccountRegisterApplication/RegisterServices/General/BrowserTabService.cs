using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZennoLab.CommandCenter;

namespace AccountRegisterApplication.RegisterServices.General
{
    internal class BrowserTabService
    {
        private readonly Instance _instance;

        public BrowserTabService(Instance instance)
        {
            _instance = instance;
        }

        public bool WaitElement(string xpath, int waitTimeInSecond)
        {
            for(int i = 0; i < waitTimeInSecond; i++)
            {
                HtmlElement htmlElement = _instance.ActiveTab.GetDocumentByAddress("0").FindElementByXPath(xpath, 0);
                if(!htmlElement.IsNull && !htmlElement.IsVoid)
                {
                    return true;
                }
                Thread.Sleep(1000);
            }

            return false;
        }
    }
}
