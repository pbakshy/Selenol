// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

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

        private static TextboxElement GetTextbox(string id)
        {
            return Browser.Current.Textbox(By.Id(id));
        }

        private static SelectElement GetSelect(string id)
        {
            return Browser.Current.Select(By.Id(id));
        }
    }
}