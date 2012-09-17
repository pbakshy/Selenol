// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;

using OpenQA.Selenium;

using Selenol.Validation;

namespace Selenol.Elements
{
    /// <summary>The table element.</summary>
    [Tag(HtmlElements.Table)]
    public class TableElement : ContainerElement
    {
        private const string HeadRowsSelector = HtmlElements.TableHead + " " + HtmlElements.TableRow;

        private const string FooterRowsSelector = HtmlElements.TableFooter + " " + HtmlElements.TableRow;

        private const string BodyRowsSelector = HtmlElements.TableBody + " " + HtmlElements.TableRow;

        /// <summary>Initializes a new instance of the <see cref="TableElement"/> class.</summary>
        /// <param name="webElement">The web element.</param>
        public TableElement(IWebElement webElement)
            : base(webElement)
        {
        }

        /// <summary>Gets the head rows of the table.</summary>
        public IEnumerable<TableRowElement> HeadRows
        {
            get
            {
                return this.GetRows(By.CssSelector(HeadRowsSelector));
            }
        }

        /// <summary>Gets the footer rows of the table.</summary>
        public IEnumerable<TableRowElement> FooterRows
        {
            get
            {
                return this.GetRows(By.CssSelector(FooterRowsSelector));
            }
        }

        /// <summary>Gets the body rows of the table.</summary>
        public IEnumerable<TableRowElement> BodyRows
        {
            get
            {
                return this.GetRows(By.CssSelector(BodyRowsSelector));
            }
        }

        /// <summary>Gets the all rows of the table.</summary>
        public IEnumerable<TableRowElement> AllRows
        {
            get
            {
                return this.GetRows(By.TagName(HtmlElements.TableRow));
            }
        } 

        private IEnumerable<TableRowElement> GetRows(By by)
        {
            return this.WebElement.FindElements(by).Select((element, index) => new TableRowElement(element, this, index)).ToArray();
        }
    }
}