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
            this.TypedElement = this.CreateElement();
        }

        [Test]
        public void GetId()
        {
            this.WebElement.Expect(x => x.GetAttribute("id")).Return("test-element");
            this.TypedElement.Id.Should().Be("test-element");
        }

        [Test]
        public void GetClassesNoneDeclared()
        {
            this.WebElement.Expect(x => x.GetAttribute("class")).Return(null);
            this.TypedElement.Classes.Should().BeEmpty();
        }

        [Test]
        public void GetClassesOnlyOne()
        {
            this.WebElement.Expect(x => x.GetAttribute("class")).Return("web-element");
            this.TypedElement.Classes.Should().Equal(new[] { "web-element" }.AsEnumerable());
        }

        [Test]
        public void GetClassesSeveral()
        {
            this.WebElement.Expect(x => x.GetAttribute("class")).Return(" web element  some-other  ");
            this.TypedElement.Classes.Should().Equal(new[] { "web", "element", "some-other" }.AsEnumerable());
        }

        [Test]
        public void HasClass()
        {
            this.WebElement.Expect(x => x.GetAttribute("class")).Return(" web element  some-other  ");
            this.TypedElement.HasClass("web").Should().BeTrue();
            this.TypedElement.HasClass("visual").Should().BeFalse();
        }

        [Test]
        public void GetAttributeValue()
        {
            this.WebElement.Expect(x => x.GetAttribute("attr1")).Return("text");
            this.WebElement.Expect(x => x.GetAttribute("attr2")).Return(string.Empty);
            this.WebElement.Expect(x => x.GetAttribute("attr3")).Return(null);

            this.TypedElement.GetAttributeValue("attr1").Should().Be("text");
            this.TypedElement.GetAttributeValue("attr2").Should().Be(string.Empty);
            this.TypedElement.GetAttributeValue("attr3").Should().Be(string.Empty);
        }

        [Test]
        public void HasAttribute()
        {
            this.WebElement.Expect(x => x.GetAttribute("attr1")).Return("text");
            this.WebElement.Expect(x => x.GetAttribute("attr2")).Return(string.Empty);
            this.WebElement.Expect(x => x.GetAttribute("attr3")).Return(null);

            this.TypedElement.HasAttribute("attr1").Should().BeTrue();
            this.TypedElement.HasAttribute("attr2").Should().BeTrue();
            this.TypedElement.HasAttribute("attr3").Should().BeFalse();
        }

        protected abstract TElement CreateElement();
    }
}