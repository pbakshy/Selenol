// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using OpenQA.Selenium.Firefox;

using Selenol.FunctionalTests.WebServer;

using TechTalk.SpecFlow;

namespace Selenol.FunctionalTests
{
    [Binding]
    public class TestRunHooks
    {
        private static SimpleWebServer webServer;

        [BeforeTestRun]
        public static void BeforeScenario()
        {
            webServer = new SimpleWebServer(Configuration.ServerPort);
            Browser.Current = new FirefoxDriver();
        }

        [AfterScenario]
        public static void AfterScenario()
        {
            var driver = Browser.Current;
            if (driver == null)
            {
                return;
            }

            foreach (var windowHandle in driver.WindowHandles)
            {
                driver.SwitchTo().Window(windowHandle).Close();
            }

            webServer.Dispose();
        }
    }
}
