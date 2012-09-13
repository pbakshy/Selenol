// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using FluentAssertions;

using NUnit.Framework;

using Rhino.Mocks;

using Selenol.Elements;

namespace Selenol.Tests.Elements
{
    [TestFixture]
    public abstract class BaseButtonElementTest : BaseHtmlElementTest<ButtonElement>
    {
        [Test]
        public void GetValue()
        {
            this.WebElement.Stub(x => x.GetAttribute("value")).Return("test");
            this.TypedElement.Value.Should().Be("test");
        }

        [Test]
        public void GetNotExistingValue()
        {
            this.WebElement.Stub(x => x.GetAttribute("value")).Return(null);
            this.TypedElement.Value.Should().Be(string.Empty);
        }

        [Test]
        public void Click()
        {
            this.TypedElement.Click();
            this.WebElement.AssertWasCalled(x => x.Click());
        }

        protected override ButtonElement CreateElement()
        {
            return new ButtonElement(this.WebElement);
        }
    }
}