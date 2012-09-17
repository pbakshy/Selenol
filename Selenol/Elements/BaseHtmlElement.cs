// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;

using OpenQA.Selenium;

using Selenol.Extensions;
using Selenol.Validation;

namespace Selenol.Elements
{
    /// <summary>The base html element.</summary>
    public abstract class BaseHtmlElement
    {
        /// <summary>Initializes a new instance of the <see cref="BaseHtmlElement"/> class.</summary>
        /// <param name="webElement">The web element.</param>
        protected BaseHtmlElement(IWebElement webElement)
        {
            if (webElement == null)
            {
                throw new ArgumentNullException("webElement");
            }

            this.WebElement = webElement;

            var validators = this.GetType().GetCustomAttributes(true).OfType<IElementValidator>().ToArray();
            if (!validators.Any(x => x.Validate(this)))
            {
                throw new WrongElementException(validators.First().GetErrorMessage(this), this.WebElement);
            }
        }

        /// <summary>Gets the element id.</summary>
        /// <remarks>If id attribute does not exist empty string will be returned.</remarks>
        public string Id
        {
            get
            {
                return this.WebElement.GetAttribute(HtmlElementAttributes.Id) ?? string.Empty;
            }
        }

        /// <summary>Gets the element name.</summary>
        /// <remarks>If name attribute does not exist empty string will be returned.</remarks>
        public string Name 
        {
            get
            {
                return this.WebElement.GetAttribute(HtmlElementAttributes.Name) ?? string.Empty;
            }
        }

        /// <summary>Gets the element classes.</summary>
        public IEnumerable<string> Classes
        {
            get
            {
                var attributeValue = this.WebElement.GetAttribute(HtmlElementAttributes.Class);
                return !attributeValue.IsNullOrEmpty() ? attributeValue.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries) : new string[0];
            }
        }

        /// <summary>Gets a value indicating whether the element is displayed or not.</summary>
        public bool IsDisplayed
        {
            get
            {
                return this.WebElement.Displayed;
            }
        }

        /// <summary>Gets a value indicating whether the element is enabled or not.</summary>
        public bool IsEnabled
        {
            get
            {
                return this.WebElement.Enabled;
            }
        }

        /// <summary>Gets the element tag name.</summary>
        internal string TagName
        {
            get
            {
                return this.WebElement.TagName;
            }
        }

        /// <summary>Gets the wrapped Selenium element.</summary>
        protected IWebElement WebElement { get; private set; }

        /// <summary>Gets the element attribute value.</summary>
        /// <param name="attributeName">The attribute value.</param>
        /// <returns>The attribute value if it exists otherwise empty string.</returns>
        public string GetAttributeValue(string attributeName)
        {
            if (attributeName == null)
            {
                throw new ArgumentNullException("attributeName");
            }

            return this.WebElement.GetAttribute(attributeName) ?? string.Empty;
        }

        /// <summary>Checks if the element has a class.</summary>
        /// <param name="className">The class name.</param>
        /// <returns>True if element has given class otherwise false.</returns>
        public bool HasClass(string className)
        {
            if (className.IsNullOrEmpty())
            {
                throw new ArgumentNullException("className");
            }

            return this.Classes.Contains(className);
        }

        /// <summary>Checks if the element has an attribute.</summary>
        /// <param name="attributeName">The attribute name.</param>
        /// <returns>True if element has given attribute otherwise false.</returns>
        public bool HasAttribute(string attributeName)
        {
            if (attributeName.IsNullOrEmpty())
            {
                throw new ArgumentNullException("attributeName");
            }

            return this.WebElement.GetAttribute(attributeName) != null;
        }
    }
}