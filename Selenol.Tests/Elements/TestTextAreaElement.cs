// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using FluentAssertions;

using NUnit.Framework;

using Rhino.Mocks;

using Selenol.Elements;

namespace Selenol.Tests.Elements
{
    public class TestTextAreaElement : BaseHtmlElementTest<TextAreaElement>
    {
        [Test]
        public void GetText()
        {
            this.WebElement.Stub(x => x.Text).Return("some text inside");
            this.TypedElement.Text.Should().Be("some text inside");
        }

        [Test]
        public void TypeText()
        {
            this.TypedElement.TypeText("some text");
            this.WebElement.AssertWasCalled(x => x.SendKeys("some text"));
        }

        protected override TextAreaElement CreateElement()
        {
            return new TextAreaElement(this.WebElement);
        }

        protected override void SetProperElementConditions()
        {
            this.WebElement.Stub(x => x.TagName).Return("textarea");
        }
    }
}