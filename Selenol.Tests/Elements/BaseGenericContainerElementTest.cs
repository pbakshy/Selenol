// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using FluentAssertions;

using NUnit.Framework;

using OpenQA.Selenium;

using Rhino.Mocks;

using Selenol.Elements;

namespace Selenol.Tests.Elements
{
    [TestFixture]
    public abstract class BaseGenericContainerElementTest<TElement> : BaseHtmlElementTest<TElement> where TElement : GenericContainerElement
    {
        [Test]
        public void GetText()
        {
            this.WebElement.Stub(x => x.Text).Return("abc");
            this.TypedElement.Text.Should().Be("abc");
        }

        [Test]
        public void FindElement()
        {
            var selector = By.CssSelector("test");
            this.TypedElement.FindElement(selector);
            this.WebElement.AssertWasCalled(x => x.FindElement(selector));
        }

        [Test]
        public void FindElements()
        {
            var selector = By.CssSelector("test");
            this.TypedElement.FindElements(selector);
            this.WebElement.AssertWasCalled(x => x.FindElements(selector));
        }
    }
}