// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System;

using Selenol.Extensions;
using Selenol.Page;

namespace Selenol.Validation.Page
{
    /// <summary>The attribute thats validate page by an url part. When multiple attributes are specified validation is performed by "Or" clause.</summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class UrlAttribute : Attribute, IPageUrlValidator
    {
        private readonly string urlPart;

        /// <summary>Initializes a new instance of the <see cref="UrlAttribute"/> class.</summary>
        /// <param name="urlPart">The url part that will be used to validate page url. Recommended to use relative path.
        /// <example>
        ///     For the web page which accessable by "http://mysite.com/admin/index.aspx" the parameter should be "/admin/index.aspx".</example>
        /// </param>
        public UrlAttribute(string urlPart)
        {
            if (string.IsNullOrWhiteSpace(urlPart))
            {
                throw new ArgumentException("Value can not be null, empty string or only whitespaces", "urlPart");
            }

            this.urlPart = urlPart;
        }

        /// <summary>Gets or sets a value indicating whether SSL protection required for a page.</summary>
        public bool Https { get; set; }

        /// <summary>Validates the page url. Checks that a page url contains a url part.</summary>
        /// <param name="page">The page. </param>
        /// <param name="currentUrl">The current Url. </param>
        /// <returns>True if page url is valid otherwise false. </returns>
        public bool Validate(BasePage page, string currentUrl)
        {
            if (page == null)
            {
                throw new ArgumentNullException("page");
            }

            return this.CheckHttps(currentUrl) && currentUrl.Contains(this.urlPart);
        }

        /// <summary>Gets an error message for an invalid page.</summary>
        /// <param name="page">The page. </param>
        /// <param name="currentUrl">The current Url. </param>
        /// <returns>The error message. </returns>
        public string GetErrorMessage(BasePage page, string currentUrl)
        {
            if (page == null)
            {
                throw new ArgumentNullException("page");
            }

            return this.CheckHttps(currentUrl) 
                ? "Current url '{0}' does not contain '{1}' part.".F(currentUrl, this.urlPart)
                : "Current url '{0}' must be accessed by HTTPS protocol.".F(currentUrl);
        }

        private bool CheckHttps(string pageUrl)
        {
            return !this.Https || pageUrl.StartsWith("https", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}