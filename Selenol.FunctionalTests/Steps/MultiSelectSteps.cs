// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System.Linq;

using FluentAssertions;

using OpenQA.Selenium;

using Selenol.Elements;

using TechTalk.SpecFlow;

namespace Selenol.FunctionalTests.Steps
{
    [Binding]
    public class MultiSelectSteps
    {
        private MultiSelectElement multiSelect;

        [When(@"I look at a multi select with id ""(.*)""")]
        public void WhenILookAtAMultiSelectWithId(string id)
        {
            this.multiSelect = Browser.Current.MultiSelect(By.Id(id));
        }

        [When(@"I clear selection in the multi select")]
        public void WhenIClearSelectionInMultiSelectWithId()
        {
            this.multiSelect.ClearSelection();
        }

        [When(@"I select an option with text ""(.*)"" in the multi select")]
        public void WhenISelectAnOptionWithTextInMultiSelectWithId(string text)
        {
            this.multiSelect.SelectOptionByText(text);
        }

        [When(@"I select an option with value ""(.*)"" in the multi select")]
        public void WhenISelectAnOptionWithValueInMultiSelectWithId(string value)
        {
            this.multiSelect.SelectOptionByValue(value);
        }

        [Then(@"options with text ""(.*)"" are selected in the multi select")]
        public void ThenOptionsWithTextAreSelectedInSelectWithId(string[] texts)
        {
            this.multiSelect.SelectedOptions.Select(x => x.Text).Should().BeEquivalentTo(texts.AsEnumerable());
        }

        [Then(@"options with value ""(.*)"" are selected in the multi select")]
        public void ThenOptionsWithValueAreSelectedInSelectWithId(string[] values)
        {
            this.multiSelect.SelectedOptions.Select(x => x.Value).Should().BeEquivalentTo(values.AsEnumerable());
        }

        [Then(@"there are multi selects with id ""(.*)""")]
        public void ThenThereAreLisboxesWithId(string[] ids)
        {
            Browser.Current.MultiSelects().Select(x => x.Id).Should().BeEquivalentTo(ids.AsEnumerable());
        }   
    }
}