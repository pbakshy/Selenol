// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System.Linq;

using FluentAssertions;

using OpenQA.Selenium;

using Selenol.Elements;

using TechTalk.SpecFlow;

namespace Selenol.FunctionalTests.Steps
{
    [Binding]
    public class TextAreaSteps
    {
        private TextAreaElement textArea;

        [When(@"I look at a text area with id ""(.*)""")]
        public void WhenILookAtATextAreaWithId(string id)
        {
            this.textArea = Browser.Current.TextArea(By.Id(id));
        }

        [When(@"I clear the text area")]
        public void WhenIClearTextAreaWithId()
        {
            this.textArea.Clear();
        }

        [When(@"I type text ""(.*)"" to the text area")]
        public void WhenITypeTextToTextAreaWithId(string text)
        {
            this.textArea.TypeText(text);
        }

        [Then(@"text ""(.*)"" appears in the text area")]
        public void ThenTextAppearsInTextAreaWithId(string text)
        {
            this.textArea.Text.Should().Be(text);
        }

        [Then(@"there are text areas with id ""(.*)""")]
        public void ThenThereAreTextAreasWithId(string[] ids)
        {
            Browser.Current.TextAreas().Select(x => x.Id).Should().BeEquivalentTo(ids.AsEnumerable());
        }
    }
}