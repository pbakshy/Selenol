// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Text.RegularExpressions;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using Rhino.Mocks;
using Selenol.Elements;
using Selenol.Page;

namespace Selenol.Tests.SelectorAttributes
{
    public abstract class BaseSelectorAttributeTest<TValidPage, TDerivedValidPage>
        where TValidPage : SimplePageForTest, new() where TDerivedValidPage : TValidPage, new()
    {
        protected const string TestSelector = "test-selector";

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
            var page = PageFactory.Create<TValidPage>(this.WebDriver, this.JavaScriptExecutor);
            this.AssertCorrectSelectorAttributeUsage(page);
        }

        [Test]
        public void ShouldProxyPropertyFromBaseClass()
        {
            var page = PageFactory.Create<TDerivedValidPage>(this.WebDriver, this.JavaScriptExecutor);
            this.AssertCorrectSelectorAttributeUsage(page);
        }

        [Test]
        public void IncorrectAttributeUsage()
        {
            var exception = Assert.Throws<PageInitializationException>(() => this.CreatePageWithIncorrectAttributeUsage());
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
            var exception = Assert.Throws<ArgumentException>(() => this.CreatePageWithNullSelector());
            exception.Message.Should().Contain("Parameter can not be null or empty.");
        }

        [Test]
        public void ThrowsForEmptySelector()
        {
            var exception = Assert.Throws<ArgumentException>(() => this.CreatePageWithEmptySelector());
            exception.Message.Should().Contain("Parameter can not be null or empty.");
        }

        protected abstract ButtonElement GetButton(TValidPage page);

        protected abstract BasePage CreatePageWithIncorrectAttributeUsage();
        
        protected abstract BasePage CreatePageWithNullSelector();

        protected abstract BasePage CreatePageWithEmptySelector();

        private static void AssertSinglePropertyError(Exception exception, string propertyName)
        {
            var count = new Regex(propertyName).Matches(exception.Message).Count;
            count.Should().Be(1);
        }

        private void AssertCorrectSelectorAttributeUsage(TValidPage page)
        {
            this.WebElement.Stub(x => x.TagName).Return("button");
            this.WebDriver.Stub(x => x.FindElement(By.Id(TestSelector))).Return(this.WebElement);
            this.WebElement.Stub(x => x.Text).Return("abcd");

            var button = this.GetButton(page);

            button.Text.Should().Be("abcd");
            button.Should().NotBeSameAs(this.GetButton(page));
        }
    }
}