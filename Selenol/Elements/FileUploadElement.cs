// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System;
using System.IO;

using OpenQA.Selenium;

using Selenol.Extensions;
using Selenol.Validation;

namespace Selenol.Elements
{
    /// <summary>The html file upload element.</summary>
    [Input(HtmlInputTypes.File)]
    public class FileUploadElement : BaseHtmlElement
    {
        /// <summary>Initializes a new instance of the <see cref="FileUploadElement"/> class.</summary>
        /// <param name="webElement">The web element.</param>
        public FileUploadElement(IWebElement webElement)
            : base(webElement)
        {
        }

        /// <summary>Gets the selected file name.</summary>
        public string FileName
        {
            get
            {
                return this.GetAttributeValue(HtmlElementAttributes.Value);
            }
        }

        /// <summary>Select a file.</summary>
        /// <param name="fullFileName">The full file name.</param>
        public void SelectFile(string fullFileName)
        {
            if (fullFileName.IsNullOrEmpty())
            {
                throw new ArgumentNullException("fullFileName");
            }

            var file = new FileInfo(fullFileName);
            if (!file.Exists)
            {
                throw new FileNotFoundException("File '{0}' does not exist.".F(fullFileName));
            }

            this.WebElement.SendKeys(file.FullName);
        }
    }
}