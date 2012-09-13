// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System.IO;
using System.Reflection;

using FluentAssertions;

using NUnit.Framework;

using Rhino.Mocks;

using Selenol.Elements;

namespace Selenol.Tests.Elements
{
    [TestFixture]
    public class TestFileUploadElement : BaseHtmlElementTest<FileUploadElement>
    {
        [Test]
        public void GetFileName()
        {
            this.WebElement.Stub(x => x.GetAttribute("value")).Return(@"c:\sample.txt");
            this.TypedElement.FileName.Should().Be(@"c:\sample.txt");
        }

        [Test]
        public void SelectFile()
        {
            var fileName = GetAFile().FullName;
            this.TypedElement.SelectFile(fileName);
            this.WebElement.AssertWasCalled(x => x.SendKeys(fileName));
        }

        [Test, ExpectedException(typeof(FileNotFoundException))]
        public void SelectNotExistingFile()
        {
            var fileName = GetAFile().FullName + ".something.not.existing";
            this.TypedElement.SelectFile(fileName);
        }

        protected override FileUploadElement CreateElement()
        {
            return new FileUploadElement(this.WebElement);
        }

        protected override void SetProperElementConditions()
        {
            this.WebElement.Stub(x => x.TagName).Return("input");
            this.WebElement.Stub(x => x.GetAttribute("type")).Return("file");
        }

        private static FileInfo GetAFile()
        {
            return new FileInfo(Assembly.GetExecutingAssembly().Location);
        }
    }
}