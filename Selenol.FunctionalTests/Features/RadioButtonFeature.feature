Feature: RadioButtonFeature
	As an user I want to have ability to work with strong typed radio button element.

Scenario Outline: Get selected value
	Given that I am viewing "Elements" page
	When I look at a radio button with id "<id>"
	Then the radio button has value "<value>"

	Examples: 
	| id      | value |
	| radio-1 | false |
	| radio-2 | true  |
	| radio-3 | false |
	
Scenario: All radio buttons
	Given that I am viewing "Elements" page
	Then there are radio buttons with id "radio-1, radio-2, radio-3"

Scenario Outline: Select an option
	Given that I am viewing "Elements" page
	When I look at a radio button with id "<id>"
	And I check the radio button
	Then the radio button has value "true"

	Examples: 
	| id      |
	| radio-1 |
	| radio-2 |
	| radio-3 |