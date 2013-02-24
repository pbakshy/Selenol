// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System.Collections.ObjectModel;
using OpenQA.Selenium;
using Selenol.Elements;
using Selenol.Page;

namespace Selenol.SelectorAttributes
{
    /// <summary>
    /// The XPath selector attribute. Can be used for dynamic selection of elements using XPath. 
    /// An Element must be derived from <see cref="BaseHtmlElement"/> 
    /// or it can be a collection assignable from <see cref="ReadOnlyCollection{T}"/> 
    /// And used as an auto-property of class derived from <see cref="BasePage"/>.
    /// </summary>
    public class XPathAttribute : BaseSelectorAttribute
    {
        /// <summary>Initializes a new instance of the <see cref="XPathAttribute"/> class.</summary>
        /// <param name="xpath">The CSS selector.</param>
        public XPathAttribute(string xpath)
            : base(xpath)
        {
        }

        /// <summary>Gets the <see cref="By.XPath"/> selector.</summary>
        public override By Selector
        {
            get
            {
                return By.XPath(this.Value);
            }
        }
    }
}