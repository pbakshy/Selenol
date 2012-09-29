// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System.Linq;

using FluentAssertions;

using OpenQA.Selenium;

using Selenol.Elements;

using TechTalk.SpecFlow;

namespace Selenol.FunctionalTests.Steps
{
    [Binding]
    public class LinkSteps
    {
        [When(@"I click on link with ""(.*)""")]
        public void WhenIClickOnLinkWith(string id)
        {
            GetLink(id).Click();
        }

        [Then(@"there are links with id ""(.*)""")]
        public void ThenThereAreLinksWithId(string[] ids)
        {
            Browser.Current.Links().Select(x => x.Id).Should().BeEquivalentTo(ids.AsEnumerable());
        }

        [Then(@"link with id ""(.*)"" has text ""(.*)""")]
        public void ThenLinkWithIdHasText(string id, string text)
        {
            GetLink(id).Text.Should().Be(text);
        }

        private static LinkElement GetLink(string id)
        {
            return Browser.Current.Link(By.Id(id));
        }
    }
}