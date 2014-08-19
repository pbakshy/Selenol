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
    public abstract class BaseSelectElementTest<TSelectElement> : BaseHtmlElementTest<TSelectElement> where TSelectElement : BaseSelectElement
    {
        protected IWebElement Option1 { get; private set; }

        protected IWebElement Option2 { get; private set; }

        protected IWebElement Option3 { get; private set; }

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
        public void SelectOptionByText()
        {
            this.SetSelectedAndOptions();
            this.TypedElement.SelectOptionByText("c");

            this.Option3.AssertWasCalled(x => x.Click());
            this.Option1.AssertWasNotCalled(x => x.Click());
            this.Option2.AssertWasNotCalled(x => x.Click());
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
            this.Option1.AssertWasCalled(x => x.Click());
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
            this.Option1.AssertWasCalled(x => x.Click());
        }

        protected override void OnItit()
        {
            this.Option1 = MockRepository.GenerateStub<IWebElement>();
            this.Option2 = MockRepository.GenerateStub<IWebElement>();
            this.Option3 = MockRepository.GenerateStub<IWebElement>();

            this.Option1.Stub(x => x.TagName).Return("option");
            this.Option2.Stub(x => x.TagName).Return("option");
            this.Option3.Stub(x => x.TagName).Return("option");

            this.Option1.Stub(x => x.Text).Return("a");
            this.Option2.Stub(x => x.Text).Return("b");
            this.Option3.Stub(x => x.Text).Return("c");

            this.Option1.Stub(x => x.GetAttribute("value")).Return("a1");
            this.Option2.Stub(x => x.GetAttribute("value")).Return("b2");
            this.Option3.Stub(x => x.GetAttribute("value")).Return("c3");
        }

        protected override void SetProperElementConditions()
        {
            this.WebElement.Stub(x => x.TagName).Return("select");
        }

        protected void SetSelectedAndOptions()
        {
            this.Option1.Stub(x => x.GetAttribute("selected")).Return(null);
            this.Option2.Stub(x => x.GetAttribute("selected")).Return(string.Empty);
            this.Option3.Stub(x => x.GetAttribute("selected")).Return(null);

            this.WebElement.Stub(x => x.FindElements(By.TagName("option"))).Return(new ReadOnlyCollection<IWebElement>(new List<IWebElement> { this.Option1, this.Option2, this.Option3 }));
        }
    }
}