// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using FluentAssertions;

using NUnit.Framework;

using Rhino.Mocks;

using Selenol.Elements;

namespace Selenol.Tests.Elements
{
    [TestFixture]
    public class TestLinkElement : BaseHtmlElementTest<LinkElement>
    {
        [Test]
        public void GetText()
        {
            this.WebElement.Stub(x => x.Text).Return("aabc");
            this.TypedElement.Text.Should().Be("aabc");
        }

        [Test]
        public void GetUrl()
        {
            this.WebElement.Stub(x => x.GetAttribute("href")).Return("http://google.com");
            this.TypedElement.Url.Should().Be("http://google.com");
        }

        [Test]
        public void Click()
        {
            this.TypedElement.Click();
            this.WebElement.AssertWasCalled(x => x.Click());
        }

        protected override LinkElement CreateElement()
        {
            return new LinkElement(this.WebElement);
        }

        protected override void SetProperElementConditions()
        {
            this.WebElement.Stub(x => x.TagName).Return("a");
        }
    }
}