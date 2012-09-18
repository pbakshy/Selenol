// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System;

using Selenol.Elements;

namespace Selenol.Validation
{
    /// <summary>Skips the element validation. Only for internal using.</summary>
    [AttributeUsage(AttributeTargets.Class)]
    internal class SkipValidationAttribute : Attribute, IElementValidator
    {
        /// <summary>Validates an element.</summary>
        /// <param name="element">The element.</param>
        /// <returns>True if element is valid otherwise false.</returns>
        public bool Validate(BaseHtmlElement element)
        {
            return true;
        }

        /// <summary>Gets an error message for an invalid element.</summary>
        /// <param name="element">The invalid element.</param>
        /// <returns>The error message.</returns>
        public string GetErrorMessage(BaseHtmlElement element)
        {
            throw new NotSupportedException();
        }
    }
}