﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.EmailConfig
{
    public class EmailConfiguration
    {
        public string Host { get; set; }
        public int Port { get; set; } 
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
