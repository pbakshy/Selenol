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
using Selenol.Validation;
using Selenol.Validation.Element;

namespace Selenol.Tests.Elements
{
    [TestFixture]
    public class TestSearchContextExtensions
    {
        private ISearchContext context;

        private IWebElement element1, element2;

        [SetUp]
        public void Init()
        {
            this.context = MockRepository.GenerateStub<ISearchContext>();
            this.element1 = MockRepository.GenerateStub<IWebElement>();
            this.element2 = MockRepository.GenerateStub<IWebElement>();

            this.element1.Stub(x => x.GetAttribute("id")).Return("e1");
            this.element2.Stub(x => x.GetAttribute("id")).Return("e2");
        }

        [Test, TestCaseSource("SingleElementFactory")] 
        public void SingleElement(Func<ISearchContext, By, BaseHtmlElement> method, string tag, string type)
        {
            var selector = By.CssSelector("test");
            this.context.Stub(x => x.FindElement(selector)).Return(this.element1);
            this.element1.Stub(x => x.TagName).Return(tag);
            if (type != null)
            {
                this.element1.Stub(x => x.GetAttribute("type")).Return(type);
            }

            var typedElement = method(this.context, selector);
            typedElement.Should().NotBeNull();
            typedElement.Id.Should().Be("e1");
            this.context.AssertWasCalled(x => x.FindElement(selector));
        }

        [Test, TestCaseSource("MultiElementsFactory")]
        public void MultiElements(Func<ISearchContext, By, IEnumerable<BaseHtmlElement>> method, string tag, string type)
        {
            var selector = By.CssSelector("test");
            this.context.Stub(x => x.FindElements(selector))
                .Return(new ReadOnlyCollection<IWebElement>(new List<IWebElement> { this.element1, this.element2 }));

            this.element1.Stub(x => x.TagName).Return(tag);
            this.element2.Stub(x => x.TagName).Return(tag);
            if (type != null)
            {
                this.element1.Stub(x => x.GetAttribute("type")).Return(type);
                this.element2.Stub(x => x.GetAttribute("type")).Return(type);
            }

            method(this.context, selector).Select(x => x.Id).Should().Equal(new[] { "e1", "e2" }.AsEnumerable());
            this.context.AssertWasCalled(x => x.FindElements(selector));
        }

        [Test, TestCaseSource("MultiElementsDefaultFactory")]
        public void MultiElementsDefault(Func<ISearchContext, IEnumerable<BaseHtmlElement>> method, string cssSelector, string tag, string type)
        {
            var selector = By.CssSelector(cssSelector);
            this.context.Stub(x => x.FindElements(selector)).Return(new List<IWebElement> { this.element1, this.element2 }.AsReadOnly());

            this.element1.Stub(x => x.TagName).Return(tag);
            this.element2.Stub(x => x.TagName).Return(tag);
            if (type != null)
            {
                this.element1.Stub(x => x.GetAttribute("type")).Return(type);
                this.element2.Stub(x => x.GetAttribute("type")).Return(type);
            }

            method(this.context).Select(x => x.Id).Should().Equal(new[] { "e1", "e2" }.AsEnumerable());
            this.context.AssertWasCalled(x => x.FindElements(selector));
        }

        [Test, ExpectedException(typeof(ValidationAbsenceException))]
        public void FindUserControlWithoutVerification()
        {
            var selector = By.CssSelector("div");
            this.context.Stub(x => x.FindElement(selector)).Return(this.element1);
            this.context.Control<UserControlForTestWithoutVerification>(selector);
        }

        [Test, ExpectedException(typeof(ValidationAbsenceException))]
        public void FindUserControlsWithoutVerification()
        {
            var selector = By.CssSelector("div");
            this.context.Stub(x => x.FindElements(selector)).Return(new List<IWebElement> { this.element1, this.element2 }.AsReadOnly());
            this.context.Controls<UserControlForTestWithoutVerification>(selector);
        }

        [Test, ExpectedException(typeof(MissingMethodException))]
        public void FindUserControlWithoutProperConctructor()
        {
            var selector = By.CssSelector("div");
            this.context.Stub(x => x.FindElement(selector)).Return(this.element1);
            this.context.Control<UserControlForTestWithoutProperConstructor>(selector);
        }

        [Test, ExpectedException(typeof(MissingMethodException))]
        public void FindUserControlsWithoutProperConctructor()
        {
            var selector = By.CssSelector("div");
            this.context.Stub(x => x.FindElements(selector)).Return(new List<IWebElement> { this.element1, this.element2 }.AsReadOnly());
            this.context.Controls<UserControlForTestWithoutProperConstructor>(selector);
        }

        protected static IEnumerable<TestCaseData> SingleElementFactory()
        {
            yield return new TestCaseData(new Func<ISearchContext, By, BaseHtmlElement>((sc, by) => sc.Button(by)), "button", null);
            yield return new TestCaseData(new Func<ISearchContext, By, BaseHtmlElement>((sc, by) => sc.Checkbox(by)), "input", "checkbox");
            yield return new TestCaseData(new Func<ISearchContext, By, BaseHtmlElement>((sc, by) => sc.Element(by)), "p", null);
            yield return new TestCaseData(new Func<ISearchContext, By, BaseHtmlElement>((sc, by) => sc.FileUpload(by)), "input", "file");
            yield return new TestCaseData(new Func<ISearchContext, By, BaseHtmlElement>((sc, by) => sc.Form(by)), "form", null);
            yield return new TestCaseData(new Func<ISearchContext, By, BaseHtmlElement>((sc, by) => sc.Link(by)), "a", null);
            yield return new TestCaseData(new Func<ISearchContext, By, BaseHtmlElement>((sc, by) => sc.Listbox(by)), "select", null);
            yield return new TestCaseData(new Func<ISearchContext, By, BaseHtmlElement>((sc, by) => sc.RadioButton(by)), "input", "radio");
            yield return new TestCaseData(new Func<ISearchContext, By, BaseHtmlElement>((sc, by) => sc.Select(by)), "select", null);
            yield return new TestCaseData(new Func<ISearchContext, By, BaseHtmlElement>((sc, by) => sc.Table(by)), "table", null);
            yield return new TestCaseData(new Func<ISearchContext, By, BaseHtmlElement>((sc, by) => sc.TextArea(by)), "textarea", null);
            yield return new TestCaseData(new Func<ISearchContext, By, BaseHtmlElement>((sc, by) => sc.Textbox(by)), "input", "text");
            yield return new TestCaseData(new Func<ISearchContext, By, BaseHtmlElement>((sc, by) => sc.Control<UserControlForTest>(by)), "div", null);
        }

        protected static IEnumerable<TestCaseData> MultiElementsFactory()
        {
            yield return new TestCaseData(new Func<ISearchContext, By, IEnumerable<BaseHtmlElement>>((sc, by) => sc.Buttons(by)), "button", null);
            yield return new TestCaseData(new Func<ISearchContext, By, IEnumerable<BaseHtmlElement>>((sc, by) => sc.Checkboxes(by)), "input", "checkbox");
            yield return new TestCaseData(new Func<ISearchContext, By, IEnumerable<BaseHtmlElement>>((sc, by) => sc.Elements(by)), "p", null);
            yield return new TestCaseData(new Func<ISearchContext, By, IEnumerable<BaseHtmlElement>>((sc, by) => sc.FileUploads(by)), "input", "file");
            yield return new TestCaseData(new Func<ISearchContext, By, IEnumerable<BaseHtmlElement>>((sc, by) => sc.Forms(by)), "form", null);
            yield return new TestCaseData(new Func<ISearchContext, By, IEnumerable<BaseHtmlElement>>((sc, by) => sc.Links(by)), "a", null);
            yield return new TestCaseData(new Func<ISearchContext, By, IEnumerable<BaseHtmlElement>>((sc, by) => sc.Listboxes(by)), "select", null);
            yield return new TestCaseData(new Func<ISearchContext, By, IEnumerable<BaseHtmlElement>>((sc, by) => sc.RadioButtons(by)), "input", "radio");
            yield return new TestCaseData(new Func<ISearchContext, By, IEnumerable<BaseHtmlElement>>((sc, by) => sc.Selects(by)), "select", null);
            yield return new TestCaseData(new Func<ISearchContext, By, IEnumerable<BaseHtmlElement>>((sc, by) => sc.Tables(by)), "table", null);
            yield return new TestCaseData(new Func<ISearchContext, By, IEnumerable<BaseHtmlElement>>((sc, by) => sc.TextAreas(by)), "textarea", null);
            yield return new TestCaseData(new Func<ISearchContext, By, IEnumerable<BaseHtmlElement>>((sc, by) => sc.Textboxes(by)), "input", "text");
            yield return new TestCaseData(new Func<ISearchContext, By, IEnumerable<BaseHtmlElement>>((sc, by) => sc.Controls<UserControlForTest>(by)), "div", null);
        }

        protected static IEnumerable<TestCaseData> MultiElementsDefaultFactory()
        {
            yield return new TestCaseData(
                    new Func<ISearchContext, IEnumerable<BaseHtmlElement>>(x => x.Buttons()),
                    "button,input[type='button']",
                    "button",
                    null);

            yield return new TestCaseData(
                    new Func<ISearchContext, IEnumerable<BaseHtmlElement>>(x => x.Checkboxes()),
                    "input[type='checkbox']",
                    "input",
                    "checkbox");

            yield return new TestCaseData(new Func<ISearchContext, IEnumerable<BaseHtmlElement>>(x => x.Elements()), "*", "p", null);
            yield return new TestCaseData(new Func<ISearchContext, IEnumerable<BaseHtmlElement>>(x => x.FileUploads()), "input[type='file']", "input", "file");
            yield return new TestCaseData(new Func<ISearchContext, IEnumerable<BaseHtmlElement>>(x => x.Forms()), "form", "form", null);
            yield return new TestCaseData(new Func<ISearchContext, IEnumerable<BaseHtmlElement>>(x => x.Links()), "a", "a", null);
            yield return new TestCaseData(new Func<ISearchContext, IEnumerable<BaseHtmlElement>>(x => x.Listboxes()), "select[multiple]", "select", null);
            yield return new TestCaseData(new Func<ISearchContext, IEnumerable<BaseHtmlElement>>(x => x.RadioButtons()), "input[type='radio']", "input", "radio");
            yield return new TestCaseData(new Func<ISearchContext, IEnumerable<BaseHtmlElement>>(x => x.Selects()), "select:not([multiple])", "select", null);
            yield return new TestCaseData(new Func<ISearchContext, IEnumerable<BaseHtmlElement>>(x => x.Tables()), "table", "table", null);
            yield return new TestCaseData(new Func<ISearchContext, IEnumerable<BaseHtmlElement>>(x => x.TextAreas()), "textarea", "textarea", null);
            yield return new TestCaseData(new Func<ISearchContext, IEnumerable<BaseHtmlElement>>(x => x.Textboxes()), "input[type='text']", "input", "text");
        }

// ReSharper disable ClassNeverInstantiated.Local
        [Tag("div")]
        private class UserControlForTest : BaseHtmlElement
        {
            public UserControlForTest(IWebElement webElement)
                : base(webElement)
            {
            }
        }

        private class UserControlForTestWithoutVerification : BaseHtmlElement
        {
            public UserControlForTestWithoutVerification(IWebElement webElement)
                : base(webElement)
            {
            }
        }
        
// ReSharper disable UnusedParameter.Local
        [Tag("div")]
        private class UserControlForTestWithoutProperConstructor : BaseHtmlElement
        {
            public UserControlForTestWithoutProperConstructor(IWebElement webElement, int index)
                : base(webElement)
            {
            }
        }
// ReSharper restore UnusedParameter.Local
// ReSharper restore ClassNeverInstantiated.Local
    }
}