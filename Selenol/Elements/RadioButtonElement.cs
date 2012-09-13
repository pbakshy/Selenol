// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using OpenQA.Selenium;

namespace Selenol.Elements
{
    /// <summary>The radio button html element.</summary>
    public class RadioButtonElement : BaseHtmlElement
    {
        /// <summary>Initializes a new instance of the <see cref="RadioButtonElement"/> class.</summary>
        /// <param name="webElement">The web element.</param>
        public RadioButtonElement(IWebElement webElement)
            : base(webElement, x => webElement.TagName == HtmlElements.Input && webElement.GetAttribute(HtmlElementAttributes.Type) == HtmlInputTypes.RadioButton)
        {
        }

        /// <summary>Gets a value indicating whether the radio button is checked or not.</summary>
        public bool IsChecked
        {
            get
            {
                return this.HasAttribute(HtmlElementAttributes.Checked);
            }
        }

        /// <summary>Gets the radio button value.</summary>
        /// <remarks>If value attribute does not exist empty string will be returned.</remarks>
        public string Value
        {
            get
            {
                return this.GetAttributeValue(HtmlElementAttributes.Value);
            }
        }

        /// <summary>Checks the radio button.</summary>
        public void Check()
        {
            this.WebElement.Click();
        }
    }
}