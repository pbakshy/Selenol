Feature: MultiSelectFeature
	As an user I want to have ability to work with strong typed multi select element.

Scenario Outline: Get selected element
	Given that I am viewing "Elements" page
	When I look at a multi select with id "<id>"
	Then options with text "<options_text>" are selected in the multi select

	Examples: 
	| id             | options_text |
	| multi-select-1 | Dog, Rat     |
	| multi-select-2 |              |

Scenario Outline:  Select options by text
	Given that I am viewing "Elements" page
	When I look at a multi select with id "<id>"
	And I clear selection in the multi select
	And I select an option with text "<text_1>" in the multi select
	Then options with text "<text_1>" are selected in the multi select
	When I select an option with text "<text_2>" in the multi select
	Then options with text "<text_1>,<text_2>" are selected in the multi select

	Examples: 
	| id             | text_1 | text_2 |
	| multi-select-1 | Cat    | Rat    |
	| multi-select-2 | Rat    | Dog    |

Scenario Outline:  Select options by value
	Given that I am viewing "Elements" page
	When I look at a multi select with id "<id>"
	And I clear selection in the multi select
	And I select an option with value "<value_1>" in the multi select
	Then options with value "<value_1>" are selected in the multi select
	When I select an option with value "<value_2>" in the multi select
	Then options with value "<value_1>,<value_2>" are selected in the multi select

	Examples: 
	| id             | value_1   | value_2   |
	| multi-select-1 | cat-value | rat-value |
	| multi-select-2 | rat-value | dog-value |

Scenario: All multi selects
	Given that I am viewing "Elements" page
	Then there are multi selects with id "multi-select-1, multi-select-2"