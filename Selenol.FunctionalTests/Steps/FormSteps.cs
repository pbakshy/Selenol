// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System.Linq;

using FluentAssertions;

using OpenQA.Selenium;

using Selenol.Elements;

using TechTalk.SpecFlow;

namespace Selenol.FunctionalTests.Steps
{
    [Binding]
    public class FormSteps
    {
        [When(@"I submit form with id ""(.*)""")]
        public void WhenISubmitFormWithId(string id)
        {
            Browser.Current.Form(By.Id(id)).Submit();
        }

        [Then(@"there are forms with id ""(.*)""")]
        public void ThenThereAreFromsWithId(string[] ids)
        {
            Browser.Current.Forms().Select(x => x.Id).Should().BeEquivalentTo(ids.AsEnumerable());
        }
    }
}