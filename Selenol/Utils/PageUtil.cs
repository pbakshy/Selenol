// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Linq;
using Selenol.Extensions;
using Selenol.Validation;
using Selenol.Validation.Page;

namespace Selenol.Utils
{
    /// <summary>The page utility which provides helper methods for Page objects.</summary>
    internal class PageUtil
    {
        /// <summary>Determines is current page valid for current Url. Uses <see cref="IPageUrlValidator"/> attributes for validation.</summary>
        /// <param name="pageType">The page type.</param>
        /// <param name="currentUrl">The current url.</param>
        /// <returns>The True if page valid, otherwise false.</returns>
        /// <exception cref="ValidationAbsenceException">If no validation attributes was applied for the page.</exception>
        internal static bool IsValid(Type pageType, string currentUrl)
        {
            var validators = pageType.GetCustomAttributes(true).OfType<IPageUrlValidator>().ToArray();
            if (validators.Length == 0)
            {
                throw new ValidationAbsenceException("Page '{0}' does not have any Url validation. Please add an Url validation."
                    .F(pageType.FullName));
            }

            return validators.Any(x => x.Validate(currentUrl));
        }
    }
}