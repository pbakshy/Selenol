// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using FluentAssertions;

using NUnit.Framework;

using OpenQA.Selenium;

using Rhino.Mocks;

using Selenol.Validation.Element;

namespace Selenol.Tests.Validation.Element
{
    [TestFixture]
    public class TestTagAttribute
    {
        private ElementForTest element;

        [SetUp]
        public void Init()
        {
            var webElement = MockRepository.GenerateStub<IWebElement>();
            webElement.Stub(x => x.TagName).Return("button");

            this.element = new ElementForTest(webElement);
        }

        [Test]     
        public void ValidateCorrectElement()
        {
            var attribute = new TagAttribute("button");
            attribute.Validate(this.element).Should().BeTrue();
        }

        [Test]
        public void ValidateIncorrectElement()
        {
            var attribute = new TagAttribute("div");
            attribute.Validate(this.element).Should().BeFalse();
        }

        [Test]
        public void GetErrorMessage()
        {
            var attribute = new TagAttribute("p");
            attribute.GetErrorMessage(this.element).Should().Be("'button' tag does not match to 'p' tag");
        }
    }
}