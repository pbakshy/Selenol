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
        private ListboxElement listbox;

        [When(@"I look at a listbox with id ""(.*)""")]
        public void WhenILookAtAListboxWithId(string id)
        {
            this.listbox = Browser.Current.Listbox(By.Id(id));
        }

        [When(@"I clear selection in the listbox")]
        public void WhenIClearSelectionInListboxWithId()
        {
            this.listbox.ClearSelection();
        }

        [When(@"I select an option with text ""(.*)"" in the listbox")]
        public void WhenISelectAnOptionWithTextInListboxWithId(string text)
        {
            this.listbox.SelectOptionByText(text);
        }

        [When(@"I select an option with value ""(.*)"" in the listbox")]
        public void WhenISelectAnOptionWithValueInListboxWithId(string value)
        {
            this.listbox.SelectOptionByValue(value);
        }

        [Then(@"options with text ""(.*)"" are selected in the listbox")]
        public void ThenOptionsWithTextAreSelectedInSelectWithId(string[] texts)
        {
            this.listbox.SelectedOptions.Select(x => x.Text).Should().BeEquivalentTo(texts.AsEnumerable());
        }

        [Then(@"options with value ""(.*)"" are selected in the listbox")]
        public void ThenOptionsWithValueAreSelectedInSelectWithId(string[] values)
        {
            this.listbox.SelectedOptions.Select(x => x.Value).Should().BeEquivalentTo(values.AsEnumerable());
        }

        [Then(@"there are lisboxes with id ""(.*)""")]
        public void ThenThereAreLisboxesWithId(string[] ids)
        {
            Browser.Current.Listboxes().Select(x => x.Id).Should().BeEquivalentTo(ids.AsEnumerable());
        }   
    }
}