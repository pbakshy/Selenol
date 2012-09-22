// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System;

using Selenol.Elements;
using Selenol.Extensions;

namespace Selenol.Validation.Element
{
    /// <summary>The attribute that validates an input element by a type.</summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class InputAttribute : Attribute, IElementValidator
    {
        private readonly string typeName;

        /// <summary>Initializes a new instance of the <see cref="InputAttribute"/> class.</summary>
        /// <param name="typeName">The type name.</param>
        public InputAttribute(string typeName)
        {
            if (typeName.IsNullOrEmpty())
            {
                throw new ArgumentNullException("typeName");
            }

            this.typeName = typeName;
        }

        /// <summary>Validates an element.</summary>
        /// <param name="element">The element.</param>
        /// <returns>True if element is valid otherwise false.</returns>
        public bool Validate(BaseHtmlElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            return string.Equals(element.TagName, HtmlElements.Input, StringComparison.InvariantCultureIgnoreCase)
                   && string.Equals(element.GetAttributeValue(HtmlElementAttributes.Type), this.typeName, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>Gets an error message for an invalid element.</summary>
        /// <param name="element">The invalid element.</param>
        /// <returns>The error message.</returns>
        public string GetErrorMessage(BaseHtmlElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            return string.Equals(element.TagName, HtmlElements.Input, StringComparison.InvariantCultureIgnoreCase)
                       ? @"The attribute 'type=""{0}""' does not match to 'type=""{1}""'".F(element.GetAttributeValue(HtmlElementAttributes.Type), this.typeName)
                       : "'{0}' tag does not match to '{1}' tag".F(element.TagName, HtmlElements.Input);
        }
    }
}