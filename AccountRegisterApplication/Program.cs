﻿using AccountRegisterApplication.Models.AppSettings;
using AccountRegisterApplication.RegisterManagers.Abstract;
using AccountRegisterApplication.RegisterManagers.Implementations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Resources;
using System.Text;
using ZennoLab.CommandCenter;
using ZennoLab.Emulation;
using ZennoLab.InterfacesLibrary.ProjectModel;
using ZennoLab.InterfacesLibrary.ProjectModel.Enums;

namespace AccountRegisterApplication
{
    /// <summary>
    /// Класс для запуска выполнения скрипта
    /// </summary>
    public class Program : IZennoExternalCode
    {
        /// <summary>
        /// Метод для запуска выполнения скрипта
        /// </summary>
        /// <param name="instance">Объект инстанса выделеный для данного скрипта</param>
        /// <param name="project">Объект проекта выделеный для данного скрипта</param>
        /// <returns>Код выполнения скрипта</returns>		
        public int Execute(Instance instance, IZennoPosterProjectModel project)
        {
            int executionResult = 0;

            ApplicationSettings applicationSettings = new ApplicationSettings
            {
                Site = "wb",
                ProxySettings = new ApplicationProxySettings
                {
                    FileName = "proxiesList.txt",
                    LoaderType = "txt"
                }
            };

            RegisterManager registerManager = new WbRegisterManager(instance, project, applicationSettings);
            registerManager.StartRegistration();

            return executionResult;
        }
    }
}