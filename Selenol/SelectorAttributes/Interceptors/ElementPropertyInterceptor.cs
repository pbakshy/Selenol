// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Reflection;
using OpenQA.Selenium;
using Selenol.Elements;
using Selenol.Page;

namespace Selenol.SelectorAttributes.Interceptors
{
    /// <summary>
    /// The selector interceptor. It intercepts property getter and select elements by selector from <see cref="BaseSelectorAttribute"/>.
    /// Used for properties with type based on <see cref="BaseHtmlElement"/>.
    /// Can cache the result if need.
    /// </summary>
    internal class ElementPropertyInterceptor : BaseElementPropertyInterceptor
    {
        private static readonly MethodInfo elementMethod = typeof(SearchContextExtensions).GetMethod("Element",
            BindingFlags.Public | BindingFlags.Static);

        private static readonly IDictionary<Type, MethodInfo> typeToGenericElementMethod = new Dictionary<Type, MethodInfo>();

        /// <summary>Selects value for the proxied property.</summary>
        /// <param name="propertyType">The property type.</param>
        /// <param name="page">The page.</param>
        /// <param name="selector">The selector.</param>
        /// <returns>The property value.</returns>
        protected override object SelectPropertyValue(Type propertyType, BasePage page, By selector)
        {
            if (!typeToGenericElementMethod.ContainsKey(propertyType))
            {
                typeToGenericElementMethod[propertyType] = elementMethod.MakeGenericMethod(propertyType);
            }

            var typedEmelementMethod = typeToGenericElementMethod[propertyType];
            return typedEmelementMethod.Invoke(null, new object[] { page.Context, selector });
        }
    }
}