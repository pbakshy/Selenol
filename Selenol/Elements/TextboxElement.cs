// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System;

using OpenQA.Selenium;

using Selenol.Validation.Element;

namespace Selenol.Elements
{
    /// <summary>Textbox HTML element.</summary>
    [Input(HtmlInputTypes.Textbox)]
    public class TextboxElement : BaseHtmlElement
    {
        /// <summary>Initializes a new instance of the <see cref="TextboxElement"/> class.</summary>
        /// <param name="webElement">The web element. </param>
        public TextboxElement(IWebElement webElement)
            : base(webElement)
        {
        }

        /// <summary>Gets the text typed into the textbox.</summary>
        public string Text
        {
            get
            {
                return this.GetAttributeValue(HtmlElementAttributes.Value);
            }
        }

        /// <summary>Simulates typing text into the textbox.</summary>
        /// <param name="text">The text. </param>
        public void TypeText(string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException("text");
            }

            this.WebElement.SendKeys(text);
        }

        /// <summary>Clears text in the textbox.</summary>
        public void Clear()
        {
            this.WebElement.Clear();
        }
    }
}