using AccountRegisterApplication.RegisterManager.Abstract;
using AccountRegisterApplication.RegisterServices.WB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZennoLab.CommandCenter;
using ZennoLab.InterfacesLibrary.ProjectModel;

namespace AccountRegisterApplication.RegisterManager.Implementations
{
    internal class WbRegisterManager : IRegisterManager
    {
        private readonly WbBrowserActions _wbBrowserActions;
        public WbRegisterManager(Instance instance, IZennoPosterProjectModel project)
        {
            _wbBrowserActions = new WbBrowserActions(instance);
        }

        public void StartRegistration()
        {
            _wbBrowserActions.LoadRegisterPage();
        }
    }
}
