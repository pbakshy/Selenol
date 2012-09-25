// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using Selenol.Extensions;

using TechTalk.SpecFlow;

namespace Selenol.FunctionalTests.Steps
{
    [Binding]
    public class CommonSteps
    {
        private const string PageUrlPattern = "http://localhost:{0}/{1}.html";
        
        [Given(@"that I am viewing ""(.*)"" page")]
        public void GivenThatIAmViewingPage(string pageName)
        {
            var url = PageUrlPattern.FInv(Configuration.ServerPort, pageName);
            Browser.Current.Navigate().GoToUrl(url);
        } 
    }
}