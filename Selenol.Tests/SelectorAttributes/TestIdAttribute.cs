// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Text.RegularExpressions;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using Rhino.Mocks;
using Selenol.Elements;
using Selenol.Page;
using Selenol.SelectorAttributes;

namespace Selenol.Tests.SelectorAttributes
{
    /// <summary>The test id attribute.</summary>
    [TestFixture]
    public class TestIdAttribute
    {
        private IWebDriver webDriver;
        private IJavaScriptExecutor javaScriptExecutor;
        private IWebElement webElement;

        [SetUp]
        public void Init()
        {
            this.webDriver = MockRepository.GenerateStub<IWebDriver>();
            this.javaScriptExecutor = MockRepository.GenerateStub<IJavaScriptExecutor>();
            this.webElement = MockRepository.GenerateStub<IWebElement>();

            this.webDriver.Url = "/myhome/page.aspx";
        }

        [Test]
        public void CanBeUsedForAutoProperty()
        {
            this.webElement.Stub(x => x.TagName).Return("button");
            var page = PageFactory.Create<PageWithSelectorAttribute>(this.webDriver, this.javaScriptExecutor);
            this.webDriver.Stub(x => x.FindElement(By.Id("test-selector"))).Return(this.webElement);
            this.webElement.Stub(x => x.Text).Return("abcd");

            var button = page.Button;

            button.Text.Should().Be("abcd");
            button.Should().NotBeSameAs(page.Button);
        }

        [Test]
        public void ShouldProxyPropertyFromBaseClass()
        {
            //todo refactor this with previous
            this.webElement.Stub(x => x.TagName).Return("button");
            var page = PageFactory.Create<PageInheritsPropertiesWithSelectorAttribute>(this.webDriver, this.javaScriptExecutor);
            this.webDriver.Stub(x => x.FindElement(By.Id("test-selector"))).Return(this.webElement);
            this.webElement.Stub(x => x.Text).Return("abcd");

            var button = page.Button;

            button.Text.Should().Be("abcd");
            button.Should().NotBeSameAs(page.Button);
        }
        
        [Test]
        public void IncorrectAttributeUsage()
        {
            var exception =
                Assert.Throws<Exception>(() => PageFactory.Create<PageWithIncorrectSelectorAttributeUsage>(this.webDriver, this.javaScriptExecutor));
            exception.Message.Should()
                     .Contain("'NotAuthoProperty' is not an auto property. Selector attributes can be used only for auto properties.")
                     .And
                     .Contain("'PropertyWithoutSetter' is not an auto property. Selector attributes can be used only for auto properties.")
                     .And
                     .Contain(
                         "'NotElement' property has invalid type. Selector attributes can be used only for properties with type derived from BaseHtmlElement.")
                     .And
                     .Contain("'AbstractElement' property has invalid type. Selector attributes can not be used for abstract types.")
                     .And
                     .Contain("'NotVirtualProperty' is not virtual. Selector attributes can be used only for virtual properties.")
                     .And
                     .Contain("'PropertyWithoutGetter' is not an auto property. Selector attributes can be used only for auto properties.");

            AssertSinglePropertyError(exception, "NotAuthoProperty");
            AssertSinglePropertyError(exception, "NotVirtualProperty");
            AssertSinglePropertyError(exception, "AbstractElement");
            AssertSinglePropertyError(exception, "NotElement");
        }

        [Test]
        public void ThrowsForNullSelector()
        {
            var exception = Assert.Throws<ArgumentException>(() => PageFactory.Create<PageWithNullSelector>(this.webDriver, this.javaScriptExecutor));
            exception.Message.Should().Contain("Parameter can not be null or empty.");
        }

        [Test]
        public void ThrowsForEmptySelector()
        {
            var exception = Assert.Throws<ArgumentException>(() => PageFactory.Create<PageWithEmptySelector>(this.webDriver, this.javaScriptExecutor));
            exception.Message.Should().Contain("Parameter can not be null or empty.");
        }

        private static void AssertSinglePropertyError(Exception exception, string propertyName)
        {
            var count = new Regex(propertyName).Matches(exception.Message).Count;
            count.Should().Be(1);
        }
        
        public class PageWithSelectorAttribute : SimplePageForTest
        {
            [Id("test-selector")]
            public virtual ButtonElement Button { get; private set; }
        }

        public class PageInheritsPropertiesWithSelectorAttribute : PageWithSelectorAttribute
        {
        }

        public class PageWithIncorrectSelectorAttributeUsage : SimplePageForTest
        {
            private TextboxElement notAuthoProperty;

            [Id("test-selector")]
            public virtual TextboxElement NotAuthoProperty
            {
                get
                {
                    return this.notAuthoProperty;
                }

                set
                {
                    this.notAuthoProperty = value;
                }
            }

            /// <summary>Gets the property without setter.</summary>
            [Id("test-selector")]
            public virtual TextAreaElement PropertyWithoutSetter
            {
                get
                {
                    return null;
                }
            }

            [Id("test-selector")]
            public virtual int NotElement { get; set; }

            [Id("test-selector")]
            public virtual BaseHtmlElement AbstractElement { get; set; }

            [Id("test-selector")]
            public SelectElement NotVirtualProperty { get; set; }

            [Id("test-selector")]
            public virtual TextboxElement PropertyWithoutGetter
            {
                set
                {
                    this.notAuthoProperty = value;
                }
            }
        }

        public class PageWithNullSelector : SimplePageForTest
        {
            [Id(null)]
            public virtual TextboxElement TextboxElement { get; set; } 
        }

        public class PageWithEmptySelector : SimplePageForTest
        {
            [Id("")]
            public virtual FormElement FormElement { get; set; }
        }
    }
}