// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using OpenQA.Selenium;

namespace Selenol.Elements
{
    /// <summary>The generic container element.</summary>
    public class GenericContainerElement : BaseHtmlElement
    {
        /// <summary>Initializes a new instance of the <see cref="GenericContainerElement"/> class.</summary>
        /// <param name="webElement">The web element.</param>
        public GenericContainerElement(IWebElement webElement)
            : base(webElement, x => x.TagName != HtmlElements.Input)
        {
        }

        /// <summary>Gets the element text.</summary>
        public string Text
        {
            get
            {
                return this.WebElement.Text;
            }
        }
    }
}