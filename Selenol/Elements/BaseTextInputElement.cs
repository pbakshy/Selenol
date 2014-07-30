// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System;
using OpenQA.Selenium;

namespace Selenol.Elements
{
    /// <summary>The base text input element.</summary>
    public abstract class BaseTextInputElement : BaseHtmlElement
    {
        /// <summary>Initializes a new instance of the <see cref="BaseTextInputElement"/> class.</summary>
        /// <param name="webElement">The web element.</param>
        protected BaseTextInputElement(IWebElement webElement)
            : base(webElement)
        {
        }

        /// <summary>Gets the text typed into the text input.</summary>
        public string Text
        {
            get
            {
                return this.GetAttributeValue(HtmlElementAttributes.Value);
            }
        }

        /// <summary>Simulates typing text into the text input.</summary>
        /// <param name="text">The text. </param>
        public void TypeText(string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException("text");
            }

            this.WebElement.SendKeys(text);
        }

        /// <summary>
        /// Sends Enter key to the input.
        /// </summary>
        public void SendEnter()
        {
            this.WebElement.SendKeys(Keys.Enter);
        }

        /// <summary>Clears text in the text input.</summary>
        public void Clear()
        {
            this.WebElement.Clear();
        }
    }
}