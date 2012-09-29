// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System.Linq;

using FluentAssertions;

using OpenQA.Selenium;

using Selenol.Elements;

using TechTalk.SpecFlow;

namespace Selenol.FunctionalTests.Steps
{
    [Binding]
    public class TextboxSteps
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

        [Then(@"text ""(.*)"" appears in textbox with id ""(.*)""")]
        public void ThenTextAppearsInTextboxWithId(string text, string id)
        {
            GetTextbox(id).Text.Should().Be(text);
        }

        [Then(@"there are textboxes with id ""(.*)""")]
        public void ThenThereAreTextboxesWithId(string[] ids)
        {
            Browser.Current.Textboxes().Select(x => x.Id).Should().BeEquivalentTo(ids.AsEnumerable());
        }

        private static TextboxElement GetTextbox(string id)
        {
            return Browser.Current.Textbox(By.Id(id));
        }
    }
}