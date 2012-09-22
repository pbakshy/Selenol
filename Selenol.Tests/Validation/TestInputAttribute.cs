// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using FluentAssertions;

using NUnit.Framework;

using OpenQA.Selenium;

using Rhino.Mocks;

using Selenol.Validation.Element;

namespace Selenol.Tests.Validation
{
    [TestFixture]
    public class TestInputAttribute
    {
        [Test] 
        public void ValidateCorrectElement()
        {
            ValidateTest("input", "button", true);
        }

        [Test]
        public void ValidateElementWithIncorrectType()
        {
            ValidateTest("input", "text", false);
        }

        [Test]
        public void ValidateElementWithIncorrectTag()
        {
            ValidateTest("div", null, false);
        }

        [Test]
        public void GetErrorMessageForElementWithIncorrectType()
        {
            GetErrorMessageTest("input", "button", @"The attribute 'type=""button""' does not match to 'type=""text""'");
        }

        [Test]
        public void GetErrorMessageForElementWithIncorrectTag()
        {
            GetErrorMessageTest("button", null, "'button' tag does not match to 'input' tag");
        }

        private static void ValidateTest(string tagName, string typeName, bool result)
        {
            var webElement = MockRepository.GenerateStub<IWebElement>();
            webElement.Stub(x => x.TagName).Return(tagName);
            webElement.Stub(x => x.GetAttribute("type")).Return(typeName);

            var element = new ElementForTest(webElement);
            var attribute = new InputAttribute("button");
            attribute.Validate(element).Should().Be(result);
        }

        private static void GetErrorMessageTest(string tagName, string typeName, string message)
        {
            var webElement = MockRepository.GenerateStub<IWebElement>();
            webElement.Stub(x => x.TagName).Return(tagName);
            webElement.Stub(x => x.GetAttribute("type")).Return(typeName);

            var element = new ElementForTest(webElement);
            var attribute = new InputAttribute("text");
            attribute.GetErrorMessage(element).Should().Be(message);
        }
    }
}