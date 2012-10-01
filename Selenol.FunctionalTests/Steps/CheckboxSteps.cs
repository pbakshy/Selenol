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
        private CheckboxElement checkbox;

        [When(@"I look at a checkbox with id ""(.*)""")]
        public void WhenILookAtACheckboxWithId(string id)
        {
            this.checkbox = Browser.Current.Checkbox(By.Id(id));
        }

        [When(@"I check the checkbox")]
        public void WhenICheckedCheckboxWithId()
        {
            this.checkbox.Check();
        }

        [When(@"I uncheck the checkbox")]
        public void WhenIUncheckCheckboxWithId()
        {
            this.checkbox.Uncheck();
        }

        [Then(@"the checkbox has value ""(.*)""")]
        public void ThenChecboxWithHasValue(bool value)
        {
            this.checkbox.IsChecked.Should().Be(value);
        }

        [Then(@"there are checkboxes with id ""(.*)""")]
        public void ThenThereAreCheckboxesWithId(string[] ids)
        {
            Browser.Current.Checkboxes().Select(x => x.Id).Should().BeEquivalentTo(ids.AsEnumerable());
        }
    }
}