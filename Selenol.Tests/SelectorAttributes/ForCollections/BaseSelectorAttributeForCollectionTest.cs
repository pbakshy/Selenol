// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using Rhino.Mocks;
using Selenol.Controls;
using Selenol.Elements;
using Selenol.Extensions;
using Selenol.Page;

namespace Selenol.Tests.SelectorAttributes.ForCollections
{
    public abstract class BaseSelectorAttributeForCollectionTest : SelectorAttributeTestSupport
    {
        [Test]
        public void CanBeUsedForIEnumerable()
        {
            this.AssertCorrectSelectorAttributeUsageForCollection<IEnumerable<FormElement>>("Enumerable");
        }

        [Test]
        public void CanBeUsedForIEnumerableOfControls()
        {
            this.AssertCorrectSelectorAttributeUsageForCollection<IEnumerable<FormControl>>("ControlEnumerable");
        }

        [Test]
        public void CanBeUsedForICollection()
        {
            this.AssertCorrectSelectorAttributeUsageForCollection<ICollection<FormElement>>("Collection");
        }

        [Test]
        public void CanBeUsedForICollectionOfControls()
        {
            this.AssertCorrectSelectorAttributeUsageForCollection<ICollection<FormControl>>("ControlCollection");
        }

        [Test]
        public void CanBeUsedForIList()
        {
            this.AssertCorrectSelectorAttributeUsageForCollection<IList<FormElement>>("List");
        }

        [Test]
        public void CanBeUsedForIListOfControls()
        {
            this.AssertCorrectSelectorAttributeUsageForCollection<IList<FormControl>>("ControlList");
        }

        [Test]
        public void CanBeUsedForReadOnlyCollection()
        {
            this.AssertCorrectSelectorAttributeUsageForCollection<ReadOnlyCollection<FormElement>>("ReadOnlyCollection");
        }

        [Test]
        public void CanBeUsedForControlReadOnlyCollection()
        {
            this.AssertCorrectSelectorAttributeUsageForCollection<ReadOnlyCollection<FormControl>>("ControlReadOnlyCollection");
        }

        [Test]
        public void Caching()
        {
            var page = this.CreatePageUsingFactory("PageWithCollectionTypeProperties");
            this.WebElement.Stub(x => x.TagName).Return("a");
            this.WebDriver.Stub(x => x.FindElements(this.GetByCriteria(TestSelector)))
                .Return(new ReadOnlyCollection<IWebElement>(new[] { this.WebElement }));

            var links = this.GetPropertyValue<IEnumerable<LinkElement>>(page, "Links");

            var cachedLinks = this.GetPropertyValue<IEnumerable<LinkElement>>(page, "Links");
            Assert.AreSame(links, cachedLinks);
        }

        [Test]
        public void IncorrectAttributeUsage()
        {
            var realException = Assert.Throws<TargetInvocationException>(() => this.CreatePageUsingFactory("PageWithIncorrectPropertyCollectionTypes"))
                                      .InnerException;
            realException.Should().BeOfType<PageInitializationException>();
            realException.Message.Should()
                         .Contain(this.PrepareTypeErrorString("Array"))
                         .And
                         .Contain(this.PrepareTypeErrorString("List"))
                         .And
                         .Contain(this.PrepareTypeErrorString("Enumerable"))
                         .And
                         .Contain("'AbstractElementCollection' property has invalid type. Generic type argument can not be abstract. " +
                                  "For Elements use ReadOnlyCollection<ButtonElement> instead of ReadOnlyCollection<BaseHtmlElement>. " +
                                  "For Controls use non abstract Control type as generic argument for collection.")
                         .And
                         .Contain("'AbstractControlCollection' property has invalid type. Generic type argument can not be abstract. " +
                                  "For Elements use ReadOnlyCollection<ButtonElement> instead of ReadOnlyCollection<BaseHtmlElement>. " +
                                  "For Controls use non abstract Control type as generic argument for collection.");
        }

        [Test]
        public void ThrowsWhenUsingSetter()
        {
            var page = (BasePageWithWritableProperty)this.CreatePageUsingFactory("PageWithWritableProperty");
            this.WebElement.Stub(x => x.TagName).Return("input");
            this.WebElement.Stub(x => x.GetAttribute("type")).Return("radio");

            var exception = Assert.Throws<InvalidOperationException>(() => page.RadioButtons = new[] { new RadioButtonElement(this.WebElement) });

            exception.Message.Should().Contain("Can not set value for the property 'RadioButtons' because it is used with selector attribute.");
        }

        protected abstract By GetByCriteria(string selectorValue);

        private string PrepareTypeErrorString(string propertyName)
        {
            return "'{0}' property has invalid type. Selector attributes can be used only for properties: \r\n - with type derived from BaseHtmlElement or Contro<T>\r\n - assignable from ReadOnlyCollection<T> where T : BaseHtmlElement or Control<T>"
                .F(propertyName);
        }

        private void AssertCorrectSelectorAttributeUsageForCollection<TProperty>(string propertyName) where TProperty : IEnumerable<BaseHtmlElement>
        {
            var page = this.CreatePageUsingFactory("PageWithCollectionTypeProperties");
            this.WebElement.Stub(x => x.TagName).Return("form");
            this.WebDriver.Stub(x => x.FindElements(this.GetByCriteria(TestSelector)))
                .Return(new ReadOnlyCollection<IWebElement>(new[] { this.WebElement }));
            this.WebElement.Stub(x => x.Displayed).Return(true);

            var forms = this.GetPropertyValue<TProperty>(page, propertyName).ToArray();

            forms.Should().HaveCount(1);
            forms.First().IsDisplayed.Should().Be(true);
            var uncachedElements = this.GetPropertyValue<TProperty>(page, propertyName);
            forms.Should().NotBeEquivalentTo(uncachedElements);
        }

        public class BasePageWithWritableProperty : SimplePageForTest
        {
            public virtual IEnumerable<RadioButtonElement> RadioButtons { get; set; }
        }

        public class FormControl : Control
        {
            public FormControl(IWebElement webElement)
                : base(webElement)
            {
            }
        }
    }
}