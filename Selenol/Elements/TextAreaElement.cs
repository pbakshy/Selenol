// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using OpenQA.Selenium;

using Selenol.Validation.Element;

namespace Selenol.Elements
{
    /// <summary>The text area html element.</summary>
    [Tag(HtmlElements.TextArea)]
    public class TextAreaElement : TextboxElement
    {
        /// <summary>Initializes a new instance of the <see cref="TextAreaElement"/> class.</summary>
        /// <param name="webElement">The web element. </param>
        public TextAreaElement(IWebElement webElement)
            : base(webElement)
        {
        }
    }
}