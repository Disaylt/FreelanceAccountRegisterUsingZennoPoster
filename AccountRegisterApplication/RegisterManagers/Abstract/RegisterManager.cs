using AccountRegisterApplication.Models.AppSettings;
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
        public RegisterManager(Instance instance, IZennoPosterProjectModel project, ApplicationSettings settings)
        {
            Instance = instance;
            Project = project;
            Settings = settings;
        }

        protected Instance Instance { get; }
        protected IZennoPosterProjectModel Project { get; }
        protected ApplicationSettings Settings { get; }

        public abstract void StartRegistration();
    }
}
