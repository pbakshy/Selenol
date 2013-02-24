// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Reflection;
using System.Text.RegularExpressions;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using Rhino.Mocks;
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
        public void Caching()
        {
            var page = this.CreatePageUsingFactory("PageWithSelectorAttribute");
            this.WebDriver.Stub(x => x.FindElement(this.GetByCriteria(TestSelector))).Return(this.WebElement);
            this.WebElement.Stub(x => x.TagName).Return("select");

            var select = this.GetSelect(page);

            select.Should().BeSameAs(this.GetSelect(page));
        }

        [Test]
        public void ShouldProxyPropertyFromBaseClass()
        {
            var page = this.CreatePageUsingFactory("PageInheritsPropertiesWithSelectorAttribute");
            this.AssertCorrectSelectorAttributeUsage(page);
        }

        [Test]
        public void ShouldProxyProtectedProperty()
        {
            var page = this.CreatePageUsingFactory("PageWithProtectedProperty");
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
                         "'NotElement' property has invalid type. Selector attributes can be used only for properties with type derived from BaseHtmlElement or assignable from ReadOnlyCollection<T> where T : BaseHtmlElement.")
                     .And
                     .Contain("'AbstractElement' property has invalid type. Selector attributes can not be used for abstract types.")
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
            AssertSinglePropertyError(realException, "AbstractElement");
            AssertSinglePropertyError(realException, "NotElement");
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

        private void AssertCorrectSelectorAttributeUsage(SimplePageForTest page)
        {
            this.WebElement.Stub(x => x.TagName).Return("button");
            this.WebDriver.Stub(x => x.FindElement(this.GetByCriteria(TestSelector))).Return(this.WebElement);
            this.WebElement.Stub(x => x.Text).Return("abcd");

            var button = this.GetButton(page);

            button.Text.Should().Be("abcd");
            button.Should().NotBeSameAs(this.GetButton(page));
        }
    }
}