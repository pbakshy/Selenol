// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System.Linq;

using FluentAssertions;

using OpenQA.Selenium;

using Selenol.Elements;

using TechTalk.SpecFlow;

namespace Selenol.FunctionalTests.Steps
{
    [Binding]
    public class SelectSteps
    {
        private SelectElement select;

        [When(@"I look at a select with id ""(.*)""")]
        public void WhenILookAtASelectWithId(string id)
        {
            this.select = Browser.Current.Select(By.Id(id));
        }

        [When(@"I select option with text ""(.*)"" in the select")]
        public void WhenISelectOptionWithInSelectWith(string text)
        {
            this.select.SelectOptionByText(text);
        }

        [When(@"I select option with value ""(.*)"" in the select")]
        public void WhenISelectOptionWithValueInSelectWith(string value)
        {
            this.select.SelectOptionByValue(value);
        }

        [Then(@"in the select selected text is ""(.*)""")]
        public void ThenInSelectWithIdSelectedTextIs(string text)
        {
            this.select.SelectedOption.Text.Should().Be(text);
        }

        [Then(@"in the select selected value is ""(.*)""")]
        public void ThenInSelectWithIdSelectedValueIs(string value)
        {
            this.select.SelectedOption.Value.Should().Be(value);
        }

        [Then(@"there are selects with id ""(.*)""")]
        public void ThenThereAreSelectsWithId(string[] ids)
        {
            Browser.Current.Selects().Select(x => x.Id).Should().BeEquivalentTo(ids.AsEnumerable());
        }
    }
}