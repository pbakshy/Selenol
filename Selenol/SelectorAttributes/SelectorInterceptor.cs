// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;
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

        private static readonly IDictionary<Type, MethodInfo> typeToGenericElementMethod = new Dictionary<Type, MethodInfo>();

        /// <summary>The intercept.</summary>
        /// <param name="invocation">The invocation.</param>
        public void Intercept(IInvocation invocation)
        {
            var methodInfo = invocation.Method;
            var propertyInfo = invocation.TargetType.GetProperty(methodInfo);
            var selectorAttribute = Attribute.GetCustomAttributes(propertyInfo).OfType<BaseSelectorAttribute>().First();

            var propertyType = propertyInfo.PropertyType;
            if (!typeToGenericElementMethod.ContainsKey(propertyType))
            {
                typeToGenericElementMethod[propertyType] = elementMethod.MakeGenericMethod(propertyType);
            }

            var typedEmelementMethod = typeToGenericElementMethod[propertyType];
            var page = (BasePage)invocation.InvocationTarget;
            invocation.ReturnValue = typedEmelementMethod.Invoke(null, new object[] { page.Context, selectorAttribute.Selector });
        }
    }
}