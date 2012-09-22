// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using OpenQA.Selenium;

using Selenol.Extensions;
using Selenol.Validation;
using Selenol.Validation.Element;

namespace Selenol.Elements
{
    /// <summary>The base html element.</summary>
    public abstract class BaseHtmlElement
    {
        private const string ParentXPathSelector = "./parent::*";

        private const string NextSiblingXPathSelector = "./following-sibling::*";

        private const string PreviousSiblingXPathSelector = "./preceding-sibling::*";

        /// <summary>Initializes a new instance of the <see cref="BaseHtmlElement"/> class.</summary>
        /// <param name="webElement">The web element.</param>
        protected BaseHtmlElement(IWebElement webElement)
        {
            if (webElement == null)
            {
                throw new ArgumentNullException("webElement");
            }

            this.WebElement = webElement;

            var validators = this.GetType().GetCustomAttributes(false).OfType<IElementValidator>().ToArray();
            if (validators.Length == 0)
            {
                throw new ValidationAbsenceException("Element '{0}' does not have any validation. Please add a validation.".F(this.GetType()));
            }

            if (!validators.Any(x => x.Validate(this)))
            {
                var message = validators.Select(x => x.GetErrorMessage(this)).Join(" or ");
                throw new WrongElementException(message, this.WebElement);
            }
        }

        /// <summary>Gets the element id.</summary>
        /// <remarks>If id attribute does not exist empty string will be returned.</remarks>
        public string Id
        {
            get
            {
                return this.WebElement.GetAttribute(HtmlElementAttributes.Id) ?? string.Empty;
            }
        }

        /// <summary>Gets the element name.</summary>
        /// <remarks>If name attribute does not exist empty string will be returned.</remarks>
        public string Name 
        {
            get
            {
                return this.WebElement.GetAttribute(HtmlElementAttributes.Name) ?? string.Empty;
            }
        }

        /// <summary>Gets the element classes.</summary>
        public IEnumerable<string> Classes
        {
            get
            {
                var attributeValue = this.WebElement.GetAttribute(HtmlElementAttributes.Class);
                return !attributeValue.IsNullOrEmpty() ? attributeValue.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries) : new string[0];
            }
        }

        /// <summary>Gets a value indicating whether the element is displayed or not.</summary>
        public bool IsDisplayed
        {
            get
            {
                return this.WebElement.Displayed;
            }
        }

        /// <summary>Gets a value indicating whether the element is enabled or not.</summary>
        public bool IsEnabled
        {
            get
            {
                return this.WebElement.Enabled;
            }
        }

        /// <summary>Gets a value indicating whether the element has parent or not.</summary>
        public bool HasParent
        {
            get
            {
                return this.HasElement(() => this.Parent);
            }
        }

        /// <summary>Gets a value indicating whether the element has next sibling or not.</summary>
        public bool HasNextSibling
        {
            get
            {
                return this.HasElement(() => this.NextSibling);
            }
        }

        /// <summary>Gets a value indicating whether the element has previous sibling or not.</summary>
        public bool HasPreviousSibling
        {
            get
            {
                return this.HasElement(() => this.PreviousSibling);
            }
        }

        /// <summary>Gets the parent element.</summary>
        public ContainerElement Parent
        {
            get
            {
                return new ContainerElement(this.WebElement.FindElement(By.XPath(ParentXPathSelector)));
            }
        }

        /// <summary>Gets the next sibling element.</summary>
        public BaseHtmlElement NextSibling
        {
            get
            {
                return new BasicHtmlElement(this.WebElement.FindElement(By.XPath(NextSiblingXPathSelector)));
            }
        }

        /// <summary>Gets the previous sibling element.</summary>
        public BaseHtmlElement PreviousSibling
        {
            get
            {
                return new BasicHtmlElement(this.WebElement.FindElement(By.XPath(PreviousSiblingXPathSelector)));
            }
        }

        /// <summary>Gets the element tag name.</summary>
        internal string TagName
        {
            get
            {
                return this.WebElement.TagName;
            }
        }

        /// <summary>Gets the wrapped Selenium element.</summary>
        protected IWebElement WebElement { get; private set; }

        /// <summary>Gets the element attribute value.</summary>
        /// <param name="attributeName">The attribute value.</param>
        /// <returns>The attribute value if it exists otherwise empty string.</returns>
        public string GetAttributeValue(string attributeName)
        {
            if (attributeName == null)
            {
                throw new ArgumentNullException("attributeName");
            }

            return this.WebElement.GetAttribute(attributeName) ?? string.Empty;
        }

        /// <summary>Checks if the element has a class.</summary>
        /// <param name="className">The class name.</param>
        /// <returns>True if element has given class otherwise false.</returns>
        public bool HasClass(string className)
        {
            if (className.IsNullOrEmpty())
            {
                throw new ArgumentNullException("className");
            }

            return this.Classes.Contains(className);
        }

        /// <summary>Checks if the element has an attribute.</summary>
        /// <param name="attributeName">The attribute name.</param>
        /// <returns>True if element has given attribute otherwise false.</returns>
        public bool HasAttribute(string attributeName)
        {
            if (attributeName.IsNullOrEmpty())
            {
                throw new ArgumentNullException("attributeName");
            }

            return this.WebElement.GetAttribute(attributeName) != null;
        }

        /// <summary>Changes the element type to a given one.</summary>
        /// <typeparam name="TElement">The element type.</typeparam>
        /// <returns>The casted element.</returns>
        /// <remarks>Do not cast the element using language constructions, use this method instead. Otherwise <see cref="InvalidCastException"/> will be thrown.</remarks>
        public TElement As<TElement>() where TElement : BaseHtmlElement
        {
            var targetType = typeof(TElement);
            var constructor = targetType.GetConstructors().FirstOrDefault(IsProperElementConstructor);
            if (constructor == null)
            {
                throw new MissingMethodException(
                    "The element type '{0}' does not have constructor with the only one parameter of type IWebElement. Please add it in order to have ability to use As method."
                        .F(targetType));
            }

            try
            {
                return (TElement)constructor.Invoke(new object[] { this.WebElement });
            }
            catch (TargetInvocationException ex)
            {
                if (ex.InnerException != null)
                {
                    throw ex.InnerException;
                }

                throw;
            }
        }

        private static bool IsProperElementConstructor(ConstructorInfo constructor)
        {
            var parameters = constructor.GetParameters();
            return parameters.Length == 1 && parameters[0].ParameterType == typeof(IWebElement);
        }

        private bool HasElement(Func<BaseHtmlElement> elementGetter)
        {
            try
            {
                elementGetter();
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }
}