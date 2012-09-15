// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;

using OpenQA.Selenium;

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
        public static GenericContainerElement Element(this ISearchContext context, By by)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            if (@by == null)
            {
                throw new ArgumentNullException("by");
            }

            return new GenericContainerElement(context.FindElement(by));
        }

        /// <summary>Finds elements that meets a criteria.</summary>
        /// <param name="context">The search context.</param>
        /// <param name="by">The criteria.</param>
        /// <returns>The found elements.</returns>
        /// <remarks>If <paramref name="by"/> is skipped all elements will be returned.</remarks>
        public static IEnumerable<GenericContainerElement> Elements(this ISearchContext context, By by = null)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            return context.FindElements(by ?? By.CssSelector(ElementsDefaultSelector)).Select(x => new GenericContainerElement(x)).ToArray();
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

        /// <summary>Finds a listbox that meets a criteria.</summary>
        /// <param name="context">The search context.</param>
        /// <param name="by">The criteria.</param>
        /// <returns>The found listbox.</returns>
        public static ListboxElement Listbox(this ISearchContext context, By by)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            if (@by == null)
            {
                throw new ArgumentNullException("by");
            }

            return new ListboxElement(context.FindElement(by));
        }

        /// <summary>Finds listboxes that meets a criteria.</summary>
        /// <param name="context">The search context.</param>
        /// <param name="by">The criteria.</param>
        /// <returns>The found listboxes.</returns>
        /// <remarks>If <paramref name="by"/> is skipped all listobxes will be returned.</remarks>
        public static IEnumerable<ListboxElement> Listboxes(this ISearchContext context, By by = null)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            return context.FindElements(by ?? By.CssSelector(HtmlElements.Select)).Select(x => new ListboxElement(x)).ToArray();
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

            return context.FindElements(by ?? By.CssSelector(HtmlElements.Select)).Select(x => new SelectElement(x)).ToArray();
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
    }
}