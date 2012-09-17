// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System;

using Selenol.Elements;
using Selenol.Extensions;

namespace Selenol.Validation
{
    /// <summary>The attribute that validates an element by a tag name.</summary>
    public class TagAttribute : Attribute, IElementValidator
    {
        private readonly string tagName;

        /// <summary>Initializes a new instance of the <see cref="TagAttribute"/> class.</summary>
        /// <param name="tagName">The tag name.</param>
        public TagAttribute(string tagName)
        {
            if (tagName.IsNullOrEmpty())
            {
                throw new ArgumentNullException("tagName");
            }

            this.tagName = tagName;
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

            return Equals(element.TagName, this.tagName);
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

            return "'{0}' tag does not match to '{1}' tag".F(element.TagName, this.tagName);
        }
    }
}