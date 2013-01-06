// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using Selenol.Page;

namespace Selenol.Validation.Page
{
    /// <summary>The interface that used to validate a page url.</summary>
    public interface IPageUrlValidator
    {
        /// <summary>Validates a page.</summary>
        /// <param name="page">The page. </param>
        /// <param name="currentUrl">The current Url. </param>
        /// <returns>True if page is valid otherwise false. </returns>
        bool Validate(BasePage page, string currentUrl);

        /// <summary>Gets an error message for an invalid page.</summary>
        /// <param name="page">The page. </param>
        /// <param name="currentUrl">The current Url. </param>
        /// <returns>The error message. </returns>
        string GetErrorMessage(BasePage page, string currentUrl);
    }
}