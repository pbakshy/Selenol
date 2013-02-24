// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        private Type proxiedType;
        private List<PropertyInfo> processedProperties;

        /// <summary>
        /// Invoked by the generation process to notify 
        /// that the whole process is completed.
        /// </summary>
        public void MethodsInspected()
        {
            var notPublicProperties = this.proxiedType.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic)
                .Except(this.processedProperties)
                .Where(notPublicProperty => notPublicProperty.IsPropertyWithSelectorAttribute());
            foreach (var notPublicProperty in notPublicProperties)
            {
                this.AppendError("Selector attributes can not be used for private or internal property '{0}'. Make property public or protected."
                    .F(notPublicProperty.Name));
            }

            this.proxiedType = null;
            this.processedProperties = null;
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
            this.SetProxiedTypeIfNeed(type);
            var methodInfo = memberInfo as MethodInfo;
            if (methodInfo == null || !methodInfo.IsGetter())
            {
                return;
            }

            var property = type.GetProperty(methodInfo);
            this.AppedProcessedProperty(property);
            if (property.IsPropertyWithSelectorAttribute())
            {
                this.AppendError("'{0}' is not virtual. Selector attributes can be used only for virtual properties.".F(property.Name));
            }
        }

        /// <summary>Invoked by the generation process to know if
        /// the specified member should be intercepted.</summary>
        /// <param name="type">The type.</param>
        /// <param name="methodInfo">The method Info.</param>
        /// <returns>The true if should otherwise false.</returns>
        public bool ShouldInterceptMethod(Type type, MethodInfo methodInfo) 
        {
            this.SetProxiedTypeIfNeed(type);
            if (!methodInfo.IsGetter() && !methodInfo.IsSetter())
            {
                return false;
            }

            var propertyInfo = type.GetProperty(methodInfo);
            this.AppedProcessedProperty(propertyInfo);
            if (!propertyInfo.IsPropertyWithSelectorAttribute())
            {
                return false;
            }

            if (!propertyInfo.IsAutoProperty())
            {
                this.AppendError("'{0}' is not an auto property. Selector attributes can be used only for auto properties.".F(propertyInfo.Name));

                return false;
            }

            var propertyType = propertyInfo.PropertyType;
            var isValidCollectionType = IsValidCollectionType(propertyType);
            if (isValidCollectionType && propertyType.GetGenericArguments().Single().IsAbstract)
            {
                var errorMessage = "'{0}' property has invalid type. Generic type argument can not be abstract. For example use ReadOnlyCollection<ButtonElement> instead of ReadOnlyCollection<BaseHtmlElement>."
                    .F(propertyInfo.Name);
                this.AppendError(errorMessage);
                return false;
            }

            if (!isValidCollectionType && !typeof(BaseHtmlElement).IsAssignableFrom(propertyType))
            {
                var errorMessage = "'{0}' property has invalid type. Selector attributes can be used only for properties with type derived from BaseHtmlElement or assignable from ReadOnlyCollection<T> where T : BaseHtmlElement."
                    .F(propertyInfo.Name);
                this.AppendError(errorMessage);
                return false;
            }

            if (!isValidCollectionType && propertyType.IsAbstract)
            {
                this.AppendError("'{0}' property has invalid type. Selector attributes can not be used for abstract types.".F(propertyInfo.Name));
                return false;
            }

            return true;
        }

        private static bool IsValidCollectionType(Type type)
        {
            if (!type.IsGenericType)
            {
                return false;
            }

            var genericTypeDefinition = type.GetGenericTypeDefinition();
            if (!genericTypeDefinition.IsAssignableFromGenericType(typeof(ReadOnlyCollection<>)))
            {
                return false;
            }

            var genericArguments = type.GetGenericArguments();
            if (genericArguments.Length != 1)
            {
                return false;
            }

            var genericArgument = genericArguments.Single();
            return typeof(BaseHtmlElement).IsAssignableFrom(genericArgument);
        }

        private void SetProxiedTypeIfNeed(Type type)
        {
            if (this.proxiedType == null)
            {
                this.proxiedType = type;
            }
        }

        private void AppedProcessedProperty(PropertyInfo processedProperty)
        {
            if (this.processedProperties == null)
            {
                this.processedProperties = new List<PropertyInfo>();
            }

            this.processedProperties.Add(processedProperty);
        }

        private void AppendError(string errorMessage)
        {
            if (this.errorParts == null)
            {
                this.errorParts = new List<string> { "The page '{0}' can not be initialized.".F(this.proxiedType.FullName) };
            }

            if (!this.errorParts.Contains(errorMessage))
            {
                this.errorParts.Add(errorMessage);
            }
        }
    }
}