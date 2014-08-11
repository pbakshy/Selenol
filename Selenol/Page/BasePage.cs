// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.ObjectModel;
using System.Linq;
using Castle.DynamicProxy;
using OpenQA.Selenium;
using Selenol.Extensions;
using Selenol.Utils;
using Selenol.Validation.Page;

namespace Selenol.Page
{
    /// <summary>A Page class represents content of a web page or frame.</summary>
    public abstract class BasePage : ISearchContext, IJavaScriptExecutor
    {
        private IWebDriver webDriver;
        private IJavaScriptExecutor javaScriptExecutor;
        private bool isInitialized;

        /// <summary>Gets the web driver.</summary>
        internal IWebDriver WebDriver
        {
            get
            {
                this.CheckIsInitialized();
                return this.webDriver;
            }
        }

        /// <summary>Gets the java script executor.</summary>
        internal IJavaScriptExecutor JavaScriptExecutor
        {
            get
            {
                this.CheckIsInitialized();
                return this.javaScriptExecutor;
            }
        }

        /// <summary>
        /// Finds the first <see cref="T:OpenQA.Selenium.IWebElement"/> using the given method. 
        /// </summary>
        /// <param name="by">The locating mechanism to use.</param>
        /// <returns>
        /// The first matching <see cref="T:OpenQA.Selenium.IWebElement"/> on the current context.
        /// </returns>
        /// <exception cref="T:OpenQA.Selenium.NoSuchElementException">If no element matches the criteria.</exception>
        public IWebElement FindElement(By @by)
        {
            this.CheckIsInitialized();
            this.Validate();
            return this.webDriver.FindElement(by);
        }

        /// <summary>
        ///     Finds all <see cref="T:OpenQA.Selenium.IWebElement">IWebElements</see> within the current context
        ///     using the given mechanism.
        /// </summary>
        /// <param name="by">The locating mechanism to use.</param>
        /// <returns>
        ///     A <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> of all
        ///     <see cref="T:OpenQA.Selenium.IWebElement">WebElements</see>
        ///     matching the current criteria, or an empty list if nothing matches.
        /// </returns>
        public ReadOnlyCollection<IWebElement> FindElements(By @by)
        {
            this.CheckIsInitialized();
            this.Validate();
            return this.webDriver.FindElements(by);
        }

        /// <summary>Executes JavaScript in the context of the current Page.</summary>
        /// <param name="script">The JavaScript code to execute. </param>
        /// <param name="args">The arguments to the script. </param>
        /// <returns>The value returned by the script. </returns>
        /// <remarks><para>The <see cref="M:OpenQA.Selenium.IJavaScriptExecutor.ExecuteScript(System.String,System.Object[])"/> method executes JavaScript in the context of the current Page. This means that "document" will refer to the current document. If the script has a return value, then the following steps will be taken:. </para>
        /// <para><list type="bullet"><item><description>For an HTML element, this method returns a
        ///                <see cref="T:OpenQA.Selenium.IWebElement"/>.
        ///              </description></item>
        /// <item><description>For a number, a
        ///                <see cref="T:System.Int64"/>
        ///                is returned.</description></item>
        /// <item><description>For a boolean, a
        ///                <see cref="T:System.Boolean"/>
        ///                is returned.</description></item>
        /// <item><description>For all other cases a
        ///                <see cref="T:System.String"/>
        ///                is returned.</description></item>
        /// <item><description>For an array,we check the first element, and attempt to return a
        ///                <see cref="T:System.Collections.Generic.List`1"/>
        ///                of that type, following the rules above. Nested lists are not
        ///                supported.</description></item>
        /// <item><description>If the value is null or there is no return value,
        ///                <see langword="null"/>
        ///                is returned.</description></item>
        /// </list>
        /// </para>
        /// <para>Arguments must be a number (which will be converted to a <see cref="T:System.Int64"/> ), a <see cref="T:System.Boolean"/> , a <see cref="T:System.String"/> or a <see cref="T:OpenQA.Selenium.IWebElement"/> . An exception will be thrown if the arguments do not meet these criteria. The arguments will be made available to the JavaScript via the "arguments" magic variable, as if the function were called via "Function.apply". </para>
        /// .</remarks>
        public object ExecuteScript(string script, params object[] args)
        {
            this.CheckIsInitialized();
            this.Validate();
            return this.javaScriptExecutor.ExecuteScript(script, args);
        }

        /// <summary>Executes JavaScript asynchronously in the context of the current Page.</summary>
        /// <param name="script">The JavaScript code to execute. </param>
        /// <param name="args">The arguments to the script. </param>
        /// <returns>The value returned by the script. </returns>
        public object ExecuteAsyncScript(string script, params object[] args)
        {
            this.CheckIsInitialized();
            this.Validate();
            return this.javaScriptExecutor.ExecuteAsyncScript(script, args);
        }

        /// <summary>Initializes the page.</summary>
        /// <param name="driver">The web driver. </param>
        /// <param name="jsExecutor">The JavaScript executor. </param>
        internal void Initialize(IWebDriver driver, IJavaScriptExecutor jsExecutor)
        {
            if (driver == null)
            {
                throw new ArgumentNullException("driver");
            }

            if (jsExecutor == null)
            {
                throw new ArgumentNullException("jsExecutor");
            }

            this.webDriver = driver;
            this.javaScriptExecutor = jsExecutor;
            this.Validate();
            this.isInitialized = true;
        }

        private void CheckIsInitialized()
        {
            if (!this.isInitialized)
            {
                var message =
                    "The page '{0}' has not been initialized yet. It can happen if you have created the page by constructor. Please use PageFactory.Page method instead."
                        .F(this.GetType().Name);
                throw new PageInitializationException(message);
            }
        }

        private void Validate()
        {
            var currentUrl = this.webDriver.Url;
            if (PageUtil.IsValid(ProxyUtil.GetUnproxiedType(this), currentUrl))
            {
                return;
            }

            var validators = this.GetType().GetCustomAttributes(true).OfType<IPageUrlValidator>().ToArray();
            var message = validators.Select(x => x.GetErrorMessage(currentUrl)).Join(" Or ");
            throw new PageValidationException(message, this);
        }
    }
}