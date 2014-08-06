// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Reflection;
using NUnit.Framework;
using OpenQA.Selenium;
using Rhino.Mocks;
using Selenol.Extensions;
using Selenol.Page;

namespace Selenol.Tests.SelectorAttributes
{
    public class SelectorAttributeTestSupport
    {
        protected const string TestSelector = "test-selector";
        private static readonly MethodInfo factoryMethod = typeof(ContainerFactory).GetMethod("Create", BindingFlags.Public | BindingFlags.Static);

        protected IWebDriver WebDriver { get; private set; }

        protected IJavaScriptExecutor JavaScriptExecutor { get; private set; }

        protected IWebElement WebElement { get; private set; }

        [SetUp]
        public virtual void Init()
        {
            this.WebDriver = MockRepository.GenerateStub<IWebDriver>();
            this.JavaScriptExecutor = MockRepository.GenerateStub<IJavaScriptExecutor>();
            this.WebElement = MockRepository.GenerateStub<IWebElement>();

            this.WebDriver.Url = "/myhome/page.aspx";
        }

        protected T GetPropertyValue<T>(SimplePageForTest page, string propertyName)
        {
            var buttonProperty = page.GetType().GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (buttonProperty == null)
            {
                Assert.Fail("Page '{0}' does not cointain property '{1}' which required for the tests.".F(page.GetType().FullName, propertyName));
            }

            return (T)buttonProperty.GetValue(page, null);
        }

        protected SimplePageForTest CreatePageUsingFactory(string pageClassName)
        {
            var typeName = "{0}+{1}".FInv(this.GetType().FullName, pageClassName);
            var validPageType = Type.GetType(typeName);
            if (validPageType == null)
            {
                Assert.Fail("Unable to finde page with type '{0}' which required for the tests".F(typeName));
            }

            var typedCreateMethod = factoryMethod.MakeGenericMethod(validPageType);
            return (SimplePageForTest)typedCreateMethod.Invoke(null, new object[] { this.WebDriver, this.JavaScriptExecutor });
        }
    }
}