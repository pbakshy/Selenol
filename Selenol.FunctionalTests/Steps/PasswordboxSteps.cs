// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using FluentAssertions;
using OpenQA.Selenium;
using Selenol.Elements;
using TechTalk.SpecFlow;

namespace Selenol.FunctionalTests.Steps
{
    [Binding]
    public class PasswordboxSteps
    {
        private PasswordboxElement element;

        [When(@"I look at a password field with id ""(.*)""")]
        public void WhenILookAtAPasswordInputWithId(string id)
        {
            this.element = Browser.Current.Passwordbox(By.Id(id));
        }

        [Then(@"text ""(.*)"" appears in the password field")]
        public void ThenTextAppearsInThePasswordField(string text)
        {
            this.element.Text.Should().Be(text);
        }

        [When(@"I type password ""(.*)"" to the password field")]
        public void WhenITypePasswordToThePasswordField(string password)
        {
            this.element.TypeText(password);
        }

        [When(@"I clear the password field")]
        public void WhenIClearThePasswordField()
        {
            this.element.Clear();
        }
    }
}