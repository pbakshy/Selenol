// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using OpenQA.Selenium;
using Selenol.Elements;
using Selenol.Page;

namespace Selenol.SelectorAttributes
{
    /// <summary>
    /// The Class selector attribute. Can be used for dynamic selection of elements by their class name. 
    /// Element must derived from <see cref="BaseHtmlElement"/> and used as an auto-property of class derived from <see cref="BasePage"/>
    /// </summary>
    public class ClassAttribute : BaseSelectorAttribute
    {
        /// <summary>Initializes a new instance of the <see cref="ClassAttribute"/> class.</summary>
        /// <param name="className">The class Name.</param>
        public ClassAttribute(string className)
            : base(className)
        {
        }

        /// <summary>Gets the <see cref="By.ClassName"/> selector.</summary>
        public override By Selector
        {
            get
            {
                return By.ClassName(this.Value);
            }
        }
    }
}