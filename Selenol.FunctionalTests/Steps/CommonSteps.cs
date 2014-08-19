// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using FluentAssertions;

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

        [Then(@"I see an alert with text ""(.*)""")]
        public void ThenISeeAnAlertWithText(string text)
        {
            var alert = Browser.Current.SwitchTo().Alert();
            var actualText = alert.Text;
            alert.Accept();
            actualText.Should().Be(text);
        }

        [Then(@"I will navigate to page ""(.*)""")]
        public void ThenIWillNavigateToPage(string page)
        {
            var url = PageUrlPattern.FInv(Configuration.ServerPort, page);
            Browser.Current.Url.Should().StartWith(url);
        }
    }
}