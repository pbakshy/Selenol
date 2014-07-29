// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System.Linq;
using FluentAssertions;
using OpenQA.Selenium.Remote;
using Selenol.Extensions;
using Selenol.FunctionalTests.PageObjects;
using TechTalk.SpecFlow;

namespace Selenol.FunctionalTests.Steps
{
    [Binding]
    public class DynamicBindingSteps
    {
        private ElementsPage elementsPage;

        [When(@"I am acessing ""Elements"" page using page object")]
        public void WhenIAmAcessingPageUsingPageObject()
        {
            var webDriver = (RemoteWebDriver)Browser.Current;
            this.elementsPage = webDriver.GoTo<ElementsPage>("http://localhost:{0}/Elements.html".FInv(Configuration.ServerPort));
        }

        [Then(@"value of FirstSelect must be '(.*)'")]
        public void ThenValueOfFirstMultiSelectMustBe(string expectedValue)
        {
            this.elementsPage.FirstSelect.SelectedOption.Value.Should().Be(expectedValue);
        }

        [Then(@"id of SecondButton must be '(.*)'")]
        public void ThenIdOfSecondButtonMustBe(string expectedId)
        {
            this.elementsPage.SecondButton.Id.Should().Be(expectedId);
        }

        [Then(@"second radio button in the sequence must be checked")]
        public void ThenSecondRadioButtonInTheSequenceMustBeChecked()
        {
            this.elementsPage.RadioButtons.Skip(1).First().IsChecked.Should().BeTrue();
        }

        [Then(@"SecondCheckbox must be unchecked")]
        public void ThenSecondCheckboxMustBeUnchecked()
        {
            this.elementsPage.SecondCheckbox.IsChecked.Should().BeFalse();
        }
    }
}