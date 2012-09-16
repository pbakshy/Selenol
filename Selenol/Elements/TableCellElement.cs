// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using OpenQA.Selenium;

namespace Selenol.Elements
{
    /// <summary>The table cell element.</summary>
    public class TableCellElement : ContainerElement
    {
        /// <summary>Initializes a new instance of the <see cref="TableCellElement"/> class.</summary>
        /// <param name="webElement">The web element. </param>
        /// <param name="parent">The parent row. </param>
        /// <param name="index">The index. </param>
        public TableCellElement(IWebElement webElement, TableRowElement parent, int index)
            : base(webElement, x => x.TagName == HtmlElements.TableCell || x.TagName == HtmlElements.TableHeaderCell)
        {
            this.Parent = parent;
            this.Index = index;
        }

        /// <summary>Gets the parent row.</summary>
        public TableRowElement Parent { get; private set; }

        /// <summary>Gets the index.</summary>
        public int Index { get; private set; }
    }
}