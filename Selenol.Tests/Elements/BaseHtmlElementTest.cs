// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System.Linq;

using FluentAssertions;

using NUnit.Framework;

using OpenQA.Selenium;

using Rhino.Mocks;

using Selenol.Elements;

namespace Selenol.Tests.Elements
{
    [TestFixture]
    public abstract class BaseHtmlElementTest<TElement> where TElement : BaseHtmlElement
    {
        protected IWebElement WebElement { get; private set; }

        protected TElement TypedElement { get; private set; }

        [SetUp]
        public void Init()
        {
            this.WebElement = MockRepository.GenerateStub<IWebElement>();
            this.SetProperElementConditions();
            this.TypedElement = this.CreateElement();
        }

        [Test, ExpectedException(typeof(WrongElementException))]
        public void WrongElement()
        {
            this.WebElement = MockRepository.GenerateStub<IWebElement>();
            this.SetWrongElementConditions();
            this.CreateElement();
        }

        [Test]
        public void GetId()
        {
            this.WebElement.Stub(x => x.GetAttribute("id")).Return("test-element");
            this.TypedElement.Id.Should().Be("test-element");
        }

        [Test]
        public void GetNotExistingId()
        {
            this.WebElement.Stub(x => x.GetAttribute("id")).Return(null);
            this.TypedElement.Id.Should().Be(string.Empty);
        }

        [Test]
        public void GetClassesNoneDeclared()
        {
            this.WebElement.Stub(x => x.GetAttribute("class")).Return(null);
            this.TypedElement.Classes.Should().BeEmpty();
        }

        [Test]
        public void GetClassesOnlyOne()
        {
            this.WebElement.Stub(x => x.GetAttribute("class")).Return("web-element");
            this.TypedElement.Classes.Should().Equal(new[] { "web-element" }.AsEnumerable());
        }

        [Test]
        public void GetClassesSeveral()
        {
            this.WebElement.Stub(x => x.GetAttribute("class")).Return(" web element  some-other  ");
            this.TypedElement.Classes.Should().Equal(new[] { "web", "element", "some-other" }.AsEnumerable());
        }

        [Test]
        public void HasClass()
        {
            this.WebElement.Stub(x => x.GetAttribute("class")).Return(" web element  some-other  ");
            this.TypedElement.HasClass("web").Should().BeTrue();
            this.TypedElement.HasClass("visual").Should().BeFalse();
        }

        [Test]
        public void GetAttributeValue()
        {
            this.WebElement.Stub(x => x.GetAttribute("attr1")).Return("text");
            this.WebElement.Stub(x => x.GetAttribute("attr2")).Return(string.Empty);
            this.WebElement.Stub(x => x.GetAttribute("attr3")).Return(null);

            this.TypedElement.GetAttributeValue("attr1").Should().Be("text");
            this.TypedElement.GetAttributeValue("attr2").Should().Be(string.Empty);
            this.TypedElement.GetAttributeValue("attr3").Should().Be(string.Empty);
        }

        [Test]
        public void HasAttribute()
        {
            this.WebElement.Stub(x => x.GetAttribute("attr1")).Return("text");
            this.WebElement.Stub(x => x.GetAttribute("attr2")).Return(string.Empty);
            this.WebElement.Stub(x => x.GetAttribute("attr3")).Return(null);

            this.TypedElement.HasAttribute("attr1").Should().BeTrue();
            this.TypedElement.HasAttribute("attr2").Should().BeTrue();
            this.TypedElement.HasAttribute("attr3").Should().BeFalse();
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void GetIsDisplayed(bool wrappedValue)
        {
            this.WebElement.Stub(x => x.Displayed).Return(wrappedValue);
            this.TypedElement.IsDisplayed.Should().Be(wrappedValue);
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void GetIsEnabled(bool wrappedValue)
        {
            this.WebElement.Stub(x => x.Enabled).Return(wrappedValue);
            this.TypedElement.IsEnabled.Should().Be(wrappedValue);
        }

        [Test]
        public void GetName()
        {
            this.WebElement.Stub(x => x.GetAttribute("name")).Return("test-name");
            this.TypedElement.Name.Should().Be("test-name");
        }

        [Test]
        public void GetNotExistingName()
        {
            this.WebElement.Stub(x => x.GetAttribute("name")).Return(null);
            this.TypedElement.Name.Should().Be(string.Empty);
        }

        protected abstract TElement CreateElement();

        protected abstract void SetProperElementConditions();

        protected virtual void SetWrongElementConditions()
        {
            this.WebElement.Stub(x => x.TagName).Return("p");
        }
    }
}