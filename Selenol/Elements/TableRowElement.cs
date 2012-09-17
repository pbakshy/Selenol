// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;

using OpenQA.Selenium;

using Selenol.Validation;

namespace Selenol.Elements
{
    /// <summary>The table row element.</summary>
    [Tag(HtmlElements.TableRow)]
    public class TableRowElement : ContainerElement
    {
        private const string CellSelector = HtmlElements.TableCell + "," + HtmlElements.TableHeaderCell;

        /// <summary>Initializes a new instance of the <see cref="TableRowElement"/> class.</summary>
        /// <param name="webElement">The web element. </param>
        /// <param name="parent">The parent table. </param>
        /// <param name="index">The index. </param>
        public TableRowElement(IWebElement webElement, TableElement parent, int index)
            : base(webElement)
        {
            this.Parent = parent;
            this.Index = index;
        }

        /// <summary>Gets the parent table.</summary>
        public TableElement Parent { get; private set; }

        /// <summary>Gets the index.</summary>
        public int Index { get; private set; }

        /// <summary>Gets the cells of the row.</summary>
        public IEnumerable<TableCellElement> Cells
        {
            get
            {
                return this.WebElement.FindElements(By.CssSelector(CellSelector)).Select((element, index) => new TableCellElement(element, this, index)).ToArray();
            }
        }
    }
}