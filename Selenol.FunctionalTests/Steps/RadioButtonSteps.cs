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
        private RadioButtonElement radioButton;

        [When(@"I check the radio button")]
        public void WhenISelectRadionButtonWith()
        {
            this.radioButton.Check();
        }

        [When(@"I look at a radio button with id ""(.*)""")]
        public void WhenILookAtARadioButtonWithId(string id)
        {
            this.radioButton = Browser.Current.RadioButton(By.Id(id));
        }

        [Then(@"the radio button has value ""(.*)""")]
        public void ThenRadioButtonWithIdHasValue(bool value)
        {
            this.radioButton.IsChecked.Should().Be(value);
        }

        [Then(@"there are radio buttons with id ""(.*)""")]
        public void ThenThereAreRadioButtonsWithId(string[] ids)
        {
            Browser.Current.RadioButtons().Select(x => x.Id).Should().BeEquivalentTo(ids.AsEnumerable());
        }
    }
}