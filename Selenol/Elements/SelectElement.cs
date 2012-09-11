// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;

using OpenQA.Selenium;

namespace Selenol.Elements
{
    /// <summary>The select html element.</summary>
    public class SelectElement : BaseHtmlElement
    {
        /// <summary>Initializes a new instance of the <see cref="SelectElement"/> class.</summary>
        /// <param name="webElement">The web element. </param>
        public SelectElement(IWebElement webElement)
            : base(webElement, x => webElement.TagName == "select")
        {
        }

        /// <summary>Gets the options.</summary>
        public IEnumerable<OptionElement> Options
        {
            get
            {
                return this.WebElement.FindElements(By.TagName("option")).Select(x => new OptionElement(x)).ToArray();
            }
        }

        /// <summary>Gets the selected option.</summary>
        public OptionElement SelectedOption
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>Select option by text.</summary>
        /// <param name="text">The text.</param>
        /// <returns>The same select element.</returns>
        public SelectElement SelectOptionByText(string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException("text");
            }

            throw new NotImplementedException();
        }

        /// <summary>Select option by value.</summary>
        /// <param name="value">The value.</param>
        /// <returns>The same select element.</returns>
        public SelectElement SelectOptionByValue(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            throw new NotImplementedException();
        }
    }
}