// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Reflection;
using System.Text.RegularExpressions;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using Rhino.Mocks;
using Selenol.Controls;
using Selenol.Elements;
using Selenol.Page;

namespace Selenol.Tests.SelectorAttributes
{
    public abstract class BaseSelectorAttributeTest : SelectorAttributeTestSupport
    {
        [Test]
        public void CanBeUsedForAutoProperty()
        {
            var page = this.CreatePageUsingFactory("PageWithSelectorAttribute");
            this.AssertCorrectSelectorAttributeUsage(page);
        }

        [Test]
        public void CanBeUsedForControlAutoProperty()
        {
            var page = this.CreatePageUsingFactory("PageWithSelectorAttribute");
            this.AssertCorrectSelectorAttributeUsageForControl(page);
        }

        [Test]
        public void Caching()
        {
            var page = this.CreatePageUsingFactory("PageWithSelectorAttribute");
            this.WebDriver.Stub(x => x.FindElement(this.GetByCriteria(TestSelector))).Return(this.WebElement);
            this.WebElement.Stub(x => x.TagName).Return("select");

            var select = this.GetSelect(page);

            select.Should().BeSameAs(this.GetSelect(page));
        }

        [Test]
        public void CachingControl()
        {
            var page = this.CreatePageUsingFactory("PageWithSelectorAttribute");
            this.WebDriver.Stub(x => x.FindElement(this.GetByCriteria(TestSelector))).Return(this.WebElement);

            var cashedControl = this.GetTableControl(page, "CachedControl");

            cashedControl.Should().BeSameAs(this.GetTableControl(page, "CachedControl"));
        }

        [Test]
        public void ShouldProxyPropertyFromBaseClass()
        {
            var page = this.CreatePageUsingFactory("PageInheritsPropertiesWithSelectorAttribute");
            this.AssertCorrectSelectorAttributeUsage(page);
        }

        [Test]
        public void ShouldProxyControlPropertyFromBaseClass()
        {
            var page = this.CreatePageUsingFactory("PageInheritsPropertiesWithSelectorAttribute");
            this.AssertCorrectSelectorAttributeUsageForControl(page);
        }

        [Test]
        public void ShouldProxyProtectedProperty()
        {
            var page = this.CreatePageUsingFactory("PageWithProtectedProperty");
            this.AssertCorrectSelectorAttributeUsage(page);
        }

        [Test]
        public void ShouldProxyProtectedControlProperty()
        {
            var page = this.CreatePageUsingFactory("PageWithProtectedProperty");
            this.AssertCorrectSelectorAttributeUsageForControl(page);
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
                     .Contain("'InvalidType' property has invalid type. Selector attributes can be used only for properties: " +
                              "\r\n - with type derived from BaseHtmlElement or Contro<T>" +
                              "\r\n - assignable from ReadOnlyCollection<T> where T : BaseHtmlElement or Control<T>")
                     .And
                     .Contain("'AbstractElement' property has invalid type. Selector attributes can not be used for abstract types.")
                     .And
                     .Contain("'AbstractControl' property has invalid type. Selector attributes can not be used for abstract types.")
                     .And
                     .Contain("'NotVirtualProperty' is not virtual. Selector attributes can be used only for virtual properties.")
                     .And
                     .Contain("'PropertyWithoutGetter' is not an auto property. Selector attributes can be used only for auto properties.")
                     .And
                     .Contain("Selector attributes can not be used for private or internal property 'InternalProperty'. Make property public or protected.")
                     .And
                     .Contain("Selector attributes can not be used for private or internal property 'PrivateProperty'. Make property public or protected.");

            AssertSinglePropertyError(realException, "NotAuthoProperty");
            AssertSinglePropertyError(realException, "NotVirtualProperty");
            AssertSinglePropertyError(realException, "'AbstractElement'");
            AssertSinglePropertyError(realException, "'AbstractControl'");
            AssertSinglePropertyError(realException, "InvalidType");
            AssertSinglePropertyError(realException, "InternalProperty");
            AssertSinglePropertyError(realException, "PrivateProperty");
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

        [Test]
        public void ThrowsWhenUsingSetter()
        {
            var page = (BasePageWithWritableProperty)this.CreatePageUsingFactory("PageWithWritableProperty");
            this.WebElement.Stub(x => x.TagName).Return("a");

            var exception = Assert.Throws<InvalidOperationException>(() => page.Link = new LinkElement(this.WebElement));

            exception.Message.Should().Contain("Can not set value for the property 'Link' because it is used with selector attribute.");
        }

        protected abstract By GetByCriteria(string selectorValue);

        private static void AssertSinglePropertyError(Exception exception, string propertyName)
        {
            var count = new Regex(propertyName).Matches(exception.Message).Count;
            count.Should().Be(1);
        }

        private ButtonElement GetButton(SimplePageForTest page)
        {
            return this.GetPropertyValue<ButtonElement>(page, "Button");
        }

        private SelectElement GetSelect(SimplePageForTest page)
        {
            return this.GetPropertyValue<SelectElement>(page, "Select");
        }

        private TableControl GetTableControl(SimplePageForTest page, string propertyName = "TableControl")
        {
            return this.GetPropertyValue<TableControl>(page, propertyName);
        }

        private void AssertCorrectSelectorAttributeUsage(SimplePageForTest page)
        {
            this.WebElement.Stub(x => x.TagName).Return("button");
            this.WebDriver.Stub(x => x.FindElement(this.GetByCriteria(TestSelector))).Return(this.WebElement);
            this.WebElement.Stub(x => x.Text).Return("abcd");

            var button = this.GetButton(page);

            button.Text.Should().Be("abcd");
            button.Should().NotBeSameAs(this.GetButton(page));
        }

        private void AssertCorrectSelectorAttributeUsageForControl(SimplePageForTest page)
        {
            this.WebDriver.Stub(x => x.FindElement(this.GetByCriteria(TestSelector))).Return(this.WebElement);
            this.WebElement.Stub(x => x.Text).Return("table text");
            var tableControl = this.GetTableControl(page);

            tableControl.Text.Should().Be("table text");
            tableControl.Should().NotBeSameAs(this.GetTableControl(page));
        }

        public class BasePageWithWritableProperty : SimplePageForTest
        {
            public virtual LinkElement Link { get; set; }
        }

        public class TableControl : Control
        {
            public TableControl(IWebElement webElement)
                : base(webElement)
            {
            }

            public string Text
            {
                get
                {
                    return this.WebElement.Text;
                }
            }
        }
    }
}