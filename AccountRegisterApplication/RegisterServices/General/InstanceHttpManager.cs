using HttpManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZennoLab.CommandCenter;

namespace AccountRegisterApplication.RegisterServices.General
{
    internal abstract class InstanceHttpManager
    {
        private Instance _instance;
        public InstanceHttpManager(Instance instance)
        {
            _instance = instance;
        }

        public IHttpSender HttpSender { get; private set; }

        public void UpdateCookies()
        {

        }

        private 
    }
}
