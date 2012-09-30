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
        private string fileName;

        [When(@"I choose a file for file upload with id ""(.*)""")]
        public void WhenIChooseAFileForFileUploadWithId(string id)
        {
            var assemblyPath = Path.GetDirectoryName(Assembly.GetAssembly(typeof(FileUploadSteps)).Location);
// ReSharper disable AssignNullToNotNullAttribute
            var assemblyDirectory = new DirectoryInfo(assemblyPath);
// ReSharper restore AssignNullToNotNullAttribute
            this.fileName = assemblyDirectory.GetFiles().First().FullName;
            GetFileUpload(id).SelectFile(this.fileName);
        }

        [Then(@"there are file uploads with id ""(.*)""")]
        public void ThenThereAreFileUploadsWithId(string[] ids)
        {
            Browser.Current.FileUploads().Select(x => x.Id).Should().BeEquivalentTo(ids.AsEnumerable());
        }

        [Then(@"the file upload with ""(.*)"" has the file")]
        public void ThenTheFileUploadWithHasTheFile(string id)
        {
            GetFileUpload(id).FileName.Should().Be(this.fileName);
        }

        private static FileUploadElement GetFileUpload(string id)
        {
            return Browser.Current.FileUpload(By.Id(id));
        }
    }
}