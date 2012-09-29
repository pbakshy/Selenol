// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System.Linq;

using FluentAssertions;

using OpenQA.Selenium;

using Selenol.Elements;

using TechTalk.SpecFlow;

namespace Selenol.FunctionalTests.Steps
{
    [Binding]
    public class ElementSteps
    {
        [When(@"I type text ""(.*)"" to textbox with id ""(.*)""")]
        public void WhenITypeTextToTextboxWithId(string text, string id)
        {
            GetTextbox(id).TypeText(text);
        }

        [When(@"I clear textbox with id ""(.*)""")]
        public void WhenIClearTextboxWithId(string id)
        {
            GetTextbox(id).Clear();
        }

        [When(@"I select option with text ""(.*)"" in select with ""(.*)""")]
        public void WhenISelectOptionWithInSelectWith(string text, string id)
        {
            GetSelect(id).SelectOptionByText(text);
        }

        [When(@"I select option with value ""(.*)"" in select with ""(.*)""")]
        public void WhenISelectOptionWithValueInSelectWith(string value, string id)
        {
            GetSelect(id).SelectOptionByValue(value);
        }

        [When(@"I clear text area with id ""(.*)""")]
        public void WhenIClearTextAreaWithId(string id)
        {
            GetTextArea(id).Clear();
        }

        [When(@"I type text ""(.*)"" to text area with id ""(.*)""")]
        public void WhenITypeTextToTextAreaWithId(string text, string id)
        {
            GetTextArea(id).TypeText(text);
        }

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

        [Then(@"text ""(.*)"" appears in textbox with id ""(.*)""")]
        public void ThenTextAppearsInTextboxWithId(string text, string id)
        {
            GetTextbox(id).Text.Should().Be(text);
        }

        [Then(@"in select with id ""(.*)"" selected text is ""(.*)""")]
        public void ThenInSelectWithIdSelectedTextIs(string id, string text)
        {
            GetSelect(id).SelectedOption.Text.Should().Be(text);
        }

        [Then(@"in select with id ""(.*)"" selected value is ""(.*)""")]
        public void ThenInSelectWithIdSelectedValueIs(string id, string value)
        {
            GetSelect(id).SelectedOption.Value.Should().Be(value);
        }

        [Then(@"text ""(.*)"" appears in text area with id ""(.*)""")]
        public void ThenTextAppearsInTextAreaWithId(string text, string id)
        {
            GetTextArea(id).Text.Should().Be(text);
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

        private static TextboxElement GetTextbox(string id)
        {
            return Browser.Current.Textbox(By.Id(id));
        }

        private static SelectElement GetSelect(string id)
        {
            return Browser.Current.Select(By.Id(id));
        }

        private static TextAreaElement GetTextArea(string id)
        {
            return Browser.Current.TextArea(By.Id(id));
        }

        private static ListboxElement GetListbox(string id)
        {
            return Browser.Current.Listbox(By.Id(id));
        }
    }
}