// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using OpenQA.Selenium;

namespace Selenol.Elements
{
    /// <summary>The option element.</summary>
    public class OptionElement : BaseHtmlElement
    {
        /// <summary>Initializes a new instance of the <see cref="OptionElement"/> class.</summary>
        /// <param name="webElement">The web element.</param>
        public OptionElement(IWebElement webElement)
            : base(webElement, x => webElement.TagName == "option")
        {
        }

        /// <summary>Gets the option text.</summary>
        public string Text
        {
            get
            {
                return this.WebElement.Text;
            }
        }

        /// <summary>Gets the option value.</summary>
        public string Value
        {
            get
            {
                return this.WebElement.GetAttribute(HtmlElementAttributes.Value);
            }
        }

        /// <summary>Gets a value indicating whether the option is selected or not.</summary>
        public bool IsSelected
        {
            get
            {
                return this.HasAttribute(HtmlElementAttributes.Selected);
            }
        }

        /// <summary>Select the option.</summary>
        /// <returns>The same option.</returns>
        public OptionElement Select()
        {
            this.WebElement.Click();
            return this;
        }
    }
}