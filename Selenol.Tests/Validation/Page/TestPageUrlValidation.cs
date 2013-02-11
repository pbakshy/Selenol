// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using Rhino.Mocks;
using Selenol.Page;
using Selenol.Validation;
using Selenol.Validation.Page;

namespace Selenol.Tests.Validation.Page
{
    [TestFixture]
    public class TestPageUrlValidation
    {
        private IWebDriver webDriver;
        private IJavaScriptExecutor javaScriptExecutor;

        [SetUp]
        public void Init()
        {
            this.webDriver = MockRepository.GenerateStub<IWebDriver>();
            this.javaScriptExecutor = MockRepository.GenerateStub<IJavaScriptExecutor>();
        }

        [Test]
        public void ValidationPasses()
        {
            this.webDriver.Url = "http://mysite/home/page.aspx";

            Assert.DoesNotThrow(() => PageFactory.Create<PageWithSingleValidation>(this.webDriver, this.javaScriptExecutor));
        }

        [Test]
        public void IncorectUrlSingleValidator()
        {
            this.webDriver.Url = "http://mysite/work/index.aspx";

            var excepion = Assert.Throws<PageValidationException>(
                    () => PageFactory.Create<PageWithSingleValidation>(this.webDriver, this.javaScriptExecutor));

            excepion.Message.Should().Be("Current url 'http://mysite/work/index.aspx' does not contain '/home/page.aspx' part.");
        }

        [Test]
        public void IncorectUrlSeveralValidators()
        {
            this.webDriver.Url = "http://mysite/work/index.aspx";

            var excepion = Assert.Throws<PageValidationException>(
                () => PageFactory.Create<PageWithSeveralValidations>(this.webDriver, this.javaScriptExecutor));

            excepion.Message.Should().Contain("Current url 'http://mysite/work/index.aspx' does not contain '/mountains/everest.aspx' part.")
                .And.Contain("Current url 'http://mysite/work/index.aspx' does not contain '/sweets/icecream.aspx' part.")
                .And.Contain("Current url 'http://mysite/work/index.aspx' must be accessed by HTTPS protocol.");
        }

        [Test]
        public void ValidationIsAbsent()
        {
            var exception = Assert.Throws<ValidationAbsenceException>(
                () => PageFactory.Create<PageWithoutValidation>(this.webDriver, this.javaScriptExecutor));

            exception.Message.Should().Be(
                "Page 'Selenol.Tests.Validation.Page.TestPageUrlValidation+PageWithoutValidation' does not have any Url validation. Please add an Url validation.");
        }

        [Test]
        public void HttpsCheckFails()
        {
            this.webDriver.Url = "http://mysite/school/class.aspx";

            var excepion = Assert.Throws<PageValidationException>(
                () => PageFactory.Create<PageWithHttps>(this.webDriver, this.javaScriptExecutor));

            excepion.Message.Should().Be("Current url 'http://mysite/school/class.aspx' must be accessed by HTTPS protocol.");
        }

        [Test]
        public void HttpsCheckPasses()
        {
            this.webDriver.Url = "https://mysite/school/class.aspx";

            Assert.DoesNotThrow(() => PageFactory.Create<PageWithHttps>(this.webDriver, this.javaScriptExecutor));
        }
        
        [TestCase("http://mysite/home/page.aspx")]
        [TestCase("https://mysite/home/page.aspx")]
        public void NoHttpsCheck(string currentUrl)
        {
            this.webDriver.Url = currentUrl;

            Assert.DoesNotThrow(() => PageFactory.Create<PageWithSingleValidation>(this.webDriver, this.javaScriptExecutor));
        }

        public class PageWithoutValidation : BasePage
        {
        }

        [Url("/home/page.aspx", Https = false)]
        public class PageWithSingleValidation : BasePage
        {
        }

        [Url("/school/class.aspx", Https = true)]
        public class PageWithHttps : BasePage
        {
        }

        [Url("/mountains/everest.aspx")]
        [Url("/sweets/icecream.aspx")]
        [Url("/mytest/page.aspx", Https = true)]
        public class PageWithSeveralValidations : BasePage
        {
        }

        public class DerivedPage : PageWithSingleValidation
        {
        }
    }
}