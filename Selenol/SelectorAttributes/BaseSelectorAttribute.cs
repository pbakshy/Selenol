// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System;
using OpenQA.Selenium;
using Selenol.Elements;
using Selenol.Page;

namespace Selenol.SelectorAttributes
{
    /// <summary>
    /// The base selector attribute. Should be used for dynamic mapping element derived from <see cref="BaseHtmlElement"/>
    /// to properties of classes derived from <see cref="BasePage"/>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public abstract class BaseSelectorAttribute : Attribute
    {
        /// <summary>Initializes a new instance of the <see cref="BaseSelectorAttribute"/> class.</summary>
        /// <param name="selectorValue">The value.</param>
        protected BaseSelectorAttribute(string selectorValue)
        {
            if (string.IsNullOrWhiteSpace(selectorValue))
            {
                throw new ArgumentException("Parameter can not be null or empty.", "selectorValue");
            }

            this.Value = selectorValue;
        }

        /// <summary>Gets the instance <see cref="By"/> selector with corresponding value.</summary>
        public abstract By Selector { get; }

        /// <summary>Gets the selector value.</summary>
        protected string Value { get; private set; }
    }
}