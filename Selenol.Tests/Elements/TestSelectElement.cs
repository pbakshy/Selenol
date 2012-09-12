// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using FluentAssertions;

using NUnit.Framework;

using OpenQA.Selenium;

using Rhino.Mocks;

using Selenol.Elements;

namespace Selenol.Tests.Elements
{
    public class TestSelectElement : BaseHtmlElementTest<SelectElement>
    {
        private IWebElement option1, option2, option3;

        [Test]
        public void GetOptions()
        {
            this.SetSelectedAndOptions();

            //TODO: create a helper with custom comparer
            this.TypedElement.Options
                .Select(x => new Tuple<string, string>(x.Text, x.Value))
                .Should().Equal(new Tuple<string, string>("a", "a1"), new Tuple<string, string>("b", "b2"), new Tuple<string, string>("c", "c3"));
        }

        [Test]
        public void GetSelectedOption()
        {
            this.SetSelectedAndOptions();
            this.TypedElement.SelectedOption.Value.Should().Be("b2");
        }

        [Test]
        public void GetDefaultSelectedOption()
        {
            this.option1.Stub(x => x.GetAttribute("selected")).Return(null);
            this.option2.Stub(x => x.GetAttribute("selected")).Return(null);
            this.option3.Stub(x => x.GetAttribute("selected")).Return(null);

            this.WebElement.Stub(x => x.FindElements(By.TagName("option"))).Return(new ReadOnlyCollection<IWebElement>(new List<IWebElement> { this.option1, this.option2, this.option3 }));
            this.TypedElement.SelectedOption.Value.Should().Be("a1");
        }

        [Test]
        public void GetSelectedOptionFromEmptySelect()
        {
            this.WebElement.Stub(x => x.FindElements(By.TagName("option"))).Return(new ReadOnlyCollection<IWebElement>(new List<IWebElement>()));
            this.TypedElement.SelectedOption.Should().BeNull();
        }

        [Test]
        public void SelectOptionByText()
        {
            this.SetSelectedAndOptions();
            this.TypedElement.SelectOptionByText("c");
            this.option3.AssertWasCalled(x => x.Click());
        }

        [Test, ExpectedException(typeof(ElementNotFoundException))]
        public void SelectOptionByTextMissingElement()
        {
            this.SetSelectedAndOptions();
            this.TypedElement.SelectOptionByText("missing");
        }

        [Test]
        public void SelectOptionByValue()
        {
            this.SetSelectedAndOptions();
            this.TypedElement.SelectOptionByValue("a1");
            this.option1.AssertWasCalled(x => x.Click());
        }

        [Test, ExpectedException(typeof(ElementNotFoundException))]
        public void SelectOptionByValueMissingElement()
        {
            this.SetSelectedAndOptions();
            this.TypedElement.SelectOptionByValue("missing");
        }

        [Test]
        public void SelectOption()
        {
            this.SetSelectedAndOptions();
            this.TypedElement.Options.First().Select();
            this.option1.AssertWasCalled(x => x.Click());
        }

        protected override void OnItit()
        {
            this.option1 = MockRepository.GenerateStub<IWebElement>();
            this.option2 = MockRepository.GenerateStub<IWebElement>();
            this.option3 = MockRepository.GenerateStub<IWebElement>();

            this.option1.Stub(x => x.TagName).Return("option");
            this.option2.Stub(x => x.TagName).Return("option");
            this.option3.Stub(x => x.TagName).Return("option");

            this.option1.Stub(x => x.Text).Return("a");
            this.option2.Stub(x => x.Text).Return("b");
            this.option3.Stub(x => x.Text).Return("c");

            this.option1.Stub(x => x.GetAttribute("value")).Return("a1");
            this.option2.Stub(x => x.GetAttribute("value")).Return("b2");
            this.option3.Stub(x => x.GetAttribute("value")).Return("c3");
        }

        protected override SelectElement CreateElement()
        {
            return new SelectElement(this.WebElement);
        }

        protected override void SetProperElementConditions()
        {
            this.WebElement.Stub(x => x.TagName).Return("select");
        }

        private void SetSelectedAndOptions()
        {
            this.option1.Stub(x => x.GetAttribute("selected")).Return(null);
            this.option2.Stub(x => x.GetAttribute("selected")).Return(string.Empty);
            this.option3.Stub(x => x.GetAttribute("selected")).Return(null);

            this.WebElement.Stub(x => x.FindElements(By.TagName("option"))).Return(new ReadOnlyCollection<IWebElement>(new List<IWebElement> { this.option1, this.option2, this.option3 }));
        }
    }
}