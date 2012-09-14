// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using OpenQA.Selenium;

namespace Selenol.Elements
{
    /// <summary>The table cell element.</summary>
    public class TableCellElement : BaseHtmlElement
    {
        /// <summary>Initializes a new instance of the <see cref="TableCellElement"/> class.</summary>
        /// <param name="webElement">The web element.</param>
        public TableCellElement(IWebElement webElement)
            : base(webElement, x => x.TagName == HtmlElements.TableCell || x.TagName == HtmlElements.TableHeaderCell)
        {
        }
    }
}