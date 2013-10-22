// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System;
using OpenQA.Selenium;
using Selenol.Validation.Element;

namespace Selenol.Elements
{
    /// <summary>The password box.</summary>
    [Input(HtmlInputTypes.Passwordbox)]
    public class PasswordboxElement : BaseTextInputElement
    {
        /// <summary>Initializes a new instance of the <see cref="PasswordboxElement"/> class.</summary>
        /// <param name="webElement">The web element.</param>
        public PasswordboxElement(IWebElement webElement)
            : base(webElement)
        {
        }
    }
}