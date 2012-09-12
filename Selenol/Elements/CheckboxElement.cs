// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using OpenQA.Selenium;

namespace Selenol.Elements
{
    /// <summary>The checkbox html element.</summary>
    public class CheckboxElement : BaseHtmlElement
    {
        /// <summary>Initializes a new instance of the <see cref="CheckboxElement"/> class.</summary>
        /// <param name="webElement">The web element.</param>
        public CheckboxElement(IWebElement webElement)
            : base(webElement, x => webElement.TagName == HtmlElements.Input && x.GetAttributeValue(HtmlElementAttributes.Type) == "checkbox")
        {
        }

        /// <summary>Gets a value indicating whether the checkbox is checked or not.</summary>
        public bool IsChecked
        {
            get
            {
                return this.HasAttribute(HtmlElementAttributes.Checked);
            }
        }

        /// <summary>Checks the checkbox.</summary>
        public void Check()
        {
            if (!this.IsChecked)
            {
                this.WebElement.Click();
            }
        }

        /// <summary>Unchecks the checkbox.</summary>
        public void Uncheck()
        {
            if (this.IsChecked)
            {
                this.WebElement.Click();
            }
        }

        /// <summary>Toggle the checkbox state.</summary>
        public void ToggleCheck()
        {
            this.WebElement.Click();
        }
    }
}