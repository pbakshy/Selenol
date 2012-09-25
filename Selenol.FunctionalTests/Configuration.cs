// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System.Configuration;

namespace Selenol.FunctionalTests
{
    public class Configuration
    {
        private const string ServerPortKey = "serverPort";

        public static int ServerPort
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings[ServerPortKey]);
            }
        }
    }
}