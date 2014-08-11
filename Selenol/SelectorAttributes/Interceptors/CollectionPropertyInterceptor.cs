// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using OpenQA.Selenium;
using Selenol.Controls;
using Selenol.Elements;

namespace Selenol.SelectorAttributes.Interceptors
{
    /// <summary>
    /// The selector interceptor. It intercepts property getter and select elements by selector from <see cref="BaseSelectorAttribute"/>.
    /// Used for properties with generic collection type based on <see cref="ReadOnlyCollection{T}"/>. 
    /// With generic argument based on <see cref="BaseHtmlElement"/>
    /// Can cache the result if need.
    /// </summary>
    internal class CollectionPropertyInterceptor : BasePropertyInterceptor
    {
        private static readonly MethodInfo elementsMethod = typeof(SearchContextExtensions).GetMethod("Elements",
            BindingFlags.Public | BindingFlags.Static);

        private static readonly MethodInfo controlsMethod = typeof(SearchContextExtensions).GetMethod("Controls",
            BindingFlags.Public | BindingFlags.Static);

        private static readonly MethodInfo toListMethod = typeof(Enumerable).GetMethod("ToList", BindingFlags.Public | BindingFlags.Static);

        private static readonly IDictionary<Type, MethodInfo> typeToGenericCollectionMethod = new Dictionary<Type, MethodInfo>();
        private static readonly IDictionary<Type, MethodInfo> typeToGenericToListMethod = new Dictionary<Type, MethodInfo>();
        private static readonly IDictionary<Type, Type> typeToReadOnlyCollectionType = new Dictionary<Type, Type>();

        /// <summary>Selects value for the proxied property.</summary>
        /// <param name="propertyType">The property type.</param>
        /// <param name="context">The page.</param>
        /// <param name="selector">The selector.</param>
        /// <returns>The property value.</returns>
        protected override object SelectPropertyValue(Type propertyType, object context, By selector)
        {
            var genericArgType = propertyType.GetGenericArguments().Single();
            var elements = SelectElements(context, selector, genericArgType);

            var list = ToList(genericArgType, elements);

            return CreateReadOnlyCollection(genericArgType, list);
        }

        private static object SelectElements(object context, By selector, Type genericArgType)
        {
            if (!typeToGenericCollectionMethod.ContainsKey(genericArgType))
            {
                var factoryMethod = typeof(Control).IsAssignableFrom(genericArgType)
                                        ? controlsMethod
                                        : elementsMethod;
                typeToGenericCollectionMethod[genericArgType] = factoryMethod.MakeGenericMethod(genericArgType);
            }

            var typedCollectionMethod = typeToGenericCollectionMethod[genericArgType];
            return typedCollectionMethod.Invoke(null, new[] { context, selector });
        }

        private static object ToList(Type genericArgType, object elements)
        {
            if (!typeToGenericToListMethod.ContainsKey(genericArgType))
            {
                typeToGenericToListMethod[genericArgType] = toListMethod.MakeGenericMethod(genericArgType);
            }

            var typedToListMethod = typeToGenericToListMethod[genericArgType];
            return typedToListMethod.Invoke(null, new[] { elements });
        }

        private static object CreateReadOnlyCollection(Type genericArgType, object list)
        {
            if (!typeToReadOnlyCollectionType.ContainsKey(genericArgType))
            {
                var readOnlyCollectionType = typeof(ReadOnlyCollection<>);
                typeToReadOnlyCollectionType[genericArgType] = readOnlyCollectionType.MakeGenericType(genericArgType);
            }

            return Activator.CreateInstance(typeToReadOnlyCollectionType[genericArgType], list);
        }
    }
}