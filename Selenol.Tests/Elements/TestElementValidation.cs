// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using FluentAssertions;

using NUnit.Framework;

using OpenQA.Selenium;

using Rhino.Mocks;

using Selenol.Elements;
using Selenol.Validation;

namespace Selenol.Tests.Elements
{
    [TestFixture]
    public class TestElementValidation
    {
        private IWebElement webElement;

        [SetUp]
        public void Init()
        {
            this.webElement = MockRepository.GenerateStub<IWebElement>();
            this.webElement.Stub(x => x.TagName).Return("p");
        }

        [Test]
        public void IncorrectElementSingleValidator()
        {
            var exception = Assert.Throws<WrongElementException>(() => new ElementWithSingleValidation(this.webElement));
            exception.Message.Should().Be("'p' tag does not match to 'div' tag");
        }

        [Test]
        public void IncorrectElementSeveralValidator()
        {
            var exception = Assert.Throws<WrongElementException>(() => new ElementWithSeveralValidations(this.webElement));
            exception.Message.Should().Contain("'p' tag does not match to 'div' tag").And.Contain("'p' tag does not match to 'input' tag");
        }

        [Test, ExpectedException(typeof(ValidationAbsenceException))]
        public void ValidationIsAbsent()
        {
// ReSharper disable ObjectCreationAsStatement
            new ElementWithoutValidation(this.webElement);
// ReSharper restore ObjectCreationAsStatement
        }

        [Test, ExpectedException(typeof(WrongElementException))]
        public void ValidationDoesNotInherit()
        {
            var divWebElement = MockRepository.GenerateStub<IWebElement>();
            divWebElement.Stub(x => x.TagName).Return("div");

// ReSharper disable ObjectCreationAsStatement
            new DerivedElement(divWebElement);
// ReSharper restore ObjectCreationAsStatement
        }

        [Tag("div")]
        private class ElementWithSingleValidation : BaseHtmlElement
        {
            public ElementWithSingleValidation(IWebElement webElement)
                : base(webElement)
            {
            }
        }

        [Tag("div")]
        [Input("text")]
        private class ElementWithSeveralValidations : BaseHtmlElement
        {
            public ElementWithSeveralValidations(IWebElement webElement)
                : base(webElement)
            {
            }
        }

        private class ElementWithoutValidation : BaseHtmlElement
        {
            public ElementWithoutValidation(IWebElement webElement)
                : base(webElement)
            {
            }
        }

        [Input("text")]
        private class DerivedElement : ElementWithSingleValidation 
        {
            public DerivedElement(IWebElement webElement)
                : base(webElement)
            {
            }
        }
    }
}