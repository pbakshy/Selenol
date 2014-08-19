// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System;

using OpenQA.Selenium;

using Selenol.Validation.Element;

namespace Selenol.Elements
{
    /// <summary>Textbox HTML element.</summary>
    [Input(HtmlInputTypes.Textbox)]
    public class TextboxElement : BaseTextInputElement
    {
        /// <summary>Initializes a new instance of the <see cref="TextboxElement"/> class.</summary>
        /// <param name="webElement">The web element. </param>
        public TextboxElement(IWebElement webElement)
            : base(webElement)
        {
        }
    }
}