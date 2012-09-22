// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using OpenQA.Selenium;

using Selenol.Validation.Element;

namespace Selenol.Elements
{
    /// <summary>The basic element without the validation. Only for internal using.</summary>
    [SkipValidation]
    internal class BasicHtmlElement : BaseHtmlElement
    {
        /// <summary>Initializes a new instance of the <see cref="BasicHtmlElement"/> class.</summary>
        /// <param name="webElement">The web element.</param>
        public BasicHtmlElement(IWebElement webElement)
            : base(webElement)
        {
        }
    }
}