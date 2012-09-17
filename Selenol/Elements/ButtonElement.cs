// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using OpenQA.Selenium;

using Selenol.Validation;

namespace Selenol.Elements
{
    /// <summary>The button element.</summary>
    [Tag(HtmlElements.Button)]
    [Input(HtmlInputTypes.Button)]
    [Input(HtmlInputTypes.Submit)]
    [Input(HtmlInputTypes.Reset)]
    public class ButtonElement : BaseHtmlElement
    {
        /// <summary>Initializes a new instance of the <see cref="ButtonElement"/> class.</summary>
        /// <param name="webElement">The web element.</param>
        public ButtonElement(IWebElement webElement)
            : base(webElement)
        {
        }

        /// <summary>Gets the button text.</summary>
        /// <remarks>If text does not exist empty string will be returned.</remarks>
        public string Text
        {
            get
            {
                return this.TagName == HtmlElements.Button ? this.WebElement.Text : this.Value;
            }
        }

        /// <summary>Gets the button value.</summary>
        /// <remarks>If value attribute does not exist empty string will be returned.</remarks>
        public string Value
        {
            get
            {
                return this.GetAttributeValue(HtmlElementAttributes.Value);
            }
        }

        /// <summary>Clicks the button.</summary>
        public void Click()
        {
            this.WebElement.Click();
        }
    }
}