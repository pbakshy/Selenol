// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System;

using FluentAssertions;

using NUnit.Framework;

using OpenQA.Selenium;

using Rhino.Mocks;

using Selenol.Elements;
using Selenol.Validation.Element;

namespace Selenol.Tests.Elements
{
    [TestFixture]
    public class TestCastingElements
    {
        [Test] 
        public void CastBasicHtmlElement()
        {
            var webElement = MockRepository.GenerateStub<IWebElement>();
            webElement.Stub(x => x.TagName).Return("button");
            webElement.Stub(x => x.GetAttribute("id")).Return("b1");

            new BasicHtmlElement(webElement).As<ButtonElement>().Id.Should().Be("b1");
        }

        [Test]
        public void CastContainerElement()
        {
            var webElement = MockRepository.GenerateStub<IWebElement>();
            webElement.Stub(x => x.TagName).Return("table");
            webElement.Stub(x => x.GetAttribute("id")).Return("t1");

            new ContainerElement(webElement).As<TableElement>().Id.Should().Be("t1");
        }

        [Test, ExpectedException(typeof(MissingMethodException))]
        public void ElementDoesNotHavePropertConstructor()
        {
            var webElement = MockRepository.GenerateStub<IWebElement>();
            webElement.Stub(x => x.TagName).Return("div");

            new BasicHtmlElement(webElement).As<NotProperElement>();
        }

// ReSharper disable ClassNeverInstantiated.Local
// ReSharper disable UnusedParameter.Local
        [Tag("div")]
        private class NotProperElement : BaseHtmlElement
        {
            public NotProperElement(IWebElement webElement, int number)
                : base(webElement)
            {
            }
        }
// ReSharper restore ClassNeverInstantiated.Local
// ReSharper restore UnusedParameter.Local
    }
}