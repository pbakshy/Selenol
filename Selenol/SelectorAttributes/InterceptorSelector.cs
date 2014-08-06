// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;
using Selenol.Elements;
using Selenol.Extensions;
using Selenol.SelectorAttributes.Interceptors;

namespace Selenol.SelectorAttributes
{
    /// <summary>The interceptor selector which selects appropriate interceptor where it needs.</summary>
    internal class InterceptorSelector : IInterceptorSelector
    {
        /// <summary>Select interceptors which must be applied to the method.</summary>
        /// <param name="type">The type.</param>
        /// <param name="method">The method.</param>
        /// <param name="interceptors">The interceptors.</param>
        /// <returns>The interceptors after filtering.</returns>
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            if (method.IsGetter())
            {
                var propertyInfo = type.GetProperty(method);
                if (propertyInfo.IsPropertyWithSelectorAttribute())
                {
                    return typeof(BaseHtmlElement).IsAssignableFrom(method.ReturnType) 
                        ? interceptors.Where(x => x is ElementPropertyInterceptor).ToArray() 
                        : interceptors.Where(x => x is ElementCollectionPropertyInterceptor).ToArray();
                }
            }

            if (method.IsSetter())
            {
                var propertyInfo = type.GetProperty(method);
                if (propertyInfo.IsPropertyWithSelectorAttribute())
                {
                    return interceptors.Where(x => x is InvalidWriteOperationInterceptor).ToArray();
                }
            }

            return interceptors.Where(x => !(x is ElementPropertyInterceptor) 
                && !(x is ElementCollectionPropertyInterceptor)
                && !(x is InvalidWriteOperationInterceptor)).ToArray();
        }
    }
}