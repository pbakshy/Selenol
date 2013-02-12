// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using OpenQA.Selenium;
using Selenol.Elements;
using Selenol.Page;

namespace Selenol.SelectorAttributes
{
    /// <summary>
    /// The Tag Name selector attribute. Can be used for dynamic selection of elements by the tag name. 
    /// Element must be derived from <see cref="BaseHtmlElement"/> and used as an auto-property of class derived from <see cref="BasePage"/>
    /// </summary>
    public class TagNameAttribute : BaseSelectorAttribute
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="TagNameAttribute" /> class.
        /// </summary>
        /// <param name="tagName">The tag name.</param>
        public TagNameAttribute(string tagName)
            : base(tagName)
        {
        }

        /// <summary>
        ///     Gets the <see cref="By.TagName" /> selector.
        /// </summary>
        public override By Selector
        {
            get
            {
                return By.TagName(this.Value);
            }
        }
    }
}