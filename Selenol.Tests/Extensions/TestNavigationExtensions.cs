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
        private const string StartUrl = "http://localhost/current/page.html";
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

        [Test]
        public void GoToValidPageUsingPageObject()
        {
            var currentPage = this.MakeCurrentPage();
            this.StubButton();
            this.StubStartUrl();
            this.webDriver.Stub(x => x.Url).Return(ValidUrl);

            var newPage = currentPage.Go(p => p.Button.Click()).To<SimplePageForTest>();

            newPage.Should().NotBeNull();
        }

        [Test]
        public void GoToInvidPageUsingPageObjectThrowsValidation()
        {
            var currentPage = this.MakeCurrentPage();
            this.StubButton();
            this.StubStartUrl();
            this.webDriver.Stub(x => x.Url).Return(InvalidUrl);

            var exception = Assert.Throws<PageValidationException>(() => currentPage.Go(p => p.Button.Click()).To<SimplePageForTest>());
            exception.Message.Should().Contain("'http://my.home.site/home/index' does not contain '/myhome/page.aspx' part.");
        }

        [Test]
        public void GoToWithoutNavigationUsingPageObjectThrowsValidation()
        {
            var currentPage = this.MakeCurrentPage();
            this.StubButton();
            this.webDriver.Stub(x => x.Url).Return(StartUrl);

            var exception = Assert.Throws<TimeoutException>(() => currentPage.Go(p => p.Button.Click()).To<SimplePageForTest>());
            exception.Message.Should().Contain("Waited for url matched 'SimplePageForTest' page.");
        }

        private CurrentPageForTest MakeCurrentPage()
        {
            this.StubStartUrl();
            return ContainerFactory.Page<CurrentPageForTest>(this.webDriver, this.javaScriptExecutor);
        }

        private void StubStartUrl()
        {
            this.webDriver.Stub(x => x.Url).Return(StartUrl).Repeat.Once();
        }

        private void StubButton()
        {
            this.StubStartUrl();
            var button = MockRepository.GenerateStub<IWebElement>();
            this.webDriver.Stub(x => x.FindElement(By.Id(CurrentPageForTest.ButtonId))).Return(button);
            button.Stub(x => x.TagName).Return("button");
        }

        [Url("/current/page.html")]
        public class CurrentPageForTest : BasePage
        {
            public const string ButtonId = "test-button";

            public ButtonElement Button
            {
                get
                {
                    return this.Button(By.Id(ButtonId));
                }
            }
        }
    }
}