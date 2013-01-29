// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Linq;
using OpenQA.Selenium;
using Selenol.Extensions;
using Selenol.Validation;
using Selenol.Validation.Page;

namespace Selenol.Page
{
    /// <summary>A Page class represents content of a web page or frame.</summary>
    public abstract class BasePage : IJavaScriptExecutor
    {
        private IWebDriver webDriver;
        private IJavaScriptExecutor javaScriptExecutor;
        private bool isInitialized;

        /// <summary>Gets the context which holds the page or frame content.</summary>
        public ISearchContext Context
        {
            get
            {
                this.CheckIsInitialized();
                this.Validate();
                return this.webDriver;
            }
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
        /// <param name="jsExecutor">The javascript executor. </param>
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
                    "The page '{0}' has not been initialized yet. It can happen if you have created the page by constructor. Please use PageFactory.Create method instead."
                        .F(this.GetType().Name);
                throw new PageInitializationException(message, this);
            }
        }

        private void Validate()
        {
            var validators = this.GetType().GetCustomAttributes(true).OfType<IPageUrlValidator>().ToArray();
            if (validators.Length == 0)
            {
                throw new ValidationAbsenceException("Page '{0}' does not have any Url validation. Please add an Url validation.".F(this.GetType()));
            }

            var currentUrl = this.webDriver.Url;
            if (!validators.Any(x => x.Validate(this, currentUrl)))
            {
                var message = validators.Select(x => x.GetErrorMessage(this, currentUrl)).Join(" Or ");
                throw new PageValidationException(message, this);
            }
        }
    }
}