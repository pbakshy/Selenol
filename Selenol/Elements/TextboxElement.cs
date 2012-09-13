// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System;

using OpenQA.Selenium;

namespace Selenol.Elements
{
    /// <summary>Textbox HTML element.</summary>
    public class TextboxElement : BaseHtmlElement
    {
        /// <summary>Initializes a new instance of the <see cref="TextboxElement"/> class.</summary>
        /// <param name="webElement">The web element. </param>
        public TextboxElement(IWebElement webElement)
            : this(webElement, x => x.TagName == HtmlElements.Input && x.GetAttributeValue(HtmlElementAttributes.Type) == HtmlInputTypes.Textbox)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="TextboxElement"/> class.</summary>
        /// <param name="webElement">The web element.</param>
        /// <param name="checkElementPredicate">The check element predicate.</param>
        protected TextboxElement(IWebElement webElement, Func<BaseHtmlElement, bool> checkElementPredicate)
            : base(webElement, checkElementPredicate)
        {
        }

        /// <summary>Gets the text typed into the textbox.</summary>
        public string Text
        {
            get
            {
                return this.WebElement.Text;
            }
        }

        /// <summary>Simulates typing text into the textbox.</summary>
        /// <param name="text">The text. </param>
        /// <returns>The same textbox. </returns>
        public TextboxElement TypeText(string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException("text");
            }

            this.WebElement.SendKeys(text);
            return this;
        }
    }
}