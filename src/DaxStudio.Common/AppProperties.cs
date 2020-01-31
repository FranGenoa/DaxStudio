﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaxStudio.Common
{
    public static class AppProperties
    {
        internal const string LoggingEnabledByHotKey = "LoggingEnabledByHotKey";
        internal const string LoggingEnabledByCommandLine = "LoggingEnabledByCommandLine";
        internal const string PortNumber = "PortNumber";
        internal const string FileName = "FileName";
        internal const string CrashTest = "CrashTest";
        internal const string Database = "Database";
        internal const string Server = "Server";

        public static CmdLineArgs _args;
        public static CmdLineArgs Args(this System.Windows.Application app)
        {
            return new CmdLineArgs(app);
            //if (_args == null) _args = new CmdLineArgs(app);
            //return _args;
        }
    }
}
