// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System.Drawing;

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
            this.element = Browser.Current.Container(By.Id(id));
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

        [Then(@"width of the element is (\d+)")]
        public void ThenWidthOfTheElementIs(int width)
        {
            this.element.Styles.Width.Should().BeInRange(width - 10, width + 10);
        }

        [Then(@"height of the element is (\d+)")]
        public void ThenHeightOfTheElementIs(int height)
        {
            this.element.Styles.Height.Should().BeInRange(height - 10, height + 10);
        }

        [Then(@"color of the element is ""(.*)""")]
        public void ThenColorOfTheElementIs(string colorName)
        {
            var color = Color.FromName(colorName);
            var actualColor = this.element.Styles.ForegroundColor;
            actualColor.A.Should().Be(color.A);
            actualColor.R.Should().Be(color.R);
            actualColor.G.Should().Be(color.G);
            actualColor.B.Should().Be(color.B);
        }

        [Then(@"border of the element is ""(.*)""")]
        public void ThenBorderOfTheElementIs(string borderStyle)
        {
            this.element.Styles.GetStyle("border-style").Should().Be(borderStyle);
        }
    }
}