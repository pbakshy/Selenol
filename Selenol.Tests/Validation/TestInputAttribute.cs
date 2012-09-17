// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using FluentAssertions;

using NUnit.Framework;

using OpenQA.Selenium;

using Rhino.Mocks;

using Selenol.Elements;
using Selenol.Validation;

namespace Selenol.Tests.Validation
{
    [TestFixture]
    public class TestInputAttribute
    {
        [Test] 
        public void ValidateCorrectElement()
        {
            var webElement = MockRepository.GenerateStub<IWebElement>();
            webElement.Stub(x => x.TagName).Return("input");
            webElement.Stub(x => x.GetAttribute("type")).Return("button");

            var element = new ButtonElement(webElement);
            var attribute = new InputAttribute("button");
            attribute.Validate(element).Should().BeTrue();
        }

        [Test]
        public void ValidateElementWithIncorrectType()
        {
            var webElement = MockRepository.GenerateStub<IWebElement>();
            webElement.Stub(x => x.TagName).Return("input");
            webElement.Stub(x => x.GetAttribute("type")).Return("button");

            var element = new ButtonElement(webElement);
            var attribute = new InputAttribute("text");
            attribute.Validate(element).Should().BeFalse();
        }

        [Test]
        public void ValidateElementWithIncorrectTag()
        {
            var webElement = MockRepository.GenerateStub<IWebElement>();
            webElement.Stub(x => x.TagName).Return("button");

            var element = new ButtonElement(webElement);
            var attribute = new InputAttribute("button");
            attribute.Validate(element).Should().BeFalse();
        }

        [Test]
        public void GetErrorMessageForElementWithIncorrectType()
        {
            var webElement = MockRepository.GenerateStub<IWebElement>();
            webElement.Stub(x => x.TagName).Return("input");
            webElement.Stub(x => x.GetAttribute("type")).Return("button");

            var element = new ButtonElement(webElement);
            var attribute = new InputAttribute("text");
            attribute.GetErrorMessage(element).Should().Be(@"The attribute 'type=""button""' does not match to 'type=""text""'");
        }

        [Test]
        public void GetErrorMessageForElementWithIncorrectTag()
        {
            var webElement = MockRepository.GenerateStub<IWebElement>();
            webElement.Stub(x => x.TagName).Return("button");

            var element = new ButtonElement(webElement);
            var attribute = new InputAttribute("button");
            attribute.GetErrorMessage(element).Should().Be("'button' tag does not match to 'input' tag");
        }
    }
}