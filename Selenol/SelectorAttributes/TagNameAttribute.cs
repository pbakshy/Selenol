// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System.Collections.ObjectModel;
using OpenQA.Selenium;
using Selenol.Controls;
using Selenol.Elements;
using Selenol.Page;

namespace Selenol.SelectorAttributes
{
    /// <summary>
    /// The Tag Name selector attribute. Can be used for dynamic selection of elements or controls by the tag name. 
    /// An Element must be derived from <see cref="BaseHtmlElement"/> 
    /// or it can be a collection assignable from <see cref="ReadOnlyCollection{T}"/>.
    /// A Control must be derived from <see cref="Control"/> 
    /// or it can be a collection assignable from <see cref="ReadOnlyCollection{TControl}"/>.
    /// And used as an auto-property of class derived from <see cref="BasePage"/>.
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