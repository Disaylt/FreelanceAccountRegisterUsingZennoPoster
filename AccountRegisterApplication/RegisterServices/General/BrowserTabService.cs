using System;
using System.Collections.Generic;
using System.IO;
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

        public HtmlElement GetHtmlElement(string xPathElement)
        {
            HtmlElement htmlElement = _instance.ActiveTab.GetDocumentByAddress("0").FindElementByXPath(xPathElement, 0);

            return htmlElement;
        }

        public void Click(string xPathElement)
        {
            HtmlElement htmlElement = GetHtmlElement(xPathElement);
            htmlElement.Click();
        }

        public void InputText(string xPathElement, string inputText)
        {
            HtmlElement htmlElement = GetHtmlElement(xPathElement);
            htmlElement.SetValue(inputText, _instance.EmulationLevel, true);
        }

        public bool WaitElement(string xpath, int waitTimeInSecond)
        {
            for(int i = 0; i < waitTimeInSecond; i++)
            {
                HtmlElement htmlElement = GetHtmlElement(xpath);
                if (!htmlElement.IsNull && !htmlElement.IsVoid)
                {
                    return true;
                }
                Thread.Sleep(1000);
            }

            return false;
        }
    }
}
