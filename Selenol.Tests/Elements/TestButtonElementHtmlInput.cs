// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using FluentAssertions;

using NUnit.Framework;

using Rhino.Mocks;

namespace Selenol.Tests.Elements
{
    [TestFixture]
    public class TestButtonElementHtmlInput : BaseButtonElementTest
    {
        [Test]
        public void GetText()
        {
            this.WebElement.Stub(x => x.GetAttribute("value")).Return("abc");
            this.TypedElement.Text.Should().Be("abc");
        }

        [Test]
        public void GetNotExistingText()
        {
            this.WebElement.Stub(x => x.GetAttribute("value")).Return(null);
            this.TypedElement.Text.Should().Be(string.Empty);
        }

        protected override void SetProperElementConditions()
        {
            this.WebElement.Stub(x => x.TagName).Return("input");
            this.WebElement.Stub(x => x.GetAttribute("type")).Return("button");
        }
    }
}