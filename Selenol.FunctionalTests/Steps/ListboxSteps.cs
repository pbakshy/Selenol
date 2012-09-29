// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System.Linq;

using FluentAssertions;

using OpenQA.Selenium;

using Selenol.Elements;

using TechTalk.SpecFlow;

namespace Selenol.FunctionalTests.Steps
{
    [Binding]
    public class ListboxSteps
    {
        [When(@"I clear selection in listbox with id ""(.*)""")]
        public void WhenIClearSelectionInListboxWithId(string id)
        {
            GetListbox(id).ClearSelection();
        }

        [When(@"I select an option with text ""(.*)"" in listbox with id ""(.*)""")]
        public void WhenISelectAnOptionWithTextInListboxWithId(string text, string id)
        {
            GetListbox(id).SelectOptionByText(text);
        }

        [When(@"I select an option with value ""(.*)"" in listbox with id ""(.*)""")]
        public void WhenISelectAnOptionWithValueInListboxWithId(string value, string id)
        {
            GetListbox(id).SelectOptionByValue(value);
        }

        [Then(@"options with text ""(.*)"" are selected in select with id ""(.*)""")]
        public void ThenOptionsWithTextAreSelectedInSelectWithId(string[] texts, string id)
        {
            GetListbox(id).SelectedOptions.Select(x => x.Text).Should().BeEquivalentTo(texts.AsEnumerable());
        }

        [Then(@"options with value ""(.*)"" are selected in select with id ""(.*)""")]
        public void ThenOptionsWithValueAreSelectedInSelectWithId(string[] values, string id)
        {
            GetListbox(id).SelectedOptions.Select(x => x.Value).Should().BeEquivalentTo(values.AsEnumerable());
        }

        [Then(@"there are lisboxes with id ""(.*)""")]
        public void ThenThereAreLisboxesWithId(string[] ids)
        {
            Browser.Current.Listboxes().Select(x => x.Id).Should().BeEquivalentTo(ids.AsEnumerable());
        }

        private static ListboxElement GetListbox(string id)
        {
            return Browser.Current.Listbox(By.Id(id));
        }        
    }
}