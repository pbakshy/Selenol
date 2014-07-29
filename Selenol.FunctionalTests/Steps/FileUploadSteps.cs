// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System.IO;
using System.Linq;
using System.Reflection;

using FluentAssertions;

using OpenQA.Selenium;

using Selenol.Elements;

using TechTalk.SpecFlow;

namespace Selenol.FunctionalTests.Steps
{
    [Binding]
    public class FileUploadSteps
    {
        private FileUploadElement fileUpload;

        private string fileName;

        [When(@"I look at file upload with id ""(.*)""")]
        public void WhenILookAtFileUploadWithId(string id)
        {
            this.fileUpload = Browser.Current.FileUpload(By.Id(id));
        }

        [When(@"I choose a file for the file upload")]
        public void WhenIChooseAFileForFileUploadWithId()
        {
            var assemblyPath = Path.GetDirectoryName(Assembly.GetAssembly(typeof(FileUploadSteps)).Location);
// ReSharper disable AssignNullToNotNullAttribute
            var assemblyDirectory = new DirectoryInfo(assemblyPath);
// ReSharper restore AssignNullToNotNullAttribute
            this.fileName = Path.GetFileName(assemblyDirectory.GetFiles().First().FullName);
            this.fileUpload.SelectFile(this.fileName);
        }

        [Then(@"there are file uploads with id ""(.*)""")]
        public void ThenThereAreFileUploadsWithId(string[] ids)
        {
            Browser.Current.FileUploads().Select(x => x.Id).Should().BeEquivalentTo(ids.AsEnumerable());
        }

        [Then(@"the file upload has the file")]
        public void ThenTheFileUploadWithHasTheFile()
        {
            Path.GetFileName(this.fileUpload.FileName).Should().Be(this.fileName);
        }
    }
}