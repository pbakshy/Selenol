// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using FluentAssertions;

using NUnit.Framework;

using OpenQA.Selenium;

using Rhino.Mocks;

using Selenol.Validation.Element;

namespace Selenol.Tests.Validation
{
    [TestFixture]
    public class TestNotInputAttribute
    {
        [Test] 
        [TestCase("div")]
        [TestCase("p")]
        [TestCase("span")]
        public void ValidateCorrectElement(string tagName)
        {
            var webElement = MockRepository.GenerateStub<IWebElement>();
            webElement.Stub(x => x.TagName).Return(tagName);

            var element = new ElementForTest(webElement);
            var attribute = new NotInputAttribute();
            attribute.Validate(element).Should().BeTrue();
        }

        [Test]
        [TestCase("input")]
        [TestCase("textarea")]
        [TestCase("select")]
        [TestCase("button")]
        [TestCase("option")]
        public void ValidateInCorrectElement(string tagName)
        {
            var webElement = MockRepository.GenerateStub<IWebElement>();
            webElement.Stub(x => x.TagName).Return(tagName);

            var element = new ElementForTest(webElement);
            var attribute = new NotInputAttribute();
            attribute.Validate(element).Should().BeFalse();
        }

        [Test]
        public void GetErrorMessage()
        {
            var webElement = MockRepository.GenerateStub<IWebElement>();
            webElement.Stub(x => x.TagName).Return("input");

            var element = new ElementForTest(webElement);
            var attribute = new NotInputAttribute();
            attribute.GetErrorMessage(element).Should().Be("Expected tag not in set of input, textarea, select, button, option. But was 'input'.");
        }
    }
}