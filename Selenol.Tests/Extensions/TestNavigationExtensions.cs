// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using Rhino.Mocks;
using Selenol.Elements;
using Selenol.Extensions;
using Selenol.Page;
using Selenol.Validation.Page;

namespace Selenol.Tests.Extensions
{
    [TestFixture]
    public class TestNavigationExtensions
    {
        private const string ValidUrl = "http://localhost/myhome/page.aspx?id=1";
        private const string InvalidUrl = "http://my.home.site/home/index";

        private IWebDriver webDriver;
        private IJavaScriptExecutor javaScriptExecutor;
        private INavigation navigation;

        [SetUp]
        public void Init()
        {
            var mock = MockRepository.GenerateMock(typeof(IWebDriver), new[] { typeof(IJavaScriptExecutor) });
            this.webDriver = (IWebDriver)mock;
            this.javaScriptExecutor = (IJavaScriptExecutor)mock;
            this.navigation = MockRepository.GenerateStub<INavigation>();
        }

        [Test]
        public void GoToValidPageUsingWebDriver()
        {
            this.webDriver.Stub(x => x.Url).Return(ValidUrl);
            this.webDriver.Stub(x => x.Navigate()).Return(this.navigation);
            
            var page = this.webDriver.GoTo<SimplePageForTest>(ValidUrl);

            this.navigation.AssertWasCalled(x => x.GoToUrl(ValidUrl));
            page.Should().NotBeNull();
        }

        [Test]
        public void GoToInvalidPageUsingWebDriver()
        {
            this.webDriver.Stub(x => x.Url).Return(InvalidUrl);
            this.webDriver.Stub(x => x.Navigate()).Return(this.navigation);

            Assert.Throws<PageValidationException>(() => this.webDriver.GoTo<SimplePageForTest>(InvalidUrl));
            this.navigation.AssertWasCalled(x => x.GoToUrl(InvalidUrl));
        }

        [Test]
        public void GoToTimeOutUsingWebDriver()
        {
            this.webDriver.Stub(x => x.Url).Return(InvalidUrl);
            this.webDriver.Stub(x => x.Navigate()).Return(this.navigation);

            Assert.Throws<TimeoutException>(() => this.webDriver.GoTo<SimplePageForTest>("http://localhost/myhome/page.aspx?id=1"));
            this.navigation.AssertWasCalled(x => x.GoToUrl(ValidUrl));
        }

        [Test, Ignore("Need to decide whether separate timeout and validation errors or not")]
        public void GoToValidPageUsingPageObject()
        {
            var currentPage = this.MakeCurrentPage();
            this.webDriver.Stub(x => x.Url).Return(ValidUrl);

            var newPage = currentPage.GoTo<CurrentPageForTest, SimplePageForTest>(p => p.Button.Click());

            newPage.Should().NotBeNull();
        }

        [Test, Ignore("Need to decide whether separate timeout and validation errors or not")]
        public void GoToInvidPageUsingPageObjectThrowsTimeout()
        {
            var currentPage = this.MakeCurrentPage();
            this.webDriver.Stub(x => x.Url).Return(InvalidUrl);

            var exception = Assert.Throws<TimeoutException>(() => currentPage.GoTo<CurrentPageForTest, SimplePageForTest>(p => p.Button.Click()));
            exception.Message.Should().Contain("Waited for url matched 'SimplePageForTest' page.");
        }

        private CurrentPageForTest MakeCurrentPage()
        {
            this.webDriver.Stub(x => x.Url).Return("http://localhost/current/page.html").Repeat.Once();
            return PageFactory.Create<CurrentPageForTest>(this.webDriver, this.javaScriptExecutor);
        }

        [Url("/current/page.html")]
        public class CurrentPageForTest : BasePage
        {
            public ButtonElement Button
            {
                get
                {
                    return this.Context.Button(By.Id("test-button"));
                }
            }
        }
    }
}