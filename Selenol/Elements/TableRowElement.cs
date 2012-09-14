// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;

using OpenQA.Selenium;

namespace Selenol.Elements
{
    /// <summary>The table row element.</summary>
    public class TableRowElement : BaseHtmlElement
    {
        private const string CellSelector = HtmlElements.TableCell + "," + HtmlElements.TableHeaderCell;

        /// <summary>Initializes a new instance of the <see cref="TableRowElement"/> class.</summary>
        /// <param name="webElement">The web element.</param>
        public TableRowElement(IWebElement webElement)
            : base(webElement, x => x.TagName == HtmlElements.TableRow)
        {
        }

        /// <summary>Gets the cells of the row.</summary>
        public IEnumerable<TableCellElement> Cells
        {
            get
            {
                return this.WebElement.FindElements(By.CssSelector(CellSelector)).Select(x => new TableCellElement(x)).ToArray();
            }
        } 
    }
}