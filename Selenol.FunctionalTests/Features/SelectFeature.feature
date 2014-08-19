Feature: SelectFeature
	As an user I want to have ability to work with strong typed select element.

Scenario Outline: Get selected element
	Given that I am viewing "Elements" page
	When I look at a select with id "<id>"
	Then in the select selected text is "<text>"
	And in the select selected value is "<value>"

	Examples: 
	| id       | text             | value            |
	| select-1 | Rat              | rat-value        |
	| select-2 | Select an animal | Select an animal |

Scenario: All selects
	Given that I am viewing "Elements" page
	Then there are selects with id "select-1, select-2"

Scenario Outline: Select element by text
	Given that I am viewing "Elements" page
	When I look at a select with id "<id>"
	And I select option with text "<text>" in the select 
	Then in the select selected text is "<text>"
	And in the select selected value is "<value>"

	Examples: 
	| id       | text | value     |
	| select-1 | Cat  | cat-value |
	| select-2 | Dog  | dog-value |

Scenario Outline: Select element by value
	Given that I am viewing "Elements" page
	When I look at a select with id "<id>"
	And I select option with value "<value>" in the select
	Then in the select selected text is "<text>"
	And in the select selected value is "<value>"

	Examples: 
	| id       | text | value     |
	| select-1 | Cat  | cat-value |
	| select-2 | Dog  | dog-value |
