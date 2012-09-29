// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System.Linq;

using FluentAssertions;

using OpenQA.Selenium;

using Selenol.Elements;

using TechTalk.SpecFlow;

namespace Selenol.FunctionalTests.Steps
{
    [Binding]
    public class ButtonSteps
    {
        [When(@"I click on button with ""(.*)""")]
        public void WhenIClickOnButtonWith(string id)
        {
            GetButton(id).Click();
        }

        [Then(@"there are buttons with id ""(.*)""")]
        public void ThenThereAreButtonsWithId(string[] ids)
        {
            Browser.Current.Buttons().Select(x => x.Id).Should().BeEquivalentTo(ids.AsEnumerable());
        }

        [Then(@"button with id ""(.*)"" has text ""(.*)""")]
        public void ThenButtonWithIdHasText(string id, string text)
        {
            GetButton(id).Text.Should().Be(text);
        }

        private static ButtonElement GetButton(string id)
        {
            return Browser.Current.Button(By.Id(id));
        }
    }
}