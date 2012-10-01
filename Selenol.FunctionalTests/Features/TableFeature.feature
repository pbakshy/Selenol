Feature: TableFeature
	As an user I want to have ability to work with strong typed table element.

Scenario: All tables
	Given that I am viewing "Elements" page
	Then there are tables with id "table-1, table-2"

Scenario: Browsing simple table
	Given that I am viewing "Elements" page
	When I looking at table with id "table-1"
	Then the table has 2 rows
	And each row has 2 cells
	And 2nd cell in 1st row has text "table 1 row 1 cell 2"
	And 1st cell in 2nd row has text "table 1 row 2 cell 1"

Scenario: Browsing complex table
	Given that I am viewing "Elements" page
	When I looking at table with id "table-2"
	Then the table has 5 rows
	And the table has 2 rows in header
	And the table has 2 rows in body
	And the table has 1 row in footer
	And 1st cell in 1st row has text "Head"
	And 1st cell in 1st row in the table head has text "Head" 
	And 1st cell in 2nd row has text "Subhead 1"
	And 1st cell in 2nd row in the table body has text "table 2 row 2 cell 1" 
