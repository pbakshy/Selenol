// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using OpenQA.Selenium;
using Selenol.Elements;

namespace Selenol.Controls
{
    /// <summary>The Control class represents logically structured and/or reusable group of page elements.</summary>
    /// <typeparam name="T">The type of container element which is used as basis for control.</typeparam>
    public abstract class Control<T> : ContainerElement where T : ContainerElement
    {
        /// <summary>Initializes a new instance of the <see cref="Control{T}" /> class.</summary>
        /// <param name="webElement">The web element.</param>
        protected Control(IWebElement webElement) // может передавать в качестве параметра ContainerElement соответствующий...
            : base(webElement)
        {
        }
    }
}