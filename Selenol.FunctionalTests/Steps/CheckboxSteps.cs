// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System.Linq;

using FluentAssertions;

using OpenQA.Selenium;

using Selenol.Elements;

using TechTalk.SpecFlow;

namespace Selenol.FunctionalTests.Steps
{
    [Binding]
    public class CheckboxSteps
    {
        [When(@"I checked checkbox with id ""(.*)""")]
        public void WhenICheckedCheckboxWithId(string id)
        {
            GetCheckbox(id).Check();
        }

        [When(@"I uncheck checkbox with id ""(.*)""")]
        public void WhenIUncheckCheckboxWithId(string id)
        {
            GetCheckbox(id).Uncheck();
        }

        [Then(@"checkbox with ""(.*)"" has value ""(.*)""")]
        public void ThenChecboxWithHasValue(string id, bool value)
        {
            GetCheckbox(id).IsChecked.Should().Be(value);
        }

        [Then(@"there are checkboxes with id ""(.*)""")]
        public void ThenThereAreCheckboxesWithId(string[] ids)
        {
            Browser.Current.Checkboxes().Select(x => x.Id).Should().BeEquivalentTo(ids.AsEnumerable());
        }
 
        private static CheckboxElement GetCheckbox(string id)
        {
            return Browser.Current.Checkbox(By.Id(id));
        }
    }
}