// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System.Linq;

using FluentAssertions;

using OpenQA.Selenium;

using Selenol.Elements;

using TechTalk.SpecFlow;

namespace Selenol.FunctionalTests.Steps
{
    [Binding]
    public class TableSteps
    {
        private TableElement table;

        [When(@"I look at table with id ""(.*)""")]
        public void WhenIBrowsingTableWithId(string id)
        {
            this.table = Browser.Current.Table(By.Id(id));
        }

        [Then(@"there are tables with id ""(.*)""")]
        public void ThenThereAreTablesWithId(string[] ids)
        {
            Browser.Current.Tables().Select(x => x.Id).Should().BeEquivalentTo(ids.AsEnumerable());
        }

        [Then(@"the table has (.*) rows?")]
        public void ThenTheTableHasRows(int rowCount)
        {
            this.table.AllRows.Count().Should().Be(rowCount);
        }

        [Then(@"each row has (.*) cells")]
        public void ThenEachRowHasCells(int cellCount)
        {
            foreach (var row in this.table.AllRows)
            {
                row.Cells.Count().Should().Be(cellCount);
            }
        }

        [Then(@"(.*)\w{2} cell in (.*)\w{2} row has text ""(.*)""")]
        public void ThenNdCellInStRowHasText(int cellIndex, int rowIndex, string text)
        {
            this.table.AllRows.ToArray()[rowIndex - 1].Cells.ToArray()[cellIndex - 1].Text.Should().Be(text);
        }

        [Then(@"(.*)\w{2} cell in (.*)\w{2} row in the table head has text ""(.*)""")]
        public void ThenStCellInStRowHasTextInTheTableHead(int cellIndex, int rowIndex, string text)
        {
            this.table.HeadRows.ToArray()[rowIndex - 1].Cells.ToArray()[cellIndex - 1].Text.Should().Be(text);
        }

        [Then(@"(.*)\w{2} cell in (.*)\w{2} row in the table body has text ""(.*)""")]
        public void ThenStCellInNdRowInTheTableBodyHasText(int cellIndex, int rowIndex, string text)
        {
            this.table.BodyRows.ToArray()[rowIndex - 1].Cells.ToArray()[cellIndex - 1].Text.Should().Be(text);
        }

        [Then(@"the table has (.*) rows? in header")]
        public void ThenTheTableHasRowsInHeader(int rowCount)
        {
            this.table.HeadRows.Count().Should().Be(rowCount);
        }

        [Then(@"the table has (.*) rows? in body")]
        public void ThenTheTableHasRowsInBody(int rowCount)
        {
            this.table.BodyRows.Count().Should().Be(rowCount);
        }

        [Then(@"the table has (.*) rows? in footer")]
        public void ThenTheTableHasRowsInFooter(int rowCount)
        {
            this.table.FooterRows.Count().Should().Be(rowCount);
        }
    }
}