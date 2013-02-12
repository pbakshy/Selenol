// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using OpenQA.Selenium;
using Selenol.Elements;
using Selenol.Page;

namespace Selenol.SelectorAttributes
{
    /// <summary>
    /// The Id selector attribute. Can be used for dynamic selection of elements by their Id. 
    /// Element must be derived from <see cref="BaseHtmlElement"/> and used as an auto-property of class derived from <see cref="BasePage"/>
    /// </summary>
    public class IdAttribute : BaseSelectorAttribute
    {
        /// <summary>Initializes a new instance of the <see cref="IdAttribute"/> class.</summary>
        /// <param name="id">The Id.</param>
        public IdAttribute(string id)
            : base(id)
        {
        }

        /// <summary>Gets the <see cref="By.Id"/> selector.</summary>
        public override By Selector
        {
            get
            {
                return By.Id(this.Value);
            }
        }
    }
}