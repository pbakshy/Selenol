// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;
using OpenQA.Selenium;
using Selenol.Elements;
using Selenol.Extensions;
using Selenol.Page;

namespace Selenol.SelectorAttributes
{
    /// <summary>
    /// The selector interceptor. It intercepts property getter and select elements by selector from <see cref="BaseSelectorAttribute"/>.
    /// Can cache the result if need.
    /// </summary>
    internal class SelectorInterceptor : IInterceptor
    {
        private static readonly MethodInfo elementMethod = typeof(SearchContextExtensions).GetMethod("Element",
            BindingFlags.Public | BindingFlags.Static);

        private static readonly MethodInfo collectionMethod = typeof(SearchContextExtensions).GetMethod("Elements",
            BindingFlags.Public | BindingFlags.Static);

        private static readonly MethodInfo toListMethod = typeof(Enumerable).GetMethod("ToList", BindingFlags.Public | BindingFlags.Static);

        private static readonly IDictionary<Type, MethodInfo> typeToGenericElementMethod = new Dictionary<Type, MethodInfo>();
        private static readonly IDictionary<Type, MethodInfo> typeToGenericCollectionMethod = new Dictionary<Type, MethodInfo>();
        private static readonly IDictionary<Type, MethodInfo> typeToGenericToListMethod = new Dictionary<Type, MethodInfo>();
        private static readonly IDictionary<Type, Type> typeToReadOnlyCollectionType = new Dictionary<Type, Type>();

        /// <summary>The intercept.</summary>
        /// <param name="invocation">The invocation.</param>
        public void Intercept(IInvocation invocation)
        {
            var methodInfo = invocation.Method;
            var propertyInfo = invocation.TargetType.GetProperty(methodInfo);
            var selectorAttribute = Attribute.GetCustomAttributes(propertyInfo).OfType<BaseSelectorAttribute>().First();

            var propertyType = propertyInfo.PropertyType;

            var page = (BasePage)invocation.InvocationTarget;
            invocation.ReturnValue = typeof(BaseHtmlElement).IsAssignableFrom(propertyType)
                                         ? SelectScalarElement(propertyType, page, selectorAttribute.Selector)
                                         : SelectReadOnlyCollection(propertyType, page, selectorAttribute.Selector);
        }

        private static object SelectScalarElement(Type propertyType, BasePage page, By selector)
        {
            if (!typeToGenericElementMethod.ContainsKey(propertyType))
            {
                typeToGenericElementMethod[propertyType] = elementMethod.MakeGenericMethod(propertyType);
            }

            var typedEmelementMethod = typeToGenericElementMethod[propertyType];
            return typedEmelementMethod.Invoke(null, new object[] { page.Context, selector });
        }

        private static object SelectReadOnlyCollection(Type propertyType, BasePage page, By selector)
        {
            var genericArgType = propertyType.GetGenericArguments().Single();
            var elements = SelectElements(page, selector, genericArgType);

            var list = ToList(genericArgType, elements);

            return CreateReadOnlyCollection(genericArgType, list);
        }

        private static object SelectElements(BasePage page, By selector, Type genericArgType)
        {
            if (!typeToGenericCollectionMethod.ContainsKey(genericArgType))
            {
                typeToGenericCollectionMethod[genericArgType] = collectionMethod.MakeGenericMethod(genericArgType);
            }

            var typedCollectionMethod = typeToGenericCollectionMethod[genericArgType];
            return typedCollectionMethod.Invoke(null, new object[] { page.Context, selector });
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