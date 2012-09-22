// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using Selenol.Elements;

namespace Selenol.Validation.Element
{
    /// <summary>The element validator interface.</summary>
    public interface IElementValidator
    {
        /// <summary>Validates an element.</summary>
        /// <param name="element">The element.</param>
        /// <returns>True if element is valid otherwise false.</returns>
        bool Validate(BaseHtmlElement element);

        /// <summary>Gets an error message for an invalid element.</summary>
        /// <param name="element">The invalid element.</param>
        /// <returns>The error message.</returns>
        string GetErrorMessage(BaseHtmlElement element);
    }
}