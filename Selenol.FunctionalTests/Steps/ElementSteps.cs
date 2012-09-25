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
        public void WhenITypeTextToTextboxWithId(string text, string textboxId)
        {
            Browser.Current.Textbox(By.Id(textboxId)).TypeText(text);
        }

        [Then(@"text ""(.*)"" appears in textbox with id ""(.*)""")]
        public void ThenTextAppearsInTextboxWithId(string text, string textboxId)
        {
            Browser.Current.Textbox(By.Id(textboxId)).Text.Should().Be(text);
        }
    }
}