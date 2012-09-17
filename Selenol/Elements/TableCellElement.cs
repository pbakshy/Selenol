// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using OpenQA.Selenium;

using Selenol.Validation;

namespace Selenol.Elements
{
    /// <summary>The table cell element.</summary>
    [Tag(HtmlElements.TableHeaderCell)]
    [Tag(HtmlElements.TableCell)]
    public class TableCellElement : ContainerElement
    {
        /// <summary>Initializes a new instance of the <see cref="TableCellElement"/> class.</summary>
        /// <param name="webElement">The web element. </param>
        /// <param name="parent">The parent row. </param>
        /// <param name="index">The index. </param>
        public TableCellElement(IWebElement webElement, TableRowElement parent, int index)
            : base(webElement)
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