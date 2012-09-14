// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using OpenQA.Selenium;

namespace Selenol.Elements
{
    /// <summary>The table row element.</summary>
    public class TableRowElement : BaseHtmlElement
    {
        /// <summary>Initializes a new instance of the <see cref="TableRowElement"/> class.</summary>
        /// <param name="webElement">The web element.</param>
        public TableRowElement(IWebElement webElement)
            : base(webElement, x => x.TagName == HtmlElements.TableRow)
        {
        }
    }
}