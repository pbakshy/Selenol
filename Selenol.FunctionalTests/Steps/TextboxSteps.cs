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
        private TextboxElement textbox;

        [When(@"I look at a textbox with id ""(.*)""")]
        public void WhenILookAtATextboxWithId(string id)
        {
            this.textbox = Browser.Current.Textbox(By.Id(id));
        }

        [When(@"I type text ""(.*)"" to the textbox")]
        public void WhenITypeTextToTextboxWithId(string text)
        {
            this.textbox.TypeText(text);
        }

        [When(@"I clear the textbox")]
        public void WhenIClearTextboxWithId()
        {
            this.textbox.Clear();
        }

        [Then(@"text ""(.*)"" appears in the textbox")]
        public void ThenTextAppearsInTextboxWithId(string text)
        {
            this.textbox.Text.Should().Be(text);
        }

        [Then(@"there are textboxes with id ""(.*)""")]
        public void ThenThereAreTextboxesWithId(string[] ids)
        {
            Browser.Current.Textboxes().Select(x => x.Id).Should().BeEquivalentTo(ids.AsEnumerable());
        }
    }
}