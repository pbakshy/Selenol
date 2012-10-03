// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;

using OpenQA.Selenium;

using Selenol.Extensions;

namespace Selenol.Elements
{
    /// <summary>The element styles.</summary>
    public class ElementStyles
    {
        private static readonly Regex longHashColorRx = new Regex(
            @"^#([0-9a-f]{2})([0-9a-f]{2})([0-9a-f]{2})$", 
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private static readonly Regex shortHashColorRx = new Regex(
            @"^#([0-9a-f])([0-9a-f])([0-9a-f])$", 
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private static readonly Regex rgbColorRx = new Regex(
            @"^rgb\s*\(\s*(\d{1,3}),\s*(\d{1,3}),\s*(\d{1,3})\s*\)$", 
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private static readonly Regex rgbaColorRx = new Regex(
            @"^rgba\s*\(\s*(\d{1,3}),\s*(\d{1,3}),\s*(\d{1,3})\s*,\s*(1|0\.\d+)\s*\)$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private readonly IWebElement webElement;

        /// <summary>Initializes a new instance of the <see cref="ElementStyles"/> class.</summary>
        /// <param name="webElement">The web element.</param>
        public ElementStyles(IWebElement webElement)
        {
            this.webElement = webElement;
        }

        /// <summary>Gets the foreground color of the element.</summary>
        public Color ForegroundColor
        {
            get
            {
                return this.ParseColor(CssStyles.ForegroundColor);
            }
        }

        /// <summary>Gets the background color of the element.</summary>
        public Color BackgroundColor
        {
            get
            {
                return this.ParseColor(CssStyles.BackgroundColor);
            }
        }

        /// <summary>Gets the height of the element.</summary>
        public int Height
        {
            get
            {
                return this.webElement.Size.Height;
            }
        }

        /// <summary>Gets the width of the element.</summary>
        public int Width
        {
            get
            {
                return this.webElement.Size.Width;
            }
        }

        /// <summary>Gets a style of the element.</summary>
        /// <param name="styleName">The style name.</param>
        /// <returns>The style value.</returns>
        /// <remarks>If the style does not exist empty string will be returned.</remarks>
        public string GetStyle(string styleName)
        {
            if (styleName.IsNullOrEmpty())
            {
                throw new ArgumentNullException("styleName");
            }

            return this.webElement.GetCssValue(styleName) ?? string.Empty;
        }

        private static int ParseInteger(string value, bool allowHex = true)
        {
            return int.Parse(value, allowHex ? NumberStyles.AllowHexSpecifier : NumberStyles.None, CultureInfo.InvariantCulture);
        }

        private Color ParseColor(string styleName)
        {
            var styleValue = this.GetStyle(styleName);

            if (styleValue.IsNullOrEmpty())
            {
                return Color.Empty;
            }

            var match = longHashColorRx.Match(styleValue);
            if (match.Success)
            {
                return Color.FromArgb(ParseInteger(match.Groups[1].Value), ParseInteger(match.Groups[2].Value), ParseInteger(match.Groups[3].Value));
            }

            match = shortHashColorRx.Match(styleValue);
            if (match.Success)
            {
                var red = ParseInteger(match.Groups[1].Value);
                var green = ParseInteger(match.Groups[2].Value);
                var blue = ParseInteger(match.Groups[3].Value);
                return Color.FromArgb((red << 4) + red, (green << 4) + green, (blue << 4) + blue);
            }

            match = rgbColorRx.Match(styleValue);
            if (match.Success)
            {
                return Color.FromArgb(
                    ParseInteger(match.Groups[1].Value, false), 
                    ParseInteger(match.Groups[2].Value, false), 
                    ParseInteger(match.Groups[3].Value, false));
            }

            match = rgbaColorRx.Match(styleValue);
            if (match.Success)
            {
                var alpha = (int)Math.Round(double.Parse(match.Groups[4].Value, CultureInfo.InvariantCulture) * 255);
                return Color.FromArgb(
                    alpha,
                    ParseInteger(match.Groups[1].Value, false),
                    ParseInteger(match.Groups[2].Value, false),
                    ParseInteger(match.Groups[3].Value, false));
            }

            KnownColor knownColor;
            if (Enum.TryParse(styleValue, true, out knownColor))
            {
                return Color.FromKnownColor(knownColor);
            }

            throw new InvalidValueException(
                "Can't parse color property '{0}'. If value is correct and you need to parse it then use GetStyle method".F(styleName));
        }
    }
}