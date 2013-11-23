// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium.Remote;
using Selenol.Extensions;
using Selenol.FunctionalTests.PageObjects;
using Selenol.Page;
using TechTalk.SpecFlow;

namespace Selenol.FunctionalTests.Steps
{
    [Binding]
    public class NavigationSteps
    {
        private BasePage currentPage;
        private Exception catchedNavigationException;

        [When(@"I go to ""Elements"" page by ""(.*)"" url")]
        public void WhenITryGoToPage(string urlPart)
        {
            try
            {
                var webDriver = (RemoteWebDriver)Browser.Current;
                this.currentPage = webDriver.GoTo<ElementsPage>("http://localhost:{0}/{1}".FInv(Configuration.ServerPort, urlPart));
            }
            catch (Exception exception)
            {
                this.catchedNavigationException = exception;
            }
        }

        [Then(@"I should get instance of ""(.*)""")]
        public void ThenIShouldGetInstanceOf(string pageTypeName)
        {
            var pageType = Type.GetType("Selenol.FunctionalTests.PageObjects.{0}".FInv(pageTypeName));

            Assert.IsNotNull(pageType);
            pageType.IsInstanceOfType(this.currentPage).Should().BeTrue();
        }

        [Then(@"I should get ""(.*)"" exception")]
        public void ThenIShouldGet(string exceptionName)
        {
            Assert.IsNotNull(this.catchedNavigationException);
            this.catchedNavigationException.GetType().Name.Should().Be(exceptionName);
        }
    }
}