// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System.Diagnostics.CodeAnalysis;

namespace Selenol.Elements
{
    /// <summary>The css style names.</summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
    public class CssStyles
    {
        /// <summary>The name of the foreground color.</summary>
        public const string ForegroundColor = "color";

        /// <summary>The name of the background color.</summary>
        public const string BackgroundColor = "background-color";
    }
}