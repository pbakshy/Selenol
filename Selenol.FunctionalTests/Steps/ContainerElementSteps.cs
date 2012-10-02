// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using FluentAssertions;

using OpenQA.Selenium;

using Selenol.Elements;

using TechTalk.SpecFlow;

namespace Selenol.FunctionalTests.Steps
{
    [Binding]
    public class ContainerElementSteps
    {
        private BaseHtmlElement element;

        [When(@"I look at an element with id ""(.*)""")]
        public void WhenILookAtAnElementWithId(string id)
        {
            this.element = Browser.Current.Element(By.Id(id));
        }

        [When(@"I go to a parent of the element")]
        public void WhenIgoToAParentOfTheElement()
        {
            this.element = this.element.Parent;
        }

        [When(@"I go to a next sibling of the element")]
        public void WhenIgoToANextSiblingOfTheElement()
        {
            this.element = this.element.NextSibling;
        }

        [When(@"I go to a previous sibling of the element")]
        public void WhenIgoToAPreviousSiblingOfTheElement()
        {
            this.element = this.element.PreviousSibling;
        }

        [Then(@"the element id is ""(.*)""")]
        public void ThenTheElementIdIs(string id)
        {
            this.element.Id.Should().Be(id);
        }
    }
}