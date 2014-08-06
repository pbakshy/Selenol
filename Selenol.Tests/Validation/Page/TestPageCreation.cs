// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using Rhino.Mocks;
using Selenol.Page;
using Selenol.Validation.Page;

namespace Selenol.Tests.Validation.Page
{
    [TestFixture]
    public class TestPageCreation
    {
        private BasePage page;
        private IWebDriver webDriver;
        private IJavaScriptExecutor javaScriptExecutor;

        [SetUp]
        public void Init()
        {
            this.webDriver = MockRepository.GenerateStub<IWebDriver>();
            this.javaScriptExecutor = MockRepository.GenerateStub<IJavaScriptExecutor>();
            this.webDriver.Url = "http://supersite.com/myhome/page.aspx";
            this.page = ContainerFactory.Create<SimplePageForTest>(this.webDriver, this.javaScriptExecutor);
        }

        [Test, TestCaseSource("WithValidationActionsFactory")]
        public void ValidationPasses(Action<BasePage> action)
        {
            Assert.DoesNotThrow(() => action(this.page));
        }

        [Test, TestCaseSource("WithValidationActionsFactory")]
        public void ValidationFailsAfterNavigationToOtherPage(Action<BasePage> action)
        {
            this.webDriver.Url = "http://selenol.com/work/page.aspx";
            
            Assert.Throws<PageValidationException>(() => action(this.page));
        }

        [Test]
        public void ExecuteJavascript()
        {
            this.javaScriptExecutor.Stub(x => x.ExecuteScript("return 'Hello world'")).Return("Hello world");
            
            var jsResult = this.page.ExecuteScript("return 'Hello world'");

            jsResult.Should().Be("Hello world");
        }

        [Test]
        public void ExecuteAsyncJavascript()
        {
            this.javaScriptExecutor.Stub(x => x.ExecuteAsyncScript("return 1 === '1'")).Return(false);

            var jsResult = this.page.ExecuteAsyncScript("return 1 === '1'");

            jsResult.Should().Be(false);
        }

        protected static IEnumerable<TestCaseData> WithValidationActionsFactory()
        {
            // ReSharper disable UnusedVariable
            yield return new TestCaseData(new Action<BasePage>(x => { var context = x.Context; }));
            yield return new TestCaseData(new Action<BasePage>(x => { var result = x.ExecuteScript("return 1 == 1"); }));
            yield return new TestCaseData(new Action<BasePage>(x => { var result = x.ExecuteAsyncScript("return '1' === 1"); }));
            // ReSharper restore UnusedVariable
        }
    }
}