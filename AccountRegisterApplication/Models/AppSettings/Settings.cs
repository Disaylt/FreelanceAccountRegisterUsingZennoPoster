﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountRegisterApplication.Models.AppSettings
{
    internal class Settings
    {
        public string Site { get; set; }
        public ProxySettings ProxySettings { get; set; }
    }
}
