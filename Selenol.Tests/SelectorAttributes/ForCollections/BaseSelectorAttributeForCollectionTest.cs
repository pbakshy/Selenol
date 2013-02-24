// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using Rhino.Mocks;
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
        public void CanBeUsedForICollection()
        {
            this.AssertCorrectSelectorAttributeUsageForCollection<ICollection<FormElement>>("Collection");
        }

        [Test]
        public void CanBeUsedForIList()
        {
            this.AssertCorrectSelectorAttributeUsageForCollection<IList<FormElement>>("List");
        }

        [Test]
        public void CanBeUsedForReadOnlyCollection()
        {
            this.AssertCorrectSelectorAttributeUsageForCollection<ReadOnlyCollection<FormElement>>("ReadOnlyCollection");
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
                         .Contain(
                             "'AbstractCollection' property has invalid type. Generic type argument can not be abstract. For example use ReadOnlyCollection<ButtonElement> instead of ReadOnlyCollection<BaseHtmlElement>");
        }

        protected abstract By GetByCriteria(string selectorValue);

        private string PrepareTypeErrorString(string propertyName)
        {
            return "'{0}' property has invalid type. Selector attributes can be used only for properties with type derived from BaseHtmlElement or assignable from ReadOnlyCollection<T> where T : BaseHtmlElement."
                .F(propertyName);
        }

        private void AssertCorrectSelectorAttributeUsageForCollection<TProperty>(string propertyName) where TProperty : IEnumerable<FormElement>
        {
            var page = this.CreatePageUsingFactory("PageWithCollectionTypeProperties");
            this.WebElement.Stub(x => x.TagName).Return("form");
            this.WebDriver.Stub(x => x.FindElements(this.GetByCriteria(TestSelector)))
                .Return(new ReadOnlyCollection<IWebElement>(new[] { this.WebElement }));
            this.WebElement.Stub(x => x.Text).Return("abcd");

            var forms = this.GetPropertyValue<TProperty>(page, propertyName).ToArray();

            forms.Should().HaveCount(1);
            forms.First().Text.Should().Be("abcd");
            var uncachedElements = this.GetPropertyValue<TProperty>(page, propertyName);
            forms.Should().NotBeEquivalentTo(uncachedElements);
        }
    }
}