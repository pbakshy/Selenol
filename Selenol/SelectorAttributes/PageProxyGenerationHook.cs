// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Reflection;
using Castle.DynamicProxy;
using Selenol.Elements;
using Selenol.Extensions;
using Selenol.Page;

namespace Selenol.SelectorAttributes
{
    /// <summary>The Page proxy generation hook. Detects problems in usage of <see cref="BaseSelectorAttribute"/>.</summary>
    internal class PageProxyGenerationHook : IProxyGenerationHook
    {
        private List<string> errorParts;

        /// <summary>
        /// Invoked by the generation process to notify 
        /// that the whole process is completed.
        /// </summary>
        public void MethodsInspected()
        {
            if (this.errorParts != null)
            {
                var message = this.errorParts.Join("\r\n");
                this.errorParts = null;
                throw new PageInitializationException(message);
            }
        }

        /// <summary>Invoked by the generation process to notify that a
        /// member wasn't marked as virtual.</summary>
        /// <param name="type">The type.</param>
        /// <param name="memberInfo">The member Info.</param>
        public void NonProxyableMemberNotification(Type type, MemberInfo memberInfo)
        {
            var methodInfo = memberInfo as MethodInfo;
            if (methodInfo == null || !methodInfo.IsGetter())
            {
                return;
            }

            var property = type.GetProperty(methodInfo);
            if (property.IsPropertyWithSelectorAttribute())
            {
                this.AppendError(type, "'{0}' is not virtual. Selector attributes can be used only for virtual properties.".F(property.Name));
            }
        }

        /// <summary>Invoked by the generation process to know if
        /// the specified member should be intercepted.</summary>
        /// <param name="type">The type.</param>
        /// <param name="methodInfo">The method Info.</param>
        /// <returns>The true if should otherwise false.</returns>
        public bool ShouldInterceptMethod(Type type, MethodInfo methodInfo) 
        {
            if (!methodInfo.IsGetter() && !methodInfo.IsSetter())
            {
                return false;
            }

            var propertyInfo = type.GetProperty(methodInfo);
            if (!propertyInfo.IsPropertyWithSelectorAttribute())
            {
                return false;
            }

            if (!propertyInfo.IsAutoProperty())
            {
                this.AppendError(type, "'{0}' is not an auto property. Selector attributes can be used only for auto properties.".F(propertyInfo.Name));

                return false;
            }

            if (!typeof(BaseHtmlElement).IsAssignableFrom(propertyInfo.PropertyType))
            {
                var errorMessage = "'{0}' property has invalid type. Selector attributes can be used only for properties with type derived from BaseHtmlElement.".F(propertyInfo.Name);
                this.AppendError(type, errorMessage);
                return false;
            }

            if (propertyInfo.PropertyType.IsAbstract)
            {
                this.AppendError(type, "'{0}' property has invalid type. Selector attributes can not be used for abstract types.".F(propertyInfo.Name));
                return false;
            }

            return true;
        }

        private void AppendError(Type type, string errorMessage)
        {
            if (this.errorParts == null)
            {
                this.errorParts = new List<string> { "The page '{0}' can not be initialized.".F(type.FullName) };
            }

            if (!this.errorParts.Contains(errorMessage))
            {
                this.errorParts.Add(errorMessage);
            }
        }
    }
}