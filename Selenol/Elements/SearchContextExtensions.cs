// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using OpenQA.Selenium;
using Selenol.Controls;
using Selenol.Page;

namespace Selenol.Elements
{
    /// <summary>The search context extensions.</summary>
    public static class SearchContextExtensions
    {
        private const string ButtonsDefaultSelector =
            HtmlElements.Button + "," + HtmlElements.Input + "[" + HtmlElementAttributes.Type + "='" + HtmlInputTypes.Button + "']";

        private const string CheckboxesDefaultSelector = HtmlElements.Input + "[" + HtmlElementAttributes.Type + "='" + HtmlInputTypes.Checkbox + "']";

        private const string FileUploadsDefaultSelector = HtmlElements.Input + "[" + HtmlElementAttributes.Type + "='" + HtmlInputTypes.File + "']";

        private const string ElementsDefaultSelector = "*";

        private const string RadioButtonsDefaultSelector = HtmlElements.Input + "[" + HtmlElementAttributes.Type + "='" + HtmlInputTypes.RadioButton + "']";

        private const string TextboxesDefaultSelector = HtmlElements.Input + "[" + HtmlElementAttributes.Type + "='" + HtmlInputTypes.Textbox + "']";

        private const string PasswordboxesDefaultSelector = HtmlElements.Input + "[" + HtmlElementAttributes.Type + "='" + HtmlInputTypes.Passwordbox + "']";

        private const string SelectDefaultSelector = HtmlElements.Select + ":not([" + HtmlElementAttributes.Multiple + "])";

        private const string MultiSelectDefaultSelector = HtmlElements.Select + "[" + HtmlElementAttributes.Multiple + "]";

        /// <summary>Finds a button that meets a criteria.</summary>
        /// <param name="context">The search context.</param>
        /// <param name="by">The criteria.</param>
        /// <returns>The found button.</returns>
        public static ButtonElement Button(this ISearchContext context, By by)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            if (@by == null)
            {
                throw new ArgumentNullException("by");
            }

            return new ButtonElement(context.FindElement(by));
        }

        /// <summary>Finds buttons that meets a criteria.</summary>
        /// <param name="context">The search context.</param>
        /// <param name="by">The criteria.</param>
        /// <returns>The found buttons.</returns>
        /// <remarks>If <paramref name="by"/> is skipped all buttons will be returned.</remarks>
        public static IEnumerable<ButtonElement> Buttons(this ISearchContext context, By by = null)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            return context.FindElements(by ?? By.CssSelector(ButtonsDefaultSelector)).Select(x => new ButtonElement(x)).ToArray();
        }

        /// <summary>Finds a checkbox that meets a criteria.</summary>
        /// <param name="context">The search context.</param>
        /// <param name="by">The criteria.</param>
        /// <returns>The found checkbox.</returns>
        public static CheckboxElement Checkbox(this ISearchContext context, By by)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            if (@by == null)
            {
                throw new ArgumentNullException("by");
            }

            return new CheckboxElement(context.FindElement(by));
        }

        /// <summary>Finds checkboxes that meets a criteria.</summary>
        /// <param name="context">The search context.</param>
        /// <param name="by">The criteria.</param>
        /// <returns>The found checkboxes.</returns>
        /// <remarks>If <paramref name="by"/> is skipped all checkboxes will be returned.</remarks>
        public static IEnumerable<CheckboxElement> Checkboxes(this ISearchContext context, By by = null)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            return context.FindElements(by ?? By.CssSelector(CheckboxesDefaultSelector)).Select(x => new CheckboxElement(x)).ToArray();
        }

        /// <summary>Finds a file upload element that meets a criteria.</summary>
        /// <param name="context">The search context.</param>
        /// <param name="by">The criteria.</param>
        /// <returns>The found file upload element.</returns>
        public static FileUploadElement FileUpload(this ISearchContext context, By by)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            if (@by == null)
            {
                throw new ArgumentNullException("by");
            }

            return new FileUploadElement(context.FindElement(by));
        }

        /// <summary>Finds file upload elements that meets a criteria.</summary>
        /// <param name="context">The search context.</param>
        /// <param name="by">The criteria.</param>
        /// <returns>The found file upload elements.</returns>
        /// <remarks>If <paramref name="by"/> is skipped all file upload elements will be returned.</remarks>
        public static IEnumerable<FileUploadElement> FileUploads(this ISearchContext context, By by = null)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            return context.FindElements(by ?? By.CssSelector(FileUploadsDefaultSelector)).Select(x => new FileUploadElement(x)).ToArray();
        }

        /// <summary>Finds a form that meets a criteria.</summary>
        /// <param name="context">The search context.</param>
        /// <param name="by">The criteria.</param>
        /// <returns>The found form.</returns>
        public static FormElement Form(this ISearchContext context, By by)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            if (@by == null)
            {
                throw new ArgumentNullException("by");
            }

            return new FormElement(context.FindElement(by));
        }

        /// <summary>Finds forms that meets a criteria.</summary>
        /// <param name="context">The search context.</param>
        /// <param name="by">The criteria.</param>
        /// <returns>The found forms.</returns>
        /// <remarks>If <paramref name="by"/> is skipped all forms will be returned.</remarks>
        public static IEnumerable<FormElement> Forms(this ISearchContext context, By by = null)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            return context.FindElements(by ?? By.CssSelector(HtmlElements.Form)).Select(x => new FormElement(x)).ToArray();
        }

        /// <summary>Finds a element that meets a criteria.</summary>
        /// <param name="context">The search context.</param>
        /// <param name="by">The criteria.</param>
        /// <returns>The found element.</returns>
        public static ContainerElement Container(this ISearchContext context, By by)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            if (@by == null)
            {
                throw new ArgumentNullException("by");
            }

            return new ContainerElement(context.FindElement(by));
        }

        /// <summary>Finds elements that meets a criteria.</summary>
        /// <param name="context">The search context.</param>
        /// <param name="by">The criteria.</param>
        /// <returns>The found elements.</returns>
        /// <remarks>If <paramref name="by"/> is skipped all elements will be returned.</remarks>
        public static IEnumerable<ContainerElement> Containers(this ISearchContext context, By by = null)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            return context.FindElements(by ?? By.CssSelector(ElementsDefaultSelector)).Select(x => new ContainerElement(x)).ToArray();
        }

        /// <summary>Finds a link that meets a criteria.</summary>
        /// <param name="context">The search context.</param>
        /// <param name="by">The criteria.</param>
        /// <returns>The found link.</returns>
        public static LinkElement Link(this ISearchContext context, By by)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            if (@by == null)
            {
                throw new ArgumentNullException("by");
            }

            return new LinkElement(context.FindElement(by));
        }

        /// <summary>Finds links that meets a criteria.</summary>
        /// <param name="context">The search context.</param>
        /// <param name="by">The criteria.</param>
        /// <returns>The found links.</returns>
        /// <remarks>If <paramref name="by"/> is skipped all links will be returned.</remarks>
        public static IEnumerable<LinkElement> Links(this ISearchContext context, By by = null)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            return context.FindElements(by ?? By.CssSelector(HtmlElements.Link)).Select(x => new LinkElement(x)).ToArray();
        }

        /// <summary>Finds a multi select that meets a criteria.</summary>
        /// <param name="context">The search context.</param>
        /// <param name="by">The criteria.</param>
        /// <returns>The found multi select.</returns>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public static MultiSelectElement MultiSelect(this ISearchContext context, By by)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            if (@by == null)
            {
                throw new ArgumentNullException("by");
            }

            return new MultiSelectElement(context.FindElement(by));
        }

        /// <summary>Finds multi selects that meets a criteria.</summary>
        /// <param name="context">The search context.</param>
        /// <param name="by">The criteria.</param>
        /// <returns>The found multi selects.</returns>
        /// <remarks>If <paramref name="by"/> is skipped all multi selects will be returned.</remarks>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public static IEnumerable<MultiSelectElement> MultiSelects(this ISearchContext context, By by = null)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            return context.FindElements(by ?? By.CssSelector(MultiSelectDefaultSelector)).Select(x => new MultiSelectElement(x)).ToArray();
        }

        /// <summary>Finds a radio button that meets a criteria.</summary>
        /// <param name="context">The search context.</param>
        /// <param name="by">The criteria.</param>
        /// <returns>The found radio buttons.</returns>
        public static RadioButtonElement RadioButton(this ISearchContext context, By by)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            if (@by == null)
            {
                throw new ArgumentNullException("by");
            }

            return new RadioButtonElement(context.FindElement(by));
        }

        /// <summary>Finds radio buttons that meets a criteria.</summary>
        /// <param name="context">The search context.</param>
        /// <param name="by">The criteria.</param>
        /// <returns>The found radio buttons.</returns>
        /// <remarks>If <paramref name="by"/> is skipped all radio buttons will be returned.</remarks>
        public static IEnumerable<RadioButtonElement> RadioButtons(this ISearchContext context, By by = null)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            return context.FindElements(by ?? By.CssSelector(RadioButtonsDefaultSelector)).Select(x => new RadioButtonElement(x)).ToArray();
        }

        /// <summary>Finds a select that meets a criteria.</summary>
        /// <param name="context">The search context.</param>
        /// <param name="by">The criteria.</param>
        /// <returns>The found select.</returns>
        public static SelectElement Select(this ISearchContext context, By by)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            if (@by == null)
            {
                throw new ArgumentNullException("by");
            }

            return new SelectElement(context.FindElement(by));
        }

        /// <summary>Finds selects that meets a criteria.</summary>
        /// <param name="context">The search context.</param>
        /// <param name="by">The criteria.</param>
        /// <returns>The found selects.</returns>
        /// <remarks>If <paramref name="by"/> is skipped all selects will be returned.</remarks>
        public static IEnumerable<SelectElement> Selects(this ISearchContext context, By by = null)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            return context.FindElements(by ?? By.CssSelector(SelectDefaultSelector)).Select(x => new SelectElement(x)).ToArray();
        }

        /// <summary>Finds a table that meets a criteria.</summary>
        /// <param name="context">The search context.</param>
        /// <param name="by">The criteria.</param>
        /// <returns>The found table.</returns>
        public static TableElement Table(this ISearchContext context, By by)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            if (@by == null)
            {
                throw new ArgumentNullException("by");
            }

            return new TableElement(context.FindElement(by));
        }

        /// <summary>Finds tables that meets a criteria.</summary>
        /// <param name="context">The search context.</param>
        /// <param name="by">The criteria.</param>
        /// <returns>The found tables.</returns>
        /// <remarks>If <paramref name="by"/> is skipped all tables will be returned.</remarks>
        public static IEnumerable<TableElement> Tables(this ISearchContext context, By by = null)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            return context.FindElements(by ?? By.CssSelector(HtmlElements.Table)).Select(x => new TableElement(x)).ToArray();
        }

        /// <summary>Finds a text area that meets a criteria.</summary>
        /// <param name="context">The search context.</param>
        /// <param name="by">The criteria.</param>
        /// <returns>The found text area.</returns>
        public static TextAreaElement TextArea(this ISearchContext context, By by)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            if (@by == null)
            {
                throw new ArgumentNullException("by");
            }

            return new TextAreaElement(context.FindElement(by));
        }

        /// <summary>Finds text areas that meets a criteria.</summary>
        /// <param name="context">The search context.</param>
        /// <param name="by">The criteria.</param>
        /// <returns>The found text areas.</returns>
        /// <remarks>If <paramref name="by"/> is skipped all text areas will be returned.</remarks>
        public static IEnumerable<TextAreaElement> TextAreas(this ISearchContext context, By by = null)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            return context.FindElements(by ?? By.CssSelector(HtmlElements.TextArea)).Select(x => new TextAreaElement(x)).ToArray();
        }

        /// <summary>Finds a textbox that meets a criteria.</summary>
        /// <param name="context">The search context.</param>
        /// <param name="by">The criteria.</param>
        /// <returns>The found textbox.</returns>
        public static TextboxElement Textbox(this ISearchContext context, By by)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            if (@by == null)
            {
                throw new ArgumentNullException("by");
            }

            return new TextboxElement(context.FindElement(by));
        }

        /// <summary>Finds textboxes that meets a criteria.</summary>
        /// <param name="context">The search context.</param>
        /// <param name="by">The criteria.</param>
        /// <returns>The found textboxes.</returns>
        /// <remarks>If <paramref name="by"/> is skipped all textboxes will be returned.</remarks>
        public static IEnumerable<TextboxElement> Textboxes(this ISearchContext context, By by = null)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            return context.FindElements(by ?? By.CssSelector(TextboxesDefaultSelector)).Select(x => new TextboxElement(x)).ToArray();
        }

        /// <summary>Finds a user defined element that meets a criteria.</summary>
        /// <typeparam name="TElement">The type of user defined element.</typeparam>
        /// <param name="context">The search context.</param>
        /// <param name="by">The criteria.</param>
        /// <returns>The found a user defined element.</returns>
        public static TElement Element<TElement>(this ISearchContext context, By by) where TElement : BaseHtmlElement
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            if (@by == null)
            {
                throw new ArgumentNullException("by");
            }

            return new BasicHtmlElement(context.FindElement(by)).As<TElement>();
        }

        /// <summary>Finds user defined elements that meets a criteria.</summary>
        /// <typeparam name="TElement">The type of user defined element.</typeparam>
        /// <param name="context">The search context.</param>
        /// <param name="by">The criteria.</param>
        /// <returns>The found user defined elements.</returns>
        public static IEnumerable<TElement> Elements<TElement>(this ISearchContext context, By by) where TElement : BaseHtmlElement
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            if (@by == null)
            {
                throw new ArgumentNullException("by");
            }

            return context.FindElements(by).Select(x => new BasicHtmlElement(x).As<TElement>()).ToArray();
        }

        /// <summary>Finds a user defined controls that meets a criteria.</summary>
        /// <typeparam name="TControl">The type of user defined control.</typeparam>
        /// <param name="context">The search context.</param>
        /// <param name="by">The criteria.</param>
        /// <returns>The found a user defined control.</returns>
        public static TControl Control<TControl>(this ISearchContext context, By by) where TControl : Control
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            if (@by == null)
            {
                throw new ArgumentNullException("by");
            }

            return ContainerFactory.Control<TControl>(context.FindElement(by));
        }

        /// <summary>Finds user defined controls that meets a criteria.</summary>
        /// <typeparam name="TControl">The type of user defined control.</typeparam>
        /// <param name="context">The search context.</param>
        /// <param name="by">The criteria.</param>
        /// <returns>The found user defined controls.</returns>
        public static IEnumerable<TControl> Controls<TControl>(this ISearchContext context, By by) where TControl : Control
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            if (@by == null)
            {
                throw new ArgumentNullException("by");
            }

            return context.FindElements(by).Select(ContainerFactory.Control<TControl>);
        }

        /// <summary>Finds a password box that meets a criteria.</summary>
        /// <param name="context">The search context.</param>
        /// <param name="by">The criteria.</param>
        /// <returns>The found password box.</returns>
        public static PasswordboxElement Passwordbox(this ISearchContext context, By by)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            if (@by == null)
            {
                throw new ArgumentNullException("by");
            }

            return new PasswordboxElement(context.FindElement(by));
        }

        /// <summary>Finds password boxes that meets a criteria.</summary>
        /// <param name="context">The search context.</param>
        /// <param name="by">The criteria.</param>
        /// <returns>The found password boxes.</returns>
        /// <remarks>If <paramref name="by"/> is skipped all password boxes will be returned.</remarks>
        public static IEnumerable<PasswordboxElement> Passwordboxes(this ISearchContext context, By by = null)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            return context.FindElements(by ?? By.CssSelector(PasswordboxesDefaultSelector)).Select(x => new PasswordboxElement(x)).ToArray();
        }
    }
}