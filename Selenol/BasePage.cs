// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System;

using OpenQA.Selenium;

namespace Selenol
{
    /// <summary>A Page class represents content of a web page or frame.</summary>
    public abstract class BasePage
    {
        /// <summary>Initializes a new instance of the <see cref="BasePage"/> class.</summary>
        /// <param name="webDriver">The web driver. </param>
        protected BasePage(IWebDriver webDriver)
        {
            if (webDriver == null)
            {
                throw new ArgumentNullException("webDriver");
            }

            this.WebDriver = webDriver;
        }

        /// <summary>Gets the context which holds the page or frame content.</summary>
        public ISearchContext Context
        {
            get
            {
                return this.WebDriver;
            }
        }

        /// <summary>Gets driver which opened the page.</summary>
        protected IWebDriver WebDriver { get; private set; }
    }
}