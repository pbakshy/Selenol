// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Reflection;
using System.Text.RegularExpressions;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using Rhino.Mocks;
using Selenol.Elements;
using Selenol.Extensions;
using Selenol.Page;

namespace Selenol.Tests.SelectorAttributes
{
    public abstract class BaseSelectorAttributeTest
    {
        protected const string TestSelector = "test-selector";
        private static readonly MethodInfo factoryMethod = typeof(PageFactory).GetMethod("Create", BindingFlags.Public | BindingFlags.Static);

        protected IWebDriver WebDriver { get; private set; }

        protected IJavaScriptExecutor JavaScriptExecutor { get; private set; }

        protected IWebElement WebElement { get; private set; }

        [SetUp]
        public void Init()
        {
            this.WebDriver = MockRepository.GenerateStub<IWebDriver>();
            this.JavaScriptExecutor = MockRepository.GenerateStub<IJavaScriptExecutor>();
            this.WebElement = MockRepository.GenerateStub<IWebElement>();

            this.WebDriver.Url = "/myhome/page.aspx";
        }

        [Test]
        public void CanBeUsedForAutoProperty()
        {
            var page = this.CreatePageUsingFactory("PageWithSelectorAttribute");
            this.AssertCorrectSelectorAttributeUsage(page);
        }

        [Test]
        public void ShouldProxyPropertyFromBaseClass()
        {
            var page = this.CreatePageUsingFactory("PageInheritsPropertiesWithSelectorAttribute");
            this.AssertCorrectSelectorAttributeUsage(page);
        }

        [Test]
        public void IncorrectAttributeUsage()
        {
            var realException = Assert.Throws<TargetInvocationException>(() => this.CreatePageUsingFactory("PageWithIncorrectSelectorAttributeUsage"))
                                      .InnerException;

            realException.Should().BeOfType<PageInitializationException>();
            realException.Message.Should()
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

            AssertSinglePropertyError(realException, "NotAuthoProperty");
            AssertSinglePropertyError(realException, "NotVirtualProperty");
            AssertSinglePropertyError(realException, "AbstractElement");
            AssertSinglePropertyError(realException, "NotElement");
        }

        [Test]
        public void ThrowsForNullSelector()
        {
            var realException = Assert.Throws<TargetInvocationException>(() => this.CreatePageUsingFactory("PageWithNullSelector")).InnerException;

            realException.Should().BeOfType<ArgumentException>();
            realException.Message.Should().Contain("Parameter can not be null or empty.");
        }

        [Test]
        public void ThrowsForEmptySelector()
        {
            var realException = Assert.Throws<TargetInvocationException>(() => this.CreatePageUsingFactory("PageWithEmptySelector")).InnerException;

            realException.Should().BeOfType<ArgumentException>();
            realException.Message.Should().Contain("Parameter can not be null or empty.");
        }

        protected abstract By GetByCriteria(string selectorValue);

        private static void AssertSinglePropertyError(Exception exception, string propertyName)
        {
            var count = new Regex(propertyName).Matches(exception.Message).Count;
            count.Should().Be(1);
        }

        private static ButtonElement GetButton(SimplePageForTest page)
        {
            var buttonProperty = page.GetType().GetProperty("Button");
            if (buttonProperty == null)
            {
                Assert.Fail("Page '{0}' does not cointain property Button which requires for the tests.".F(page.GetType().FullName));
            }

            return (ButtonElement)buttonProperty.GetValue(page, null);
        }

        private SimplePageForTest CreatePageUsingFactory(string pageClassName)
        {
            var typeName = "{0}+{1}".FInv(this.GetType().FullName, pageClassName);
            var validPageType = Type.GetType(typeName);
            if (validPageType == null)
            {
                Assert.Fail("Unable to finde page with type '{0}' which requires for the tests".F(typeName));
            }

            var typedCreateMethod = factoryMethod.MakeGenericMethod(validPageType);
            return (SimplePageForTest)typedCreateMethod.Invoke(null, new object[] { this.WebDriver, this.JavaScriptExecutor });
        }

        private void AssertCorrectSelectorAttributeUsage(SimplePageForTest page)
        {
            this.WebElement.Stub(x => x.TagName).Return("button");
            this.WebDriver.Stub(x => x.FindElement(this.GetByCriteria(TestSelector))).Return(this.WebElement);
            this.WebElement.Stub(x => x.Text).Return("abcd");

            var button = GetButton(page);

            button.Text.Should().Be("abcd");
            button.Should().NotBeSameAs(GetButton(page));
        }
    }
}