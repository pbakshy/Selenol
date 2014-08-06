// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using Rhino.Mocks;
using Selenol.Controls;
using Selenol.Elements;
using Selenol.Page;
using Selenol.SelectorAttributes;

namespace Selenol.Tests.SelectorAttributes
{
    [TestFixture]
    public class TestControlCreation
    {
        private IWebElement containerElement;

        [SetUp]
        public void Init()
        {
            this.containerElement = MockRepository.GenerateStub<IWebElement>();
        }

        [Test]
        public void CreateAbstractControl()
        {
            Assert.Throws<ArgumentException>(() => ContainerFactory.Control<Control>(this.containerElement));
        }

        [Test]
        public void ProxiesElementProperty()
        {
            var backButtonElement = MockRepository.GenerateStub<IWebElement>();
            this.containerElement.Stub(x => x.FindElement(By.Id("back-button"))).Return(backButtonElement);
            backButtonElement.Stub(x => x.TagName).Return("button");
            backButtonElement.Stub(x => x.Text).Return("Back button");

            var control = ContainerFactory.Control<TestTableControl>(this.containerElement);

            control.Button.Text.Should().Be("Back button");
        }

        [Test]
        public void ProxiesElementCollectionProperty()
        {
            var textbox1 = MockRepository.GenerateStub<IWebElement>();
            var textbox2 = MockRepository.GenerateStub<IWebElement>();
            this.containerElement.Stub(x => x.FindElements(By.ClassName("simple-text")))
                .Return(new ReadOnlyCollection<IWebElement>(new[] { textbox1, textbox2 }));
            textbox1.Stub(x => x.TagName).Return("input");
            textbox1.Stub(x => x.GetAttribute("type")).Return("text");
            textbox2.Stub(x => x.TagName).Return("input");
            textbox2.Stub(x => x.GetAttribute("type")).Return("text");
            textbox1.Stub(x => x.GetAttribute("value")).Return("text 1");
            textbox2.Stub(x => x.GetAttribute("value")).Return("text 2");

            var control = ContainerFactory.Control<TestTableControl>(this.containerElement);

            control.Textboxes.First().Text.Should().Be("text 1");
            control.Textboxes.Skip(1).First().Text.Should().Be("text 2");
        }

        [Test]
        public void ProxiesControlProperty()
        {
            var controlElement = MockRepository.GenerateStub<IWebElement>();
            this.containerElement.Stub(x => x.FindElement(By.Id("first-row"))).Return(controlElement);
            controlElement.Stub(x => x.TagName).Return("tr");
            var checkbox = MockRepository.GenerateStub<IWebElement>();
            controlElement.Stub(x => x.FindElement(By.ClassName("cb"))).Return(checkbox);
            checkbox.Stub(x => x.TagName).Return("input");
            checkbox.Stub(x => x.GetAttribute("type")).Return("checkbox");
            checkbox.Stub(x => x.GetAttribute("checked")).Return("checked");

            var control = ContainerFactory.Control<TestTableControl>(this.containerElement);

            control.FirstRow.Checkbox.IsChecked.Should().Be(true);
        }

        [Test]
        public void ProxiesControlCollectionProperty()
        {
            var controlElement = MockRepository.GenerateStub<IWebElement>();
            this.containerElement.Stub(x => x.FindElements(By.TagName("tr"))).Return(new ReadOnlyCollection<IWebElement>(new[] { controlElement }));
            controlElement.Stub(x => x.TagName).Return("tr");
            var checkbox = MockRepository.GenerateStub<IWebElement>();
            controlElement.Stub(x => x.FindElement(By.ClassName("cb"))).Return(checkbox);
            checkbox.Stub(x => x.TagName).Return("input");
            checkbox.Stub(x => x.GetAttribute("type")).Return("checkbox");
            checkbox.Stub(x => x.GetAttribute("value")).Return("checkbox true value");

            var control = ContainerFactory.Control<TestTableControl>(this.containerElement);

            control.Rows.First().Checkbox.Value.Should().Be("checkbox true value");
        }

        public class TestTableControl : Control
        {
            public TestTableControl(IWebElement webElement)
                : base(webElement)
            {
            }

            [Id("back-button")]
            public virtual ButtonElement Button { get; set; }

            [Class("simple-text")]
            public virtual IEnumerable<TextboxElement> Textboxes { get; set; }

            [Id("first-row")]
            public virtual TestRowControl FirstRow { get; set; }

            [TagName("tr")]
            public virtual ICollection<TestRowControl> Rows { get; set; }
        }

        public class TestRowControl : Control
        {
            public TestRowControl(IWebElement webElement)
                : base(webElement)
            {
            }

            [Class("cb")]
            public virtual CheckboxElement Checkbox { get; set; }
        }
    }
}