// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using OpenQA.Selenium;

using Selenol.Validation.Element;

namespace Selenol.Elements
{
    /// <summary>The html link element.</summary>
    [Tag(HtmlElements.Link)]
    public class LinkElement : BaseHtmlElement
    {
        /// <summary>Initializes a new instance of the <see cref="LinkElement"/> class.</summary>
        /// <param name="webElement">The web element.</param>
        public LinkElement(IWebElement webElement)
            : base(webElement)
        {
        }

        /// <summary>Gets the link text.</summary>
        public string Text
        {
            get
            {
                return this.WebElement.Text;
            }
        }

        /// <summary>Gets the link url.</summary>
        public string Url
        {
            get
            {
                return this.GetAttributeValue(HtmlElementAttributes.HyperReference);
            }
        }

        /// <summary>Clicks the link.</summary>
        public void Click()
        {
            this.WebElement.Click();
        }
    }
}