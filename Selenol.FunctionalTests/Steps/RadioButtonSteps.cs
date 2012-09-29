// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System.Linq;

using FluentAssertions;

using OpenQA.Selenium;

using Selenol.Elements;

using TechTalk.SpecFlow;

namespace Selenol.FunctionalTests.Steps
{
    [Binding]
    public class RadioButtonSteps
    {
        [When(@"I select radio button with ""(.*)""")]
        public void WhenISelectRadionButtonWith(string id)
        {
            GetRadioButton(id).Check();
        }

        [Then(@"radio button with id ""(.*)"" has value ""(.*)""")]
        public void ThenRadioButtonWithIdHasValue(string id, bool value)
        {
            GetRadioButton(id).IsChecked.Should().Be(value);
        }

        [Then(@"there are radio buttons with id ""(.*)""")]
        public void ThenThereAreRadioButtonsWithId(string[] ids)
        {
            Browser.Current.RadioButtons().Select(x => x.Id).Should().BeEquivalentTo(ids.AsEnumerable());
        }

        private static RadioButtonElement GetRadioButton(string id)
        {
            return Browser.Current.RadioButton(By.Id(id));
        }
    }
}