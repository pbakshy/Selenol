// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using OpenQA.Selenium;

namespace Selenol.Elements
{
    /// <summary>The table element.</summary>
    public class TableElement : BaseHtmlElement
    {
        /// <summary>Initializes a new instance of the <see cref="TableElement"/> class.</summary>
        /// <param name="webElement">The web element.</param>
        public TableElement(IWebElement webElement)
            : base(webElement, x => x.TagName == HtmlElements.Table)
        {
        }

        /// <summary>Gets the head rows of the table.</summary>
        public IEnumerable<TableRowElement> HeadRows
        {
            get
            {
                var selector = string.Format(CultureInfo.InvariantCulture, "{0} {1}", HtmlElements.TableHead, HtmlElements.TableRow);
                return this.GetRows(By.CssSelector(selector));
            }
        }

        /// <summary>Gets the body rows of the table.</summary>
        public IEnumerable<TableRowElement> BodyRows
        {
            get
            {
                var selector = string.Format(CultureInfo.InvariantCulture, "{0} {1}", HtmlElements.TableBody, HtmlElements.TableRow);
                return this.GetRows(By.CssSelector(selector));
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
            return this.WebElement.FindElements(by).Select(x => new TableRowElement(x)).ToArray();
        }
    }
}