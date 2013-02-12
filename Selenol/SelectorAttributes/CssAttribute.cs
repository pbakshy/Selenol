// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using OpenQA.Selenium;
using Selenol.Elements;
using Selenol.Page;

namespace Selenol.SelectorAttributes
{
    /// <summary>
    /// The CSS selector attribute. Can be used for dynamic selection of elements using CSS selectors. 
    /// Element must be derived from <see cref="BaseHtmlElement"/> and used as an auto-property of class derived from <see cref="BasePage"/>
    /// </summary>
    public class CssAttribute : BaseSelectorAttribute
    {
        /// <summary>Initializes a new instance of the <see cref="CssAttribute"/> class.</summary>
        /// <param name="cssSelector">The CSS selector.</param>
        public CssAttribute(string cssSelector)
            : base(cssSelector)
        {
        }

        /// <summary>Gets the <see cref="By.CssSelector"/>.</summary>
        public override By Selector
        {
            get
            {
                return By.CssSelector(this.Value);
            }
        }
    }
}