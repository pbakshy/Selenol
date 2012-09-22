// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System;

using Selenol.Elements;
using Selenol.Extensions;

namespace Selenol.Validation.Element
{
    /// <summary>The attribute that validates an element is not an input element.</summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class NotInputAttribute : Attribute, IElementValidator
    {
        /// <summary>Validates an element.</summary>
        /// <param name="element">The element. </param>
        /// <returns>True if element is valid otherwise false. </returns>
        public bool Validate(BaseHtmlElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            var tagName = element.TagName;
            return !string.Equals(tagName, HtmlElements.Input, StringComparison.InvariantCultureIgnoreCase)
                   && !string.Equals(tagName, HtmlElements.Button, StringComparison.InvariantCultureIgnoreCase)
                   && !string.Equals(tagName, HtmlElements.Option, StringComparison.InvariantCultureIgnoreCase)
                   && !string.Equals(tagName, HtmlElements.Select, StringComparison.InvariantCultureIgnoreCase)
                   && !string.Equals(tagName, HtmlElements.TextArea, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>Gets an error message for an invalid element.</summary>
        /// <param name="element">The invalid element. </param>
        /// <returns>The error message. </returns>
        public string GetErrorMessage(BaseHtmlElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            return "Expected tag not in set of input, textarea, select, button, option. But was '{0}'.".F(element.TagName);
        }
    }
}